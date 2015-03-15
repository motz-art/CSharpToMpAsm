using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class ShiftRight : ShiftBase
    {
        public ShiftRight(ICode left, ICode right) : base(left, right)
        {
        }

        protected override void RotateOnce(IMpAsmWriter writer)
        {
            for (int i = ResultType.Size - 1; i >= 0; i--)
            {
                writer.RotateRightFileToW(Left.Location + i);
            }
        }
    }
}