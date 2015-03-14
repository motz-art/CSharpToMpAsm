using System.Collections.Generic;
using ICSharpCode.NRefactory.CSharp;
using CSharpToMpAsm.Compiler.Codes;

namespace CSharpToMpAsm.Compiler
{
    internal class BodyContext
    {
        private readonly TypeDefinition _definition;
        private readonly CompilationContext _context;
        private readonly Dictionary<string, IValueDestination> _destinations = new Dictionary<string, IValueDestination>();


        public BodyContext(TypeDefinition definition, CompilationContext context)
        {
            _definition = definition;
            _context = context;
        }

        public void AddDestination(IValueDestination variable)
        {
            _destinations.Add(variable.Name, variable);
        }

        public void AddParameters(IEnumerable<IValueDestination> parameters)
        {
            foreach (var parameter in parameters)
            {
                AddDestination(parameter);
            }
        }

        public IValueDestination Resolve(string identifier)
        {
            IValueDestination dest;
            if (_destinations.TryGetValue(identifier, out dest))
            {
                return dest;
            }
            return _definition.Resolve(identifier);
        }

        public MethodDefinition ResolveMethod(string identifier, ICode[] args)
        {
            //todo: use args type information to resolve method.
            return _definition.ResolveMethod(identifier);
        }

        private readonly Dictionary<string, LabelCode> _labels = new Dictionary<string, LabelCode>();

        public LabelCode ResolveLabel(string labelName)
        {
            LabelCode result;
            if (!_labels.TryGetValue(labelName, out result))
            {
                result = new LabelCode(labelName);
                _labels.Add(labelName, result);
            }
            return result;
        }
    }
}