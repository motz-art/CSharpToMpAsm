using CSharpToMpAsm.Compiler.Codes;

namespace CSharpToMpAsm.Compiler
{
    internal class Variable : IValueDestination
    {
        public Variable(string name, TypeDefinition type, ResultLocation location)
        {
            Name = name;
            Type = type;
            Location = location;
            GetValue = new GetValue(this);
        }

        public string Name { get; private set; }

        public ICode GetValue { get ; private set ; }

        public TypeDefinition Type { get; private set; }

        public ResultLocation Location { get; private set; }
    }
}