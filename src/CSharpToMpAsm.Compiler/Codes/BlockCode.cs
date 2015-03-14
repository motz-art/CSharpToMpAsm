using System;
using System.Text;

namespace CSharpToMpAsm.Compiler.Codes
{
    internal class BlockCode : ICode
    {
        private readonly ICode[] _codes;

        public BlockCode(ICode[] codes)
        {
            _codes = codes;
        }

        public TypeDefinition ResultType
        {
            get { return TypeDefinitions.Void; }
        }

        public ResultLocation Location
        {
            get { throw new InvalidOperationException("BlobkCode has Void result type."); }
        }

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
            foreach (var code in _codes)
            {
                code.WriteMpAsm(writer, memManager);
            }
        }
    }
}