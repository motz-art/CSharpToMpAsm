using CSharpToMpAsm.Compiler.Codes;

namespace CSharpToMpAsm.Compiler
{
    public class ParameterDestination : IValueDestination
    {
        protected bool Equals(ParameterDestination other)
        {
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ParameterDestination) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        public ParameterDestination(string name, TypeDefinition type)
        {
            Name = name;
            Type = type;
            GetValue = new GetValue(this);
        }

        public string Name { get; private set; }
        public ICode GetValue { get; private set; }
        public TypeDefinition Type { get; private set; }
        public ResultLocation Location { get; set; }
    }
}