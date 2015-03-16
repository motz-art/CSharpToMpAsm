using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class GetReference : ICode
    {
        public IValueDestination Value { get; set; }

        public TypeDefinition ResultType { get; private set; }

        public ResultLocation Location { get; private set; }

        public GetReference(IValueDestination value)
        {
            Value = value;
            ResultType = TypeDefinitions.Reference(value.Type);
        }

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
            Location = memManager.Alloc(Value.Type.Size);
            writer.Comment("; Taking references is not implemented.");
        }
    }
}