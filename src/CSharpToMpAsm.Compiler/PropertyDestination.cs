using CSharpToMpAsm.Compiler.Codes;

namespace CSharpToMpAsm.Compiler
{
    internal class PropertyDestination : IValueDestination
    {
        public PropertyDestination(string name, TypeDefinition type, ResultLocation address)
        {
            Name = name;
            GetValue = new GetValue(this);
            Type = type;
            Location = address;
        }

        public string Name { get; private set; }
        public ICode GetValue { get; private set; }
        public TypeDefinition Type { get; private set; }
        public ResultLocation Location { get; private set; }
    }
}