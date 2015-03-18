using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class NullCode : ICode
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

        public bool Equals(ICode other)
        {
            return other is NullCode;
        }
    }
}