namespace CSharpToMpAsm.Compiler.Codes
{
    public class BitwiseAnd : BitwiseBase
    {
        public BitwiseAnd(ICode left, ICode right) : base(left, right)
        {
        }

        public BitwiseAnd(ICode left, ICode right, ResultLocation location) : base(left, right, location)
        {
        }

        protected override void WriteBitwiseOperation(IMpAsmWriter writer, ResultLocation location)
        {
            writer.AndWFile(location);
        }
    }
}