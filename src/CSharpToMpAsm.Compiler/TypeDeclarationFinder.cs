using System;
using System.Collections.Generic;
using ICSharpCode.NRefactory.CSharp;

namespace CSharpToMpAsm.Compiler
{
    internal class TypeDeclarationFinder : DepthFirstAstVisitor
    {
        private string _currentNameSpace = "";
        private readonly List<string> _nameSpaces = new List<string>();
        private readonly List<TypeAlias> _aliases = new List<TypeAlias>();
        private readonly Action<TypeDeclaration, string, IEnumerable<string>> _registerTypeDeclaration;

        public TypeDeclarationFinder(Action<TypeDeclaration, string, IEnumerable<string>> registerTypeDeclaration)
        {
            _registerTypeDeclaration = registerTypeDeclaration;
        }

        public string CurrentNameSpace
        {
            get {
                return _currentNameSpace;
            }
        }

        public List<string> Usings
        {
            get { return _nameSpaces; }
        }

        public override void VisitTypeDeclaration(TypeDeclaration typeDeclaration)
        {
            _registerTypeDeclaration(typeDeclaration, CurrentNameSpace, _nameSpaces);
            VisitChildren(typeDeclaration);
        }

        public override void VisitNamespaceDeclaration(NamespaceDeclaration namespaceDeclaration)
        {
            var previousNameSpace = _currentNameSpace;
            var aliasesCount = _aliases.Count;
            var nameSpacesCount = _nameSpaces.Count;

            _currentNameSpace = namespaceDeclaration.FullName;
            
            base.VisitNamespaceDeclaration(namespaceDeclaration);
            
            _currentNameSpace = previousNameSpace;
            
            if (aliasesCount < _aliases.Count)
                _aliases.RemoveRange(aliasesCount, _aliases.Count - aliasesCount);

            if (nameSpacesCount < _nameSpaces.Count)
                _nameSpaces.RemoveRange(nameSpacesCount, _nameSpaces.Count - nameSpacesCount);
        }

        public override void VisitUsingAliasDeclaration(UsingAliasDeclaration usingAliasDeclaration)
        {
            throw new NotSupportedException("Using alias declarations are not supported yet.");
        }

        public override void VisitUsingDeclaration(UsingDeclaration usingDeclaration)
        {
            _nameSpaces.Add(usingDeclaration.Namespace);

            base.VisitUsingDeclaration(usingDeclaration);
        }

        public override void VisitMethodDeclaration(MethodDeclaration methodDeclaration)
        {
            
        }

        public override void VisitOperatorDeclaration(OperatorDeclaration operatorDeclaration)
        {
            
        }

        public override void VisitPropertyDeclaration(PropertyDeclaration propertyDeclaration)
        {
            
        }
    }

    public class TypeAlias
    {
    }
}