using CSharpToMpAsm.Compiler.Codes;

namespace CSharpToMpAsm.Compiler
{
    public class ParameterDestination : IValueDestination
    {
        public ParameterDestination(string name, TypeDefinition type)
        {
            Name = name;
            Type = type;
            GetValue = new GetValue(this);
        }

        public string Name { get; private set; }
        public ICode GetValue { get; private set; }
        public TypeDefinition Type { get; private set; }
        public ResultLocation Location { get; set; }
    }
}