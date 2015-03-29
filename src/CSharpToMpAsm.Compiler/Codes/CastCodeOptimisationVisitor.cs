namespace CSharpToMpAsm.Compiler.Codes
{
    public class CastCodeOptimisationVisitor : CodeOptimisationVisitor
    {
        private class ReduceVisitor : CodeOptimisationVisitor
        {
            private readonly TypeDefinition _toType;

            public ReduceVisitor(TypeDefinition toType)
            {
                _toType = toType;
            }

            protected override ICode Optimize(ShiftLeft shiftLeft)
            {
                var code = Visit(shiftLeft.Left);
                if (code.ResultType != _toType)
                {
                    code = new CastCode(_toType, code);
                }
                return new ShiftLeft(code, shiftLeft.Right, _toType);
            }

            protected override ICode Optimize(ShiftRight shiftLeft)
            {
                var code = Visit(shiftLeft.Left);
                if (code.ResultType != _toType)
                {
                    code = new CastCode(_toType, code);
                }
                return new ShiftRight(code, shiftLeft.Right, _toType);
            }

            protected override ICode Optimize(IntValue intValue)
            {
                return new IntValue(intValue.Value, _toType);
            }

            protected override ICode Optimize(GetValue getValue)
            {
                return new GetValue(getValue.Destination, _toType);
            }

            protected override ICode Optimize(CastCode castCode)
            {
                if (castCode.Code.ResultType == _toType)
                    return castCode.Code;
                var code = Visit(castCode.Code);
                if (code.ResultType == _toType)
                    return code;

                return castCode;
            }

            public static ICode ReduceType(CastCode castCode, TypeDefinition toType)
            {
                if (castCode.Code.ResultType == toType)
                    return castCode.Code;
                return new CastCode(toType, castCode.Code);
            }
        }

        protected override ICode Optimize(CastCode castCode)
        {
            if (castCode.ResultType.Size < castCode.Code.ResultType.Size)
            {
                var visitor = new ReduceVisitor(castCode.ResultType);
                var code = visitor.Visit(castCode.Code);
                if (code.ResultType == castCode.ResultType)
                    return code;
            }
            return base.Optimize(castCode);
        }
    }
}