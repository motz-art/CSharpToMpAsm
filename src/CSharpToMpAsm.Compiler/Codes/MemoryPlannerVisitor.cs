using System;
using System.Collections.Generic;
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

        protected override ICode Optimize(Assign assign)
        {
            assign.Destination.Location = _memManager.Alloc(assign.Destination);
            return new Assign(assign.Destination, Visit(assign.Code));
        }

        protected override ICode Optimize(GetValue getValue)
        {
            var location = _memManager.Alloc(getValue.Variable);
            getValue.Variable.Location = location;
            return new GetValue(getValue.Variable, getValue.ResultType, _memManager.Alloc(getValue.ResultType.Size));
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
            foreach (var parameter in method.Parameters)
            {
                parameter.Location = _memManager.Alloc(parameter);
            }
            if (method.ReturnValueLocation == null)
                method.ReturnValueLocation = _memManager.Alloc(method.ReturnType.Size);
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
            var codes = new List<ICode>();
            foreach (var code in blockCode.Codes)
            {

                var assignCode = code as Assign;
                
                if (assignCode != null) codes.Add(OptimizeAssign(assignCode));
                else
                {
                    var newCode = Visit(code);
                    codes.Add(newCode);

                    if (newCode.ResultType != TypeDefinitions.Void)
                        _memManager.Dispose(newCode.Location, newCode.ResultType.Size);
                }
            }
            return new BlockCode(codes.ToArray());
        }

        private ICode OptimizeAssign(Assign assign)
        {
            assign.Destination.Location = _memManager.Alloc(assign.Destination);

            ICode code;
            var getValue = assign.Code as GetValue;
            if (getValue != null)
            {
                code = new GetValue(getValue.Variable, getValue.ResultType, assign.Destination.Location);
            }
            else
            {
                var intValue = assign.Code as IntValue;
                if (intValue != null)
                {
                    code = new IntValue(intValue.Value, intValue.ResultType, assign.Destination.Location);
                }
                else
                    code = Visit(assign.Code);
            }
            return code;
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
    }
}