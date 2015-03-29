using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class BoolValue : ICode
    {
        public bool Value { get; private set; }

        public BoolValue(bool value)
        {
            Value = value;
        }

        public bool Equals(ICode other)
        {
            throw new NotImplementedException();
        }

        public TypeDefinition ResultType
        {
            get { throw new NotImplementedException(); }
        }

        public ResultLocation Location
        {
            get { throw new NotImplementedException(); }
        }

        public void WriteMpAsm(IMpAsmWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}