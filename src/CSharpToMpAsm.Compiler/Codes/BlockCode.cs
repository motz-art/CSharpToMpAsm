using System;
using System.Text;

namespace CSharpToMpAsm.Compiler.Codes
{
    internal class BlockCode : ICode
    {
        public ICode[] Codes { get; private set; }

        public BlockCode(ICode[] codes)
        {
            Codes = codes;
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
            foreach (var code in Codes)
            {
                code.WriteMpAsm(writer, memManager);
            }
        }
    }
}