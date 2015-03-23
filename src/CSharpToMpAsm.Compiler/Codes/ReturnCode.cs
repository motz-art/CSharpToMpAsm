using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class ReturnCode : ICode
    {
        public ICode Value { get; set; }

        public MethodDefinition Method { get; private set; }

        public TypeDefinition ResultType
        {
            get { return TypeDefinitions.Void; }
        }

        public ResultLocation Location
        {
            get { throw new InvalidOperationException("Void do not have location."); }
        }

        public ReturnCode(MethodDefinition method, ICode value)
        {
            if (method == null) throw new ArgumentNullException("method");
            Method = method;
            Value = value;
        }

        public void WriteMpAsm(IMpAsmWriter writer)
        {
            if (Value != null && Value.ResultType != TypeDefinitions.Void)
            {
                if (Value.ResultType == TypeDefinitions.Byte && Method.ReturnValueLocation.IsWorkRegister)
                {
                    var value = Value;
                    var cast = value as CastCode;
                    if (cast != null && cast.ResultType == TypeDefinitions.Byte)
                    {
                        value = cast.Code;
                    }
                    var literal = value as IntValue;
                    if (literal != null)
                    {
                        writer.Return((byte)literal.Value);
                    }
                }
                Value.WriteMpAsm(writer);

                for (int i = 0; i < ResultType.Size; i++)
                {
                    writer.MoveFileToW(Value.Location + i);
                    writer.MoveWToFile(Method.ReturnValueLocation + i);
                }
            }
            writer.Return();
        }

        protected bool Equals(ReturnCode other)
        {
            return Equals(Value, other.Value) && Equals(Method, other.Method);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ReturnCode) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Value != null ? Value.GetHashCode() : 0)*397) ^ (Method != null ? Method.GetHashCode() : 0);
            }
        }

        public bool Equals(ICode other)
        {
            var returnCode = other as ReturnCode;
            if (returnCode == null) return false;
            return Equals(returnCode);
        }
    }
}