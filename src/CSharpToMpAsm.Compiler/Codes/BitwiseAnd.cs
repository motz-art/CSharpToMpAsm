using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class BitwiseAnd : BitwiseBase
    {
        public BitwiseAnd(ICode left, ICode right) : base(left, right)
        {
        }

        protected override void WriteBitwiseOperation(IMpAsmWriter writer, ResultLocation location)
        {
            writer.AndWFile(location);
        }
    }
}