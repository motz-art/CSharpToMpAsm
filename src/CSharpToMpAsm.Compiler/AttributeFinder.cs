using System;
using System.Collections.Generic;
using System.Linq;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.PatternMatching;
using Attribute = ICSharpCode.NRefactory.CSharp.Attribute;

namespace CSharpToMpAsm.Compiler
{
    internal class AttributeFinder : IAstVisitor<IEnumerable<AttributeInfo>>
    {
        public IEnumerable<AttributeInfo> VisitAnonymousMethodExpression(AnonymousMethodExpression anonymousMethodExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitUndocumentedExpression(UndocumentedExpression undocumentedExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitArrayCreateExpression(ArrayCreateExpression arrayCreateExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitArrayInitializerExpression(ArrayInitializerExpression arrayInitializerExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitAsExpression(AsExpression asExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitAssignmentExpression(AssignmentExpression assignmentExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitBaseReferenceExpression(BaseReferenceExpression baseReferenceExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitBinaryOperatorExpression(BinaryOperatorExpression binaryOperatorExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitCastExpression(CastExpression castExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitCheckedExpression(CheckedExpression checkedExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitConditionalExpression(ConditionalExpression conditionalExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitDefaultValueExpression(DefaultValueExpression defaultValueExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitDirectionExpression(DirectionExpression directionExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitIdentifierExpression(IdentifierExpression identifierExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitIndexerExpression(IndexerExpression indexerExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitInvocationExpression(InvocationExpression invocationExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitIsExpression(IsExpression isExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitLambdaExpression(LambdaExpression lambdaExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitMemberReferenceExpression(MemberReferenceExpression memberReferenceExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitNamedArgumentExpression(NamedArgumentExpression namedArgumentExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitNamedExpression(NamedExpression namedExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitNullReferenceExpression(NullReferenceExpression nullReferenceExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitObjectCreateExpression(ObjectCreateExpression objectCreateExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitAnonymousTypeCreateExpression(AnonymousTypeCreateExpression anonymousTypeCreateExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitParenthesizedExpression(ParenthesizedExpression parenthesizedExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitPointerReferenceExpression(PointerReferenceExpression pointerReferenceExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitPrimitiveExpression(PrimitiveExpression primitiveExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitSizeOfExpression(SizeOfExpression sizeOfExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitStackAllocExpression(StackAllocExpression stackAllocExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitThisReferenceExpression(ThisReferenceExpression thisReferenceExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitTypeOfExpression(TypeOfExpression typeOfExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitTypeReferenceExpression(TypeReferenceExpression typeReferenceExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitUnaryOperatorExpression(UnaryOperatorExpression unaryOperatorExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitUncheckedExpression(UncheckedExpression uncheckedExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitQueryExpression(QueryExpression queryExpression)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitQueryContinuationClause(QueryContinuationClause queryContinuationClause)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitQueryFromClause(QueryFromClause queryFromClause)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitQueryLetClause(QueryLetClause queryLetClause)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitQueryWhereClause(QueryWhereClause queryWhereClause)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitQueryJoinClause(QueryJoinClause queryJoinClause)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitQueryOrderClause(QueryOrderClause queryOrderClause)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitQueryOrdering(QueryOrdering queryOrdering)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitQuerySelectClause(QuerySelectClause querySelectClause)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitQueryGroupClause(QueryGroupClause queryGroupClause)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitAttribute(Attribute attribute)
        {
            var arguments = attribute.Arguments.Select(x => x.AcceptVisitor(new AttributeArgumentsFinder())).ToArray();

            var typeName = attribute.Type.ResolveTypeName();

            yield return new AttributeInfo(typeName, arguments);
        }

        public IEnumerable<AttributeInfo> VisitAttributeSection(AttributeSection attributeSection)
        {
            return this.VisitChildren(attributeSection);
        }

        public IEnumerable<AttributeInfo> VisitDelegateDeclaration(DelegateDeclaration delegateDeclaration)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitNamespaceDeclaration(NamespaceDeclaration namespaceDeclaration)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitTypeDeclaration(TypeDeclaration typeDeclaration)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitUsingAliasDeclaration(UsingAliasDeclaration usingAliasDeclaration)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitUsingDeclaration(UsingDeclaration usingDeclaration)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitExternAliasDeclaration(ExternAliasDeclaration externAliasDeclaration)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitBlockStatement(BlockStatement blockStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitBreakStatement(BreakStatement breakStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitCheckedStatement(CheckedStatement checkedStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitContinueStatement(ContinueStatement continueStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitDoWhileStatement(DoWhileStatement doWhileStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitEmptyStatement(EmptyStatement emptyStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitExpressionStatement(ExpressionStatement expressionStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitFixedStatement(FixedStatement fixedStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitForeachStatement(ForeachStatement foreachStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitForStatement(ForStatement forStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitGotoCaseStatement(GotoCaseStatement gotoCaseStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitGotoDefaultStatement(GotoDefaultStatement gotoDefaultStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitGotoStatement(GotoStatement gotoStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitIfElseStatement(IfElseStatement ifElseStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitLabelStatement(LabelStatement labelStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitLockStatement(LockStatement lockStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitReturnStatement(ReturnStatement returnStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitSwitchStatement(SwitchStatement switchStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitSwitchSection(SwitchSection switchSection)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitCaseLabel(CaseLabel caseLabel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitThrowStatement(ThrowStatement throwStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitTryCatchStatement(TryCatchStatement tryCatchStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitCatchClause(CatchClause catchClause)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitUncheckedStatement(UncheckedStatement uncheckedStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitUnsafeStatement(UnsafeStatement unsafeStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitUsingStatement(UsingStatement usingStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitVariableDeclarationStatement(VariableDeclarationStatement variableDeclarationStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitWhileStatement(WhileStatement whileStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitYieldBreakStatement(YieldBreakStatement yieldBreakStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitYieldReturnStatement(YieldReturnStatement yieldReturnStatement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitAccessor(Accessor accessor)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitConstructorDeclaration(ConstructorDeclaration constructorDeclaration)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitConstructorInitializer(ConstructorInitializer constructorInitializer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitDestructorDeclaration(DestructorDeclaration destructorDeclaration)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitEnumMemberDeclaration(EnumMemberDeclaration enumMemberDeclaration)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitEventDeclaration(EventDeclaration eventDeclaration)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitCustomEventDeclaration(CustomEventDeclaration customEventDeclaration)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitFieldDeclaration(FieldDeclaration fieldDeclaration)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitIndexerDeclaration(IndexerDeclaration indexerDeclaration)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitMethodDeclaration(MethodDeclaration methodDeclaration)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitOperatorDeclaration(OperatorDeclaration operatorDeclaration)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitParameterDeclaration(ParameterDeclaration parameterDeclaration)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitPropertyDeclaration(PropertyDeclaration propertyDeclaration)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitVariableInitializer(VariableInitializer variableInitializer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitFixedFieldDeclaration(FixedFieldDeclaration fixedFieldDeclaration)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitFixedVariableInitializer(FixedVariableInitializer fixedVariableInitializer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitSyntaxTree(SyntaxTree syntaxTree)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitSimpleType(SimpleType simpleType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitMemberType(MemberType memberType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitComposedType(ComposedType composedType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitArraySpecifier(ArraySpecifier arraySpecifier)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitPrimitiveType(PrimitiveType primitiveType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitWhitespace(WhitespaceNode whitespaceNode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitText(TextNode textNode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitNewLine(NewLineNode newLineNode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitPreProcessorDirective(PreProcessorDirective preProcessorDirective)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitDocumentationReference(DocumentationReference documentationReference)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitTypeParameterDeclaration(TypeParameterDeclaration typeParameterDeclaration)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitConstraint(Constraint constraint)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitCSharpTokenNode(CSharpTokenNode cSharpTokenNode)
        {
            return null;
        }

        public IEnumerable<AttributeInfo> VisitIdentifier(Identifier identifier)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitNullNode(AstNode nullNode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitErrorNode(AstNode errorNode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeInfo> VisitPatternPlaceholder(AstNode placeholder, Pattern pattern)
        {
            throw new NotImplementedException();
        }
    }
}