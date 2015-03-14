namespace CSharpToMpAsm.Compiler
{
    internal class AttributeArguments
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public AttributeArguments(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}