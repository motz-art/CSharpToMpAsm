using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class GetValue : ICode
    {
        public IValueDestination Destination { get; set; }
        public TypeDefinition ResultType { get; private set; }
        public ResultLocation Location { get; private set; }

        public GetValue(IValueDestination destination) : this(destination, destination.Type) { }

        public GetValue(IValueDestination destination, TypeDefinition toType)
        {
            Destination = destination;
            ResultType = toType.IsReference ? toType.TypeParameters[0] : toType;
        }

        public GetValue(IValueDestination destination, TypeDefinition resultType, ResultLocation location) : this(destination, resultType)
        {
            Location = location;
        }

        public void WriteMpAsm(IMpAsmWriter writer)
        {
            writer.Comment(string.Format("; {0} ({1})", Destination.Location, Destination.Name));
            writer.Copy(Destination.Location, Location, ResultType.Size);
        }

        protected bool Equals(GetValue other)
        {
            return Equals(Destination, other.Destination);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((GetValue)obj);
        }

        public override int GetHashCode()
        {
            return (Destination != null ? Destination.GetHashCode() : 0);
        }

        public bool Equals(ICode other)
        {
            var getValue = other as GetValue;
            if (getValue == null) return false;
            return Equals(getValue);
        }
    }
}