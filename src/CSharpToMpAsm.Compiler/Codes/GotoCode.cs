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
        
        public void WriteMpAsm(IMpAsmWriter writer)
        {
            writer.GoTo(_label.GetLabel(writer));
        }

        public bool Equals(ICode other)
        {
            return Equals((object)other);
        }

        protected bool Equals(GotoCode other)
        {
            return Equals(_label, other._label);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((GotoCode) obj);
        }

        public override int GetHashCode()
        {
            return (_label != null ? _label.GetHashCode() : 0);
        }
    }
}
