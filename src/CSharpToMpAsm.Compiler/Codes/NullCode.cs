using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    internal class NullCode : ICode
    {
        public TypeDefinition ResultType
        {
            get { throw new NotImplementedException(); }
        }

        public ResultLocation Location
        {
            get { throw new NotImplementedException(); }
        }

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
            throw new NotImplementedException();
        }
    }
}