using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class IntValue : ICode
    {
        public int Value { get; private set; }

        public IntValue(int value)
            : this(value, TypeDefinitions.Int32)
        {
        }

        public IntValue(int value, TypeDefinition type)
        {
            Value = value;
            ResultType = type;
        }

        public IntValue(int value, TypeDefinition type, ResultLocation location) : this(value, type)
        {
            Location = location;
        }

        public TypeDefinition ResultType { get; private set; }

        public ResultLocation Location { get; private set; }

        public void WriteMpAsm(IMpAsmWriter writer)
        {
            var bytes = BitConverter.GetBytes(Value);

            writer.Comment(string.Format("; IntLiteral {0}(0x{0:X8}) to {1}.", Value, Location));

            for (int i = 0; i < ResultType.Size; i++)
            {
                if (i >= bytes.Length)
                    throw new InvalidOperationException("Literal value is too small.");

                var b = bytes[i];
                writer.LoadLiteralToW(b);
                if (!Location.IsWorkRegister)
                    writer.MoveWToFile(Location + i);
            }
        }

        protected bool Equals(IntValue other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((IntValue) obj);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public bool Equals(ICode other)
        {
            return Equals((object)other);
        }
    }
}