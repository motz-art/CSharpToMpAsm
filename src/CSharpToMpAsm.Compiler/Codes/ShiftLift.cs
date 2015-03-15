namespace CSharpToMpAsm.Compiler.Codes
{
    public class ShiftLift : ShiftBase
    {
        public ShiftLift(ICode left, ICode right) : base(left, right)
        {
        }

        protected override void RotateOnce(IMpAsmWriter writer)
        {
            for (int j = 0; j < ResultType.Size; j++)
            {
                writer.RotateLeftFileToW(Location + j);
            }
        }
    }
}