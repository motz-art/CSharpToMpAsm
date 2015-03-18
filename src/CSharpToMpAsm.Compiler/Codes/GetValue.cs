using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class GetValue : ICode
    {
        public IValueDestination Variable { get; set; }
        public TypeDefinition ResultType { get; private set; }
        public ResultLocation Location { get; private set; }
        public bool PreserveLocation { get; set; }

        public GetValue(IValueDestination variable) : this(variable, variable.Type) { }

        public GetValue(IValueDestination variable, TypeDefinition toType)
        {
            Variable = variable;
            ResultType = toType.IsReference ? toType.TypeParameters[0] : toType;
        }

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
            writer.Comment(string.Format("; {0} ({1})", Variable.Location, Variable.Name));
            if (!PreserveLocation)
            {
                Location = memManager.Alloc(ResultType.Size);
                for (int i = 0; i < ResultType.Size; i++)
                {
                    writer.MoveFileToW(Variable.Location + i);
                    writer.MoveWToFile(Location + i);
                }
            }
        }

        protected bool Equals(GetValue other)
        {
            return Equals(Variable, other.Variable);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((GetValue) obj);
        }

        public override int GetHashCode()
        {
            return (Variable != null ? Variable.GetHashCode() : 0);
        }

        public bool Equals(ICode other)
        {
            var getValue = other as GetValue;
            if (getValue == null) return false;
            return Equals(getValue);
        }
    }
}