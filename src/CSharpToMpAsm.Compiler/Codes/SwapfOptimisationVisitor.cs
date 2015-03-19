using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class SwapfOptimisationVisitor : CodeOptimisationVisitor
    {
        protected override ICode Optimize(BitwiseOr bitwiseOr)
        {
            if (bitwiseOr.ResultType != TypeDefinitions.Byte)
                return base.Optimize(bitwiseOr);
            
            var leftAnd = bitwiseOr.Left as BitwiseAnd;
            if (leftAnd == null)
                return base.Optimize(bitwiseOr);

            var rightAnd = bitwiseOr.Right as BitwiseAnd;
            if (rightAnd == null)
                return base.Optimize(bitwiseOr);

            int leftValue, rightValue;
            ICode leftParameter, rightParameter;
            if (TestAnd(leftAnd, out leftValue, out leftParameter)
                && TestAnd(rightAnd, out rightValue, out rightParameter))
            {
                if ((leftValue & rightValue) == 0 && leftParameter.Equals(rightParameter))
                {
                    if ((leftValue | rightValue) != 0xFF) throw new NotImplementedException();

                    return new SwapfCode(leftParameter);
                }
            }
            return base.Optimize(bitwiseOr);
        }

        private bool TestAnd(BitwiseAnd bitwiseAnd, out int value, out ICode parameter)
        {
            return (Test(bitwiseAnd.Left as ShiftBase, bitwiseAnd.Right as IntValue, out value, out parameter)) 
                 || Test(bitwiseAnd.Right as ShiftBase, bitwiseAnd.Left as IntValue, out value, out parameter);
        }

        private static bool Test(ShiftBase shift, IntValue intValue, out int value, out ICode parameter)
        {
            value = 0;
            parameter = null;
            if (shift == null || intValue==null) return false;

            parameter = shift.Left;
            var shiftValue = shift.Right as IntValue;
            if (shiftValue == null || shiftValue.Value != 4) return false;
            

            value = intValue.Value;

            return ((value & 0x0F) == 0 && shift is ShiftLeft)
                || ((value & 0xF0) == 0 && shift is ShiftRight);
        }
    }
}