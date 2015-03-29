namespace CSharpToMpAsm.Compiler.Codes
{
    public class SwapfCode : ICode
    {
        public TypeDefinition ResultType
        {
            get { return TypeDefinitions.Byte;}
        }

        public ICode Value { get; private set; }

        public ResultLocation Location { get; set; }

        public SwapfCode(ICode value)
        {
            Value = value;
        }

        public SwapfCode(ICode value, ResultLocation location) : this(value)
        {
            Location = location;
        }

        public void WriteMpAsm(IMpAsmWriter writer)
        {
            Value.WriteMpAsm(writer);
            
            if (Location.IsWorkRegister)
                writer.SwapFileToW(Value.Location);
            else
            {
                writer.Copy(Value.Location, Location, ResultType.Size);
                writer.SwapFile(Location);
            }
        }

        protected bool Equals(SwapfCode other)
        {
            return Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((SwapfCode) obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public bool Equals(ICode other)
        {
            var swapf = other as SwapfCode;
            if (swapf == null) return false;
            return Equals(swapf);
        }
    }
}