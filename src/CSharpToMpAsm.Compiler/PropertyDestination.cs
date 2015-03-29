using CSharpToMpAsm.Compiler.Codes;

namespace CSharpToMpAsm.Compiler
{
    internal class PropertyDestination : IValueDestination
    {
        public PropertyDestination(string name, TypeDefinition type, ResultLocation address)
        {
            Name = name;
            Type = type;
            Location = address;
        }

        public string Name { get; private set; }

        public ICode CreateGetValueCode()
        {
            return new GetValue(this);
        }

        public TypeDefinition Type { get; private set; }
        public ResultLocation Location { get; set; }
    }
}