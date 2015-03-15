using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class GetReference : ICode
    {
        public IValueDestination Value { get; set; }

        public TypeDefinition ResultType
        {
            get { return TypeDefinitions.Reference(Value.Type); }
        }

        public ResultLocation Location
        {
            get { return Value.Location; }
        }

        public GetReference(IValueDestination value)
        {
            Value = value;
        }

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
            
        }
    }
}