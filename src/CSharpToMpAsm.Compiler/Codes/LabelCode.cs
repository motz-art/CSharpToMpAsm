using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class LabelCode : ICode
    {
        public LabelCode(string labelName)
        {
            LabelName = labelName;
        }

        public TypeDefinition ResultType { get; set; }
        public string LabelName { get; set; }
        public ResultLocation Location { get; set; }
        public ILabel _label;

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
            _label = GetLabel(writer);
            writer.WriteLabel(_label);
        }

        public ILabel GetLabel(IMpAsmWriter writer)
        {
            return _label ?? (_label = writer.CreateLabel(LabelName));
        }
    }
}
