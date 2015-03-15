using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    internal class NullCode : ICode
    {
        public TypeDefinition ResultType
        {
            get { return TypeDefinitions.Void; }
        }

        public ResultLocation Location
        {
            get { throw new InvalidOperationException("Void result type does not have location."); }
        }

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
        }
    }
}