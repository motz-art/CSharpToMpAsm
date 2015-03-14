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

        public string GetMpAsm(CompilationContext compilationContext)
        {
            var sb = new StringBuilder();
            foreach (var code in _codes)
            {
                sb.AppendLine(code.GetMpAsm(compilationContext));
            }
            return sb.ToString();
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