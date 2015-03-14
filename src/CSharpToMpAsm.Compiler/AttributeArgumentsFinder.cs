using System;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.PatternMatching;
using Attribute = ICSharpCode.NRefactory.CSharp.Attribute;

namespace CSharpToMpAsm.Compiler
{
    internal class AttributeArgumentsFinder : IAstVisitor<AttributeArguments>
    {
        public AttributeArguments VisitAnonymousMethodExpression(AnonymousMethodExpression anonymousMethodExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitUndocumentedExpression(UndocumentedExpression undocumentedExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitArrayCreateExpression(ArrayCreateExpression arrayCreateExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitArrayInitializerExpression(ArrayInitializerExpression arrayInitializerExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitAsExpression(AsExpression asExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitAssignmentExpression(AssignmentExpression assignmentExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitBaseReferenceExpression(BaseReferenceExpression baseReferenceExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitBinaryOperatorExpression(BinaryOperatorExpression binaryOperatorExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitCastExpression(CastExpression castExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitCheckedExpression(CheckedExpression checkedExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitConditionalExpression(ConditionalExpression conditionalExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitDefaultValueExpression(DefaultValueExpression defaultValueExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitDirectionExpression(DirectionExpression directionExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitIdentifierExpression(IdentifierExpression identifierExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitIndexerExpression(IndexerExpression indexerExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitInvocationExpression(InvocationExpression invocationExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitIsExpression(IsExpression isExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitLambdaExpression(LambdaExpression lambdaExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitMemberReferenceExpression(MemberReferenceExpression memberReferenceExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitNamedArgumentExpression(NamedArgumentExpression namedArgumentExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitNamedExpression(NamedExpression namedExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitNullReferenceExpression(NullReferenceExpression nullReferenceExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitObjectCreateExpression(ObjectCreateExpression objectCreateExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitAnonymousTypeCreateExpression(AnonymousTypeCreateExpression anonymousTypeCreateExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitParenthesizedExpression(ParenthesizedExpression parenthesizedExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitPointerReferenceExpression(PointerReferenceExpression pointerReferenceExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitPrimitiveExpression(PrimitiveExpression primitiveExpression)
        {
            return new AttributeArguments(null, primitiveExpression.Value);
        }

        public AttributeArguments VisitSizeOfExpression(SizeOfExpression sizeOfExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitStackAllocExpression(StackAllocExpression stackAllocExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitThisReferenceExpression(ThisReferenceExpression thisReferenceExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitTypeOfExpression(TypeOfExpression typeOfExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitTypeReferenceExpression(TypeReferenceExpression typeReferenceExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitUnaryOperatorExpression(UnaryOperatorExpression unaryOperatorExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitUncheckedExpression(UncheckedExpression uncheckedExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitQueryExpression(QueryExpression queryExpression)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitQueryContinuationClause(QueryContinuationClause queryContinuationClause)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitQueryFromClause(QueryFromClause queryFromClause)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitQueryLetClause(QueryLetClause queryLetClause)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitQueryWhereClause(QueryWhereClause queryWhereClause)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitQueryJoinClause(QueryJoinClause queryJoinClause)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitQueryOrderClause(QueryOrderClause queryOrderClause)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitQueryOrdering(QueryOrdering queryOrdering)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitQuerySelectClause(QuerySelectClause querySelectClause)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitQueryGroupClause(QueryGroupClause queryGroupClause)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitAttribute(Attribute attribute)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitAttributeSection(AttributeSection attributeSection)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitDelegateDeclaration(DelegateDeclaration delegateDeclaration)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitNamespaceDeclaration(NamespaceDeclaration namespaceDeclaration)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitTypeDeclaration(TypeDeclaration typeDeclaration)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitUsingAliasDeclaration(UsingAliasDeclaration usingAliasDeclaration)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitUsingDeclaration(UsingDeclaration usingDeclaration)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitExternAliasDeclaration(ExternAliasDeclaration externAliasDeclaration)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitBlockStatement(BlockStatement blockStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitBreakStatement(BreakStatement breakStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitCheckedStatement(CheckedStatement checkedStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitContinueStatement(ContinueStatement continueStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitDoWhileStatement(DoWhileStatement doWhileStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitEmptyStatement(EmptyStatement emptyStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitExpressionStatement(ExpressionStatement expressionStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitFixedStatement(FixedStatement fixedStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitForeachStatement(ForeachStatement foreachStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitForStatement(ForStatement forStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitGotoCaseStatement(GotoCaseStatement gotoCaseStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitGotoDefaultStatement(GotoDefaultStatement gotoDefaultStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitGotoStatement(GotoStatement gotoStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitIfElseStatement(IfElseStatement ifElseStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitLabelStatement(LabelStatement labelStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitLockStatement(LockStatement lockStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitReturnStatement(ReturnStatement returnStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitSwitchStatement(SwitchStatement switchStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitSwitchSection(SwitchSection switchSection)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitCaseLabel(CaseLabel caseLabel)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitThrowStatement(ThrowStatement throwStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitTryCatchStatement(TryCatchStatement tryCatchStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitCatchClause(CatchClause catchClause)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitUncheckedStatement(UncheckedStatement uncheckedStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitUnsafeStatement(UnsafeStatement unsafeStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitUsingStatement(UsingStatement usingStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitVariableDeclarationStatement(VariableDeclarationStatement variableDeclarationStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitWhileStatement(WhileStatement whileStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitYieldBreakStatement(YieldBreakStatement yieldBreakStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitYieldReturnStatement(YieldReturnStatement yieldReturnStatement)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitAccessor(Accessor accessor)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitConstructorDeclaration(ConstructorDeclaration constructorDeclaration)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitConstructorInitializer(ConstructorInitializer constructorInitializer)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitDestructorDeclaration(DestructorDeclaration destructorDeclaration)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitEnumMemberDeclaration(EnumMemberDeclaration enumMemberDeclaration)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitEventDeclaration(EventDeclaration eventDeclaration)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitCustomEventDeclaration(CustomEventDeclaration customEventDeclaration)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitFieldDeclaration(FieldDeclaration fieldDeclaration)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitIndexerDeclaration(IndexerDeclaration indexerDeclaration)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitMethodDeclaration(MethodDeclaration methodDeclaration)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitOperatorDeclaration(OperatorDeclaration operatorDeclaration)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitParameterDeclaration(ParameterDeclaration parameterDeclaration)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitPropertyDeclaration(PropertyDeclaration propertyDeclaration)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitVariableInitializer(VariableInitializer variableInitializer)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitFixedFieldDeclaration(FixedFieldDeclaration fixedFieldDeclaration)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitFixedVariableInitializer(FixedVariableInitializer fixedVariableInitializer)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitSyntaxTree(SyntaxTree syntaxTree)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitSimpleType(SimpleType simpleType)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitMemberType(MemberType memberType)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitComposedType(ComposedType composedType)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitArraySpecifier(ArraySpecifier arraySpecifier)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitPrimitiveType(PrimitiveType primitiveType)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitWhitespace(WhitespaceNode whitespaceNode)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitText(TextNode textNode)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitNewLine(NewLineNode newLineNode)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitPreProcessorDirective(PreProcessorDirective preProcessorDirective)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitDocumentationReference(DocumentationReference documentationReference)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitTypeParameterDeclaration(TypeParameterDeclaration typeParameterDeclaration)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitConstraint(Constraint constraint)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitCSharpTokenNode(CSharpTokenNode cSharpTokenNode)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitIdentifier(Identifier identifier)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitNullNode(AstNode nullNode)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitErrorNode(AstNode errorNode)
        {
            throw new NotImplementedException();
        }

        public AttributeArguments VisitPatternPlaceholder(AstNode placeholder, Pattern pattern)
        {
            throw new NotImplementedException();
        }
    }
}