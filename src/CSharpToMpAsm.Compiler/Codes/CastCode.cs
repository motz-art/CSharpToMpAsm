using System;
using System.Text;

namespace CSharpToMpAsm.Compiler.Codes
{
    internal class CastCode : ICode
    {
        private readonly TypeDefinition _type;
        private readonly ICode _code;

        public CastCode(TypeDefinition type, ICode code)
        {
            _type = type;
            _code = code;
        }

        public TypeDefinition ResultType
        {
            get { return _type; }
        }

        public ResultLocation Location
        {
            get {
                if (ResultType == _code.ResultType)
                {
                    return _code.Location;
                }
                if (ResultType == TypeDefinitions.Byte)
                {
                    return ResultLocation.WorkRegister;
                }
                if (ResultType == TypeDefinitions.SByte)
                {
                    return ResultLocation.WorkRegister;
                }
                if (ResultType == TypeDefinitions.Int32)
                {
                    throw new NotImplementedException();
                }
                throw new NotSupportedException();
            }
        }

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
            _code.WriteMpAsm(writer, memManager);
            if (!_code.Location.IsWorkRegister)
            {
                writer.Comment(string.Format("; Cast to {0} type.", ResultType));
                writer.MoveFileToW(_code.Location);
            }
        }
    }
}