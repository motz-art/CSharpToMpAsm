using System;
using System.Text;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class CastCode : ICode
    {
        private readonly TypeDefinition _type;
        private ResultLocation _location;
        public ICode Code { get; private set; }

        public CastCode(TypeDefinition type, ICode code)
        {
            _type = type;
            Code = code;
        }

        public TypeDefinition ResultType
        {
            get { return _type; }
        }

        public ResultLocation Location
        {
            get { return _location; }
        }

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
            Code.WriteMpAsm(writer, memManager);

            if (ResultType == Code.ResultType)
            {
                _location =  Code.Location;
                return;
            }
            if (ResultType == TypeDefinitions.Byte || ResultType == TypeDefinitions.SByte)
            {
                _location = ResultLocation.WorkRegister;

                if (!Code.Location.IsWorkRegister)
                {
                    writer.Comment(string.Format("; Cast to {0} type.", ResultType));
                    writer.MoveFileToW(Code.Location);
                }
                return;
            }
            
            if (ResultType == TypeDefinitions.Int32)
            {
                writer.Comment(string.Format("; Cast to {0} type.", ResultType));

                _location = memManager.Alloc(ResultType.Size);

                if (Code.ResultType.IsSigned() && Code.ResultType.Size < ResultType.Size)
                {
                    throw new NotImplementedException();
                }

                for (int i = 0; i < ResultType.Size; i++)
                {
                    if (Code.ResultType.Size > i)
                    {
                        writer.MoveFileToW(Code.Location + i);
                        writer.MoveWToFile(_location + i);
                    }
                    else
                    {
                        writer.ClearFile(_location + i);
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