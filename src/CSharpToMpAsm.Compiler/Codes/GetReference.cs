using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class GetReference : ICode
    {
        public IValueDestination Value { get; set; }

        public TypeDefinition ResultType { get; private set; }

        public ResultLocation Location { get; private set; }

        public GetReference(IValueDestination value)
        {
            Value = value;
            ResultType = TypeDefinitions.Reference(value.Type);
        }

        public GetReference(IValueDestination value, ResultLocation location) : this(value)
        {
            Location = location;
        }

        public void WriteMpAsm(IMpAsmWriter writer)
        {
            writer.Copy(Value.Location, Location, Value.Type.Size);
        }

        protected bool Equals(GetReference other)
        {
            return Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((GetReference) obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public bool Equals(ICode other)
        {
            var reference = other as GetReference;
            if (reference == null) return false;
            return Equals(reference);
        }
    }
}