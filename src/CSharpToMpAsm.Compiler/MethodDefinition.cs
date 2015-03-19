using CSharpToMpAsm.Compiler.Codes;

namespace CSharpToMpAsm.Compiler
{
    public class MethodDefinition
    {
        public MethodDefinition Overrides;
        public ILabel Label;
        public ParameterDestination[] Parameters { get; set; }

        public TypeDefinition ReturnType { get; set; }
        public ResultLocation ReturnValueLocation { get; set; }

        public MethodDefinition(string name)
        {
            Name = name;
            ReturnType = TypeDefinitions.Void;
        }

        public int CodeAddress { get; set; }

        public ICode Body { get; set; }

        public bool IsAbstract { get; set; }

        public bool IsVirtual { get; set; }

        public string Name { get; set; }

        public bool ShouldInline { get; set; }

        public ICode GenerateIl(ICode[] args)
        {
            return new Call(this, args);
        }

        public void Optimize()
        {
        }
    }
}