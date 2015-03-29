using System.Linq;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class MemoryPlannerVisitor : CodeOptimisationVisitor
    {
        private readonly IMemoryManager _memManager;

        public MemoryPlannerVisitor(IMemoryManager memManager)
        {
            _memManager = memManager;
        }

        protected override ICode Optimize(EqualityCode equalityCode)
        {
            var zeroConst = (equalityCode.Left as IntValue)??(equalityCode.Right as IntValue);
            if (zeroConst != null && zeroConst.Value == 0)
            {
                var getValue = (equalityCode.Left as GetValue) ?? (equalityCode.Right as GetValue);
                if (getValue != null)
                {
                    return new EqualityCode(new GetValue(getValue.Destination, getValue.ResultType, getValue.Destination.Location), zeroConst );
                }
            }

            var left = Visit(equalityCode.Left);
            var right = Visit(equalityCode.Right);

            _memManager.Dispose(left.Location, left.ResultType.Size);
            _memManager.Dispose(right.Location, right.ResultType.Size);

            if (ReferenceEquals(left, equalityCode.Left)
                && ReferenceEquals(right, equalityCode.Right))
                return equalityCode;
            return new EqualityCode(left, right);
        }

        protected override ICode Optimize(Assign assign)
        {
            assign.Destination.Location = _memManager.Alloc(assign.Destination);
            return new Assign(assign.Destination, Visit(assign.Code));
        }

        protected override ICode Optimize(GetValue getValue)
        {
            var location = _memManager.Alloc(getValue.Destination);
            getValue.Destination.Location = location;

            var resultLocation = _memManager.Alloc(getValue.ResultType.Size);

            return new GetValue(getValue.Destination, getValue.ResultType, resultLocation);
        }

        protected override ICode Optimize(AddInts addInts)
        {
            var left = Visit(addInts.Left);
            var right = Visit(addInts.Right);
            _memManager.Dispose(right.Location, right.ResultType.Size);
            return new AddInts(left, right, left.Location);
        }

        protected override ICode Optimize(BitwiseAnd bitwiseAnd)
        {
            var left = Visit(bitwiseAnd.Left);
            var right = Visit(bitwiseAnd.Right);
            _memManager.Dispose(right.Location, right.ResultType.Size);
            return new BitwiseAnd(left, right, left.Location);
        }

        protected override ICode Optimize(Call call)
        {
            var method = call.Method;

            SetupMethodDataLocations(method);

            var args = call.Args.Select(x => Visit(x)).ToArray();
            return new Call(method, args);
        }

        protected override ICode Optimize(BitwiseOr bitwiseOr)
        {
            var left = Visit(bitwiseOr.Left);
            var right = Visit(bitwiseOr.Right);
            _memManager.Dispose(right.Location, right.ResultType.Size);
            return new BitwiseOr(left, right, left.Location);
        }

        protected override ICode Optimize(ShiftLeft shiftLeft)
        {
            var left = Visit(shiftLeft.Left);
            var right = Visit(shiftLeft.Right);
            _memManager.Dispose(right.Location, right.ResultType.Size);
            return new ShiftLeft(left, right, shiftLeft.ResultType, left.Location);
        }

        protected override ICode Optimize(ShiftRight shiftRight)
        {
            var left = Visit(shiftRight.Left);
            var right = Visit(shiftRight.Right);
            _memManager.Dispose(right.Location, right.ResultType.Size);
            return new ShiftRight(left, right, shiftRight.ResultType, left.Location);
        }

        protected override ICode Optimize(GetReference getReference)
        {
            var value = getReference.Value;
            value.Location = _memManager.Alloc(value);
            return new GetReference(value, value.Location);
        }

        protected override ICode Optimize(PostDecrementCode postDecrement)
        {
            return new PostDecrementCode(postDecrement.Destination, _memManager.Alloc(postDecrement.ResultType.Size));
        }

        protected override ICode Optimize(PostIncrementCode postIncrement)
        {
            return new PostIncrementCode(postIncrement.Destination, _memManager.Alloc(postIncrement.ResultType.Size));
        }

        protected override ICode Optimize(SwapfCode swapf)
        {
            var code = Visit(swapf.Value);
            return new SwapfCode(code, code.Location);
        }

        protected override ICode Optimize(BlockCode blockCode)
        {
            return new BlockCode(blockCode.Codes.Select(OptimizeBlockItem).ToArray());
        }

        private ICode OptimizeBlockItem(ICode code)
        {
            var assignCode = code as Assign;
            if (assignCode != null) return OptimizeAssign(assignCode);

            var postIncrement = code as PostIncrementCode;
            if (postIncrement != null)
            {
                postIncrement.Destination.Location = _memManager.Alloc(postIncrement.Destination);
                return new PostIncrementCode(postIncrement.Destination, postIncrement.Destination.Location);
            }

            var postDecrement = code as PostDecrementCode;
            if (postDecrement != null)
            {
                postDecrement.Destination.Location = _memManager.Alloc(postDecrement.Destination);
                return new PostDecrementCode(postDecrement.Destination, postDecrement.Destination.Location);
            }

            var newCode = Visit(code);

            if (newCode.ResultType != TypeDefinitions.Void)
                _memManager.Dispose(newCode.Location, newCode.ResultType.Size);

            return newCode;
        }

        private ICode OptimizeAssign(Assign assign)
        {
            assign.Destination.Location = _memManager.Alloc(assign.Destination);

            var valueCode = OptimizeValueCode(assign);
            return new Assign(assign.Destination, valueCode);
        }

        private ICode OptimizeValueCode(Assign assign)
        {
            var getValue = assign.Code as GetValue;
            if (getValue != null)
            {
                return new GetValue(getValue.Destination, getValue.ResultType, assign.Destination.Location);
            }

            var intValue = assign.Code as IntValue;
            if (intValue != null)
            {
                return new IntValue(intValue.Value, intValue.ResultType, assign.Destination.Location);
            }

            var swapf = assign.Code as SwapfCode;
            if (swapf != null)
            {
                getValue = swapf.Value as GetValue;
                if (getValue != null)
                {
                    if (ReferenceEquals(getValue.Destination, assign.Destination))
                        return new SwapfCode(new GetValue(getValue.Destination, getValue.ResultType, assign.Destination.Location), assign.Destination.Location);

                    return new SwapfCode(new GetValue(getValue.Destination, getValue.ResultType, getValue.Destination.Location), ResultLocation.WorkRegister);
                }
            }

            return Visit(assign.Code);
        }

        protected override ICode Optimize(CastCode castCode)
        {
            var newCode = Visit(castCode.Code);
            _memManager.Dispose(newCode.Location, newCode.ResultType.Size);
            var location = _memManager.Alloc(castCode.ResultType.Size);
            return new CastCode(castCode.ResultType, newCode, location);
        }

        protected override ICode Optimize(IntValue intValue)
        {
            var location = _memManager.Alloc(intValue.ResultType.Size);
            return new IntValue(intValue.Value, intValue.ResultType, location);
        }

        public void SetupMethodDataLocations(MethodDefinition method)
        {
            if (method.ReturnType != TypeDefinitions.Void && method.ReturnValueLocation == null)
            {
                if (method.ReturnType.Size == 1)
                {
                    method.ReturnValueLocation = ResultLocation.WorkRegister;
                }
                else
                {
                    method.ReturnValueLocation = _memManager.Alloc(method.ReturnType.Size);
                }
            }

            foreach (var parameter in method.Parameters)
            {
                if (parameter.Location != null) continue;
                if (IsWorkingRegisterUsagePosiible(method, parameter))
                {
                    parameter.Location = ResultLocation.WorkRegister;
                }
                else
                {
                    parameter.Location = _memManager.Alloc(parameter);
                }
            }
        }

        private bool IsWorkingRegisterUsagePosiible(MethodDefinition method, ParameterDestination parameter)
        {
            if (parameter.Type.IsReference && method.ReturnValueLocation == ResultLocation.WorkRegister) return false;

            var type = parameter.Type;
            if (parameter.Type.IsReference)
                type = CommonCodes.Dereference(parameter.Type);

            if (type.Size != 1) return false;
            if (method.Parameters.Where(x => x.Location != null).Any(x => x.Location.IsWorkRegister)) return false;
            var visitor = new ParameterUsageVisitor(parameter);
            visitor.Visit(method.Body);
            return visitor.IsWorkingRegisterUsagePosiible;
        }

        private class ParameterUsageVisitor : CodeOptimisationVisitor
        {
            private bool _isFirst = true;
            private bool _isSet = false;

            private readonly ParameterDestination _parameter;

            public ParameterUsageVisitor(ParameterDestination parameter)
            {
                _parameter = parameter;
                IsWorkingRegisterUsagePosiible = true;
            }

            public override ICode Visit(ICode code)
            {
                if (_isSet)
                    IsWorkingRegisterUsagePosiible = false;

                var result = base.Visit(code);
                _isFirst = false;
                return result;
            }

            protected override ICode Optimize(GetValue getValue)
            {
                if (ReferenceEquals(getValue.Destination, _parameter) && !_isFirst)
                    IsWorkingRegisterUsagePosiible = false;

                return base.Optimize(getValue);
            }

            protected override ICode Optimize(Assign assign)
            {
                var result = base.Optimize(assign);

                if (ReferenceEquals(assign.Destination, _parameter))
                    _isSet = true;

                return result;
            }

            public bool IsWorkingRegisterUsagePosiible { get; private set; }
        }
    }
}