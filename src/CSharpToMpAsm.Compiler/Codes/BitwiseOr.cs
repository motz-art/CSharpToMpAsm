namespace CSharpToMpAsm.Compiler.Codes
{
    public class BitwiseOr : BitwiseBase
    {
        public BitwiseOr(ICode left, ICode right) : base(left, right)
        {
        }

        public BitwiseOr(ICode left, ICode right, ResultLocation location) : base(left, right, location)
        {
        }

        protected override void WriteBitwiseOperation(IMpAsmWriter writer, ResultLocation location)
        {
            writer.OrWFile(location);
        }
    }
}