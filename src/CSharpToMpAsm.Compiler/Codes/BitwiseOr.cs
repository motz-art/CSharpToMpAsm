namespace CSharpToMpAsm.Compiler.Codes
{
    public class BitwiseOr : BitwiseBase
    {
        public BitwiseOr(ICode left, ICode right) : base(left, right)
        {
            
        }

        protected override void WriteBitwiseOperation(IMpAsmWriter writer, ResultLocation location)
        {
            writer.OrWFile(location);
        }
    }
}