using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class CastCode : ICode
    {
        private readonly TypeDefinition _type;
        public ICode Code { get; private set; }

        public CastCode(TypeDefinition type, ICode code)
        {
            _type = type;
            Code = code;
        }

        public CastCode(TypeDefinition type, ICode code, ResultLocation location) : this(type, code)
        {
            Location = location;
        }

        public TypeDefinition ResultType
        {
            get { return _type; }
        }

        public ResultLocation Location { get; private set; }

        public void WriteMpAsm(IMpAsmWriter writer)
        {
            Code.WriteMpAsm(writer);

            if (ResultType == Code.ResultType || ResultType == TypeDefinitions.Byte || ResultType == TypeDefinitions.SByte)
            {
                writer.Copy(Code.Location, Location, ResultType.Size);
                return;
            }
            
            if (ResultType == TypeDefinitions.Int32)
            {
                writer.Comment(string.Format("; Cast to {0} type.", ResultType));

                if (Code.ResultType.IsSigned() && Code.ResultType.Size < ResultType.Size)
                {
                    throw new NotImplementedException();
                }

                for (int i = 0; i < ResultType.Size; i++)
                {
                    if (Code.ResultType.Size > i)
                    {
                        writer.MoveFileToW(Code.Location + i);
                        writer.MoveWToFile(Location + i);
                    }
                    else
                    {
                        writer.ClearFile(Location + i);
                    }
                }
                return;
            }
            throw new NotSupportedException();
        }

        public bool Equals(ICode other)
        {
            return Equals((object)other);
        }

        protected bool Equals(CastCode other)
        {
            return Equals(Code, other.Code) && Equals(_type, other._type);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CastCode) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Code != null ? Code.GetHashCode() : 0)*397) ^ (_type != null ? _type.GetHashCode() : 0);
            }
        }
    }
}