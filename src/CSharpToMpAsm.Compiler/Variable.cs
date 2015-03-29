using CSharpToMpAsm.Compiler.Codes;

namespace CSharpToMpAsm.Compiler
{
    internal class Variable : IValueDestination
    {
        public Variable(string name, TypeDefinition type)
        {
            Name = name;
            Type = type;
        }

        public Variable(string name, TypeDefinition type, ResultLocation location)
            : this(name, type)
        {
            Location = location;
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