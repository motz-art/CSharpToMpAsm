using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class GotoCode : ICode
    {
        private LabelCode _label;
        
        public TypeDefinition ResultType
        {
            get
            {
                return TypeDefinitions.Void;
            }
        }

        public ResultLocation Location { get { throw new InvalidOperationException("Void code type doesn't have result location."); } }

        public GotoCode(LabelCode label)
        {
            _label = label;
        }

        public string GetMpAsm(CompilationContext compilationContext)
        {
            return string.Empty;
        }

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
            writer.GoTo(_label.GetLabel(writer));
        }
    }
}
