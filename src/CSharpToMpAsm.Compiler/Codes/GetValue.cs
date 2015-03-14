using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    internal class GetValue : ICode
    {
        public IValueDestination Variable { get; set; }

        public GetValue(IValueDestination variable)
        {
            Variable = variable;
        }

        public TypeDefinition ResultType
        {
            get { return Variable.Type; }
        }

        public ResultLocation Location
        {
            get { return Variable.Location; }
        }

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
            writer.Comment(string.Format("; {0} ({1})", Variable.Location, Variable.Name));
        }
    }
}