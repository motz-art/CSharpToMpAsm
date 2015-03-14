using System.Collections.Generic;

namespace CSharpToMpAsm.Compiler
{
    internal class AttributeInfo
    {
        public string Name { get; set; }
        public AttributeArguments[] Arguments { get; set; }

        public AttributeInfo(string name, AttributeArguments[] arguments)
        {
            Name = name;
            Arguments = arguments;
        }
    }
}