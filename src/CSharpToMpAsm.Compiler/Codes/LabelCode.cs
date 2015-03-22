using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class LabelCode : ICode
    {
        public LabelCode(string labelName)
        {
            LabelName = labelName;
        }

        public TypeDefinition ResultType { get { return TypeDefinitions.Void; } }
        public string LabelName { get; set; }
        public ResultLocation Location { get { throw new InvalidOperationException(); } }
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

        public bool Equals(ICode other)
        {
            return Equals((object)other);
        }

        protected bool Equals(LabelCode other)
        {
            return Equals(_label, other._label);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((LabelCode)obj);
        }

        public override int GetHashCode()
        {
            return (_label != null ? _label.GetHashCode() : 0);
        }
    }
}
