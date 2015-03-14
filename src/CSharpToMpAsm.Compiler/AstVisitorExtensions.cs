using System;
using System.Collections.Generic;
using ICSharpCode.NRefactory.CSharp;

namespace CSharpToMpAsm.Compiler
{
    public static class AstVisitorExtensions
    {
        public static string ResolveTypeName(this AstType type)
        {
            var simpleType = type as SimpleType;
            if (simpleType != null)
            {
                return simpleType.Identifier;
            }
            throw new NotImplementedException();
        }

        public static IEnumerable<T> VisitChildren<T>(this IAstVisitor<IEnumerable<T>> visitor, AstNode node)
        {
            AstNode next;
            for (var child = node.FirstChild; child != null; child = next)
            {
                next = child.NextSibling;
                var results = child.AcceptVisitor(visitor);
                if (results == null) continue;
                foreach (var typeDefinition in results)
                {
                    yield return typeDefinition;
                }
            }
        }

        public static void VisitChildren(this IAstVisitor visitor, AstNode node)
        {
            AstNode next;
            for (var child = node.FirstChild; child != null; child = next)
            {
                next = child.NextSibling;
                child.AcceptVisitor(visitor);
            }
        }

    }
}