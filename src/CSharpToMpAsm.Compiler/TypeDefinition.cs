using System;
using System.Collections.Generic;
using System.Linq;
using ICSharpCode.NRefactory.CSharp;
using CSharpToMpAsm.Compiler.Codes;

namespace CSharpToMpAsm.Compiler
{
    public class TypeDefinition
    {
        public string NameSpace { get; set; }
        public List<string> NameSpaces { get; set; }
        private readonly Dictionary<string, List<MethodDefinition>> _methods = new Dictionary<string, List<MethodDefinition>>();
        private readonly Dictionary<string, IValueDestination> _destinations = new Dictionary<string, IValueDestination>();

        public bool IsAbstract { get; set; }

        public string Name { get; set; }

        public List<TypeDefinition> BaseTypes { get; private set; }

        public IEnumerable<MethodDefinition> Methods
        {
            get
            {
                return BaseTypes.SelectMany(typeDefinition => typeDefinition.Methods)
                    .Union(_methods.SelectMany(method => method.Value));
            }
        }

        public int Size { get; set; }

        public TypeDefinition()
        {

        }
        public TypeDefinition(TypeDeclaration node, string nameSpace, List<string> nameSpaces)
        {
            IsAbstract = (node.Modifiers & Modifiers.Abstract) == Modifiers.Abstract;
            NameSpace = nameSpace;
            NameSpaces = nameSpaces;
            Name = node.Name;
            BaseTypes = new List<TypeDefinition>();
        }

        public MethodDefinition ResolveMethod(string identifier)
        {
            var result = TryResolveMethod(identifier);
            if (result == null)
                throw new InvalidOperationException(string.Format("Can't resolve method {0}.", identifier));
            return result;
        }

        public MethodDefinition TryResolveMethod(string identifier)
        {
            List<MethodDefinition> candidates;
            if (_methods.TryGetValue(identifier, out candidates))
            {
                return candidates.Single();
            }
            return BaseTypes.Select(x => x.TryResolveMethod(identifier)).Where(x => x != null).SingleOrDefault();
        }

        public void AddBase(TypeDefinition definition)
        {
            BaseTypes.Add(definition);
        }

        public void AddDestination(IValueDestination destination)
        {
            _destinations.Add(destination.Name, destination);
        }


        public IValueDestination Resolve(string identifier)
        {
            var result = TryResolve(identifier);
            if (result == null)
                throw new InvalidOperationException(string.Format("Can't resolve identifier {0}.", identifier));
            return result;
        }

        public IValueDestination TryResolve(string identifier)
        {
            IValueDestination dest;
            if (_destinations.TryGetValue(identifier, out dest))
            {
                return dest;
            }
            return BaseTypes.Select(x => x.TryResolve(identifier)).Where(x => x != null).SingleOrDefault();
        }

        public void AddMethod(string name, MethodDefinition definition)
        {
            List<MethodDefinition> methods;
            if (!_methods.TryGetValue(name, out methods))
            {
                methods = new List<MethodDefinition>();
                _methods.Add(name, methods);
            }
            methods.Add(definition);
        }

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}