using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class GetValue : ICode
    {
        public IValueDestination Variable { get; set; }
        public TypeDefinition ResultType { get; private set; }
        public ResultLocation Location { get; private set; }

        public GetValue(IValueDestination variable) : this(variable, variable.Type) { }

        public GetValue(IValueDestination variable, TypeDefinition toType)
        {
            Variable = variable;
            ResultType = toType.IsReference ? toType.TypeParameters[0] : toType;
        }

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
            writer.Comment(string.Format("; {0} ({1})", Variable.Location, Variable.Name));
            Location = memManager.Alloc(ResultType.Size);
            for (int i = 0; i < ResultType.Size; i++)
            {
                writer.MoveFileToW(Variable.Location + i);
                writer.MoveWToFile(Location + i);
            }
        }
    }
}