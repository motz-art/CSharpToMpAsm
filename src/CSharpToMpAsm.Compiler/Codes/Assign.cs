using System;
using System.Text;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class Assign : ICode
    {
        public IValueDestination Destination { get; set; }
        public ICode Code { get; set; }

        public Assign(IValueDestination destination, ICode code)
        {
            if (destination == null) throw new ArgumentNullException("destination");
            if (code == null) throw new ArgumentNullException("code");

            Destination = destination;
            Code = code;

            var destinationType = CommonCodes.Dereference(Destination.Type);
            var codeType = CommonCodes.Dereference(Code.ResultType);

            if (destinationType != codeType)
            {
                throw new InvalidOperationException("DestinationType do not match expression type.");
            }
        }

        public TypeDefinition ResultType
        {
            get { return Code.ResultType; }
        }

        public ResultLocation Location
        {
            get { return Code.Location; }
        }

        public void WriteMpAsm(IMpAsmWriter writer)
        {
            Code.WriteMpAsm(writer);

            writer.Comment(string.Format("; Assign {0} at {1} to {2} ({3})",
                    Code.ResultType,
                    Code.Location,
                    Destination.Location, Destination.Name));

            writer.Copy(Code.Location, Destination.Location, Destination.Type.Size);
        }

        public bool Equals(ICode other)
        {
            return Equals((object)other);
        }

        protected bool Equals(Assign other)
        {
            return Equals(Code, other.Code) && Equals(Destination, other.Destination);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Assign) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Code != null ? Code.GetHashCode() : 0)*397) ^ (Destination != null ? Destination.GetHashCode() : 0);
            }
        }
    }
}