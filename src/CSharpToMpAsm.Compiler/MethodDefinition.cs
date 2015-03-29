using System.Text;
using CSharpToMpAsm.Compiler.Codes;

namespace CSharpToMpAsm.Compiler
{
    public class MethodDefinition
    {
        public const int CodeAddressNotSet = -1;
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

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (CodeAddress != CodeAddressNotSet)
            {
                sb.AppendFormat("[CodeAddress({0})]", CodeAddress);
            }

            if (ReturnValueLocation != null)
            {
                sb.AppendFormat("[Address({0})]", ReturnValueLocation);
            }

            sb.AppendFormat("{0} {1}(", ReturnType, Name);

            foreach (var parameter in Parameters)
            {
                sb.Append(parameter);
            }

            sb.Append(")");
            return sb.ToString();
        }
    }
}