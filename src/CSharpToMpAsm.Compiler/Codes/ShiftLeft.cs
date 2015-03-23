namespace CSharpToMpAsm.Compiler.Codes
{
    public class ShiftLeft : ShiftBase
    {
        public ShiftLeft(ICode left, ICode right) : base(left, right)
        {
        }        
        
        public ShiftLeft(ICode left, ICode right, TypeDefinition resultType) : base(left, right, resultType)
        {
        }

        public ShiftLeft(ICode left, ICode right, TypeDefinition resultType, ResultLocation location) : base(left, right, resultType, location)
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