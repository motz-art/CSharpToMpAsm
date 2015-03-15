using System;
using System.Collections.Generic;
using System.Linq;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.PatternMatching;
using CSharpToMpAsm.Compiler.Codes;
using Attribute = ICSharpCode.NRefactory.CSharp.Attribute;

namespace CSharpToMpAsm.Compiler
{
    internal class BodyGenerator : IAstVisitor<BodyContext, ICode>
    {
        private readonly CompilationContext _compilationContext;
        private TypeDefinition _variableType;

        public BodyGenerator(CompilationContext compilationContext)
        {
            _compilationContext = compilationContext;
        }
        
        public ICode VisitAnonymousMethodExpression(AnonymousMethodExpression anonymousMethodExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitUndocumentedExpression(UndocumentedExpression undocumentedExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitArrayCreateExpression(ArrayCreateExpression arrayCreateExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitArrayInitializerExpression(ArrayInitializerExpression arrayInitializerExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitAsExpression(AsExpression asExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitAssignmentExpression(AssignmentExpression assignmentExpression, BodyContext data)
        {
            var value = assignmentExpression.Right.AcceptVisitor(this, data);

            var identifier = assignmentExpression.Left as IdentifierExpression;
            if (identifier == null) throw new NotImplementedException();

            var destination = data.Resolve(identifier.Identifier);
            return new Assign(destination, value);
        }
        
        public ICode VisitBaseReferenceExpression(BaseReferenceExpression baseReferenceExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitBinaryOperatorExpression(BinaryOperatorExpression binaryOperatorExpression, BodyContext data)
        {
            var left = binaryOperatorExpression.Left.AcceptVisitor(this, data);
            if (left == null) throw new InvalidOperationException("Code should not be null for left expression.");
            var right = binaryOperatorExpression.Right.AcceptVisitor(this, data);
            if (right == null) throw new InvalidOperationException("Code should not be null for right expression.");
            
            switch (binaryOperatorExpression.Operator)
            {
                    case BinaryOperatorType.Add:
                    return CreateAddOperator(left, right);
                    case BinaryOperatorType.BitwiseAnd:
                    return new BitwiseAnd(left, right);
                    case BinaryOperatorType.BitwiseOr:
                    return new BitwiseOr(left, right);
                    case BinaryOperatorType.ShiftLeft:
                    return new ShiftLeft(left, right);
                    case BinaryOperatorType.ShiftRight:
                    return new ShiftRight(left, right);
            }
            throw new NotImplementedException();

        }

        private ICode CreateAddOperator(ICode left, ICode right)
        {
            if (left.ResultType.IsNumeric() && right.ResultType.IsNumeric())
            {
                return new AddInts(left, right);
            }
            throw new NotSupportedException();
        }

        public ICode VisitCastExpression(CastExpression castExpression, BodyContext data)
        {
            var type = _compilationContext.ResolveType(castExpression.Type);
            var code = castExpression.Expression.AcceptVisitor(this, data);
            return new CastCode(type, code);
        }

        public ICode VisitCheckedExpression(CheckedExpression checkedExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitConditionalExpression(ConditionalExpression conditionalExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitDefaultValueExpression(DefaultValueExpression defaultValueExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitDirectionExpression(DirectionExpression directionExpression, BodyContext data)
        {
            if (directionExpression.FieldDirection == FieldDirection.None)
                return directionExpression.AcceptVisitor(this, data);

            if (directionExpression.FieldDirection == FieldDirection.Ref ||
                directionExpression.FieldDirection == FieldDirection.Out)
            {
                var code = directionExpression.Expression.AcceptVisitor(this, data);
                if (code.ResultType.IsReference) return code;
                var getValue = code as GetValue;
                if (getValue==null)throw new InvalidOperationException("It is expected that code should be GetValue.");
                return new GetReference(getValue.Variable);
            }

            throw new NotImplementedException();
        }

        public ICode VisitIdentifierExpression(IdentifierExpression identifierExpression, BodyContext data)
        {
            return data.Resolve(identifierExpression.Identifier).GetValue;
        }

        public ICode VisitIndexerExpression(IndexerExpression indexerExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitInvocationExpression(InvocationExpression invocationExpression, BodyContext data)
        {
            var args = invocationExpression.Arguments.Select(x => x.AcceptVisitor(this, data)).ToArray();
            var expr = invocationExpression.Target as IdentifierExpression;
            if (expr == null)
            {
                throw new NotImplementedException();
            }
            var method = data.ResolveMethod(expr.Identifier, args);
            return method.GenerateIl(args);
        }

        public ICode VisitIsExpression(IsExpression isExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitLambdaExpression(LambdaExpression lambdaExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitMemberReferenceExpression(MemberReferenceExpression memberReferenceExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitNamedArgumentExpression(NamedArgumentExpression namedArgumentExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitNamedExpression(NamedExpression namedExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitNullReferenceExpression(NullReferenceExpression nullReferenceExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitObjectCreateExpression(ObjectCreateExpression objectCreateExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitAnonymousTypeCreateExpression(AnonymousTypeCreateExpression anonymousTypeCreateExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitParenthesizedExpression(ParenthesizedExpression parenthesizedExpression, BodyContext data)
        {
            return parenthesizedExpression.Expression.AcceptVisitor(this, data);
        }

        public ICode VisitPointerReferenceExpression(PointerReferenceExpression pointerReferenceExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitPrimitiveExpression(PrimitiveExpression primitiveExpression, BodyContext data)
        {
            if (primitiveExpression.Value is int)
            {
                return new IntValue((int)primitiveExpression.Value);
            }
            throw new NotImplementedException();
        }

        public ICode VisitSizeOfExpression(SizeOfExpression sizeOfExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitStackAllocExpression(StackAllocExpression stackAllocExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitThisReferenceExpression(ThisReferenceExpression thisReferenceExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitTypeOfExpression(TypeOfExpression typeOfExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitTypeReferenceExpression(TypeReferenceExpression typeReferenceExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitUnaryOperatorExpression(UnaryOperatorExpression unaryOperatorExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitUncheckedExpression(UncheckedExpression uncheckedExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitQueryExpression(QueryExpression queryExpression, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitQueryContinuationClause(QueryContinuationClause queryContinuationClause, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitQueryFromClause(QueryFromClause queryFromClause, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitQueryLetClause(QueryLetClause queryLetClause, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitQueryWhereClause(QueryWhereClause queryWhereClause, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitQueryJoinClause(QueryJoinClause queryJoinClause, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitQueryOrderClause(QueryOrderClause queryOrderClause, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitQueryOrdering(QueryOrdering queryOrdering, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitQuerySelectClause(QuerySelectClause querySelectClause, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitQueryGroupClause(QueryGroupClause queryGroupClause, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitAttribute(Attribute attribute, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitAttributeSection(AttributeSection attributeSection, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitDelegateDeclaration(DelegateDeclaration delegateDeclaration, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitNamespaceDeclaration(NamespaceDeclaration namespaceDeclaration, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitTypeDeclaration(TypeDeclaration typeDeclaration, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitUsingAliasDeclaration(UsingAliasDeclaration usingAliasDeclaration, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitUsingDeclaration(UsingDeclaration usingDeclaration, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitExternAliasDeclaration(ExternAliasDeclaration externAliasDeclaration, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitBlockStatement(BlockStatement blockStatement, BodyContext data)
        {
            var codes = blockStatement.Statements.Select(statement => statement.AcceptVisitor(this, data)).ToArray();
            return CreateBlockCode(codes);
        }

        private static ICode CreateBlockCode(IEnumerable<ICode> codes)
        {
            var codesList = codes.ToList();
            
            if (codesList.Any(code=>code == null)) 
                throw new InvalidOperationException("Code is null.");

            if (codesList.Count == 0) return new NullCode();
            if (codesList.Count == 1) return codesList[0];
            return new BlockCode(codesList.ToArray());
        }

        public ICode VisitBreakStatement(BreakStatement breakStatement, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitCheckedStatement(CheckedStatement checkedStatement, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitContinueStatement(ContinueStatement continueStatement, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitDoWhileStatement(DoWhileStatement doWhileStatement, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitEmptyStatement(EmptyStatement emptyStatement, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitExpressionStatement(ExpressionStatement expressionStatement, BodyContext data)
        {
            return expressionStatement.Expression.AcceptVisitor(this, data);
        }

        public ICode VisitFixedStatement(FixedStatement fixedStatement, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitForeachStatement(ForeachStatement foreachStatement, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitForStatement(ForStatement forStatement, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitGotoCaseStatement(GotoCaseStatement gotoCaseStatement, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitGotoDefaultStatement(GotoDefaultStatement gotoDefaultStatement, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitGotoStatement(GotoStatement gotoStatement, BodyContext data)
        {
            var label = data.ResolveLabel(gotoStatement.Label);
            return new GotoCode(label);
        }

        public ICode VisitIfElseStatement(IfElseStatement ifElseStatement, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitLabelStatement(LabelStatement labelStatement, BodyContext data)
        {
            return new LabelCode(labelStatement.Label);
        }

        public ICode VisitLockStatement(LockStatement lockStatement, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitReturnStatement(ReturnStatement returnStatement, BodyContext data)
        {
            var value = returnStatement.Expression.AcceptVisitor(this, data);
            return new ReturnCode(data.CurrentMethod, value);
        }

        public ICode VisitSwitchStatement(SwitchStatement switchStatement, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitSwitchSection(SwitchSection switchSection, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitCaseLabel(CaseLabel caseLabel, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitThrowStatement(ThrowStatement throwStatement, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitTryCatchStatement(TryCatchStatement tryCatchStatement, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitCatchClause(CatchClause catchClause, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitUncheckedStatement(UncheckedStatement uncheckedStatement, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitUnsafeStatement(UnsafeStatement unsafeStatement, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitUsingStatement(UsingStatement usingStatement, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitVariableDeclarationStatement(VariableDeclarationStatement variableDeclarationStatement, BodyContext data)
        {
            var simpleType = variableDeclarationStatement.Type as SimpleType;
            if (simpleType!=null && simpleType.Identifier == "var")
            {
                _variableType = null;
            }
            else
            {
                _variableType = _compilationContext.ResolveType(variableDeclarationStatement.Type);
            }

            var codes = variableDeclarationStatement.Variables.Select(
                variableInitializer => variableInitializer.AcceptVisitor(this, data));
            
            return CreateBlockCode(codes);
        }

        public ICode VisitWhileStatement(WhileStatement whileStatement, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitYieldBreakStatement(YieldBreakStatement yieldBreakStatement, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitYieldReturnStatement(YieldReturnStatement yieldReturnStatement, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitAccessor(Accessor accessor, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitConstructorDeclaration(ConstructorDeclaration constructorDeclaration, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitConstructorInitializer(ConstructorInitializer constructorInitializer, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitDestructorDeclaration(DestructorDeclaration destructorDeclaration, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitEnumMemberDeclaration(EnumMemberDeclaration enumMemberDeclaration, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitEventDeclaration(EventDeclaration eventDeclaration, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitCustomEventDeclaration(CustomEventDeclaration customEventDeclaration, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitFieldDeclaration(FieldDeclaration fieldDeclaration, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitIndexerDeclaration(IndexerDeclaration indexerDeclaration, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitMethodDeclaration(MethodDeclaration methodDeclaration, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitOperatorDeclaration(OperatorDeclaration operatorDeclaration, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitParameterDeclaration(ParameterDeclaration parameterDeclaration, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitPropertyDeclaration(PropertyDeclaration propertyDeclaration, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitVariableInitializer(VariableInitializer variableInitializer, BodyContext data)
        {
            ICode code = null;

            if (variableInitializer.Initializer != null)
            {
                code = variableInitializer.Initializer.AcceptVisitor(this, data);
            }

            var type = _variableType;
            if (type == null)
            {
                if (code == null)
                    throw new InvalidOperationException("Can't determine variable type.");

                type = code.ResultType;
            }

            if (type == TypeDefinitions.Void)
                throw new InvalidOperationException("Can't create variable of void type.");

            var variable = new Variable(variableInitializer.Name, type, _compilationContext.MemAllocate(type.Size));
            data.AddDestination(variable);

            if (code!=null)
                return new Assign(variable, code);

            return null;
        }

        public ICode VisitFixedFieldDeclaration(FixedFieldDeclaration fixedFieldDeclaration, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitFixedVariableInitializer(FixedVariableInitializer fixedVariableInitializer, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitSyntaxTree(SyntaxTree syntaxTree, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitSimpleType(SimpleType simpleType, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitMemberType(MemberType memberType, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitComposedType(ComposedType composedType, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitArraySpecifier(ArraySpecifier arraySpecifier, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitPrimitiveType(PrimitiveType primitiveType, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitComment(Comment comment, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitNewLine(NewLineNode newLineNode, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitWhitespace(WhitespaceNode whitespaceNode, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitText(TextNode textNode, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitPreProcessorDirective(PreProcessorDirective preProcessorDirective, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitDocumentationReference(DocumentationReference documentationReference, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitTypeParameterDeclaration(TypeParameterDeclaration typeParameterDeclaration, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitConstraint(Constraint constraint, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitCSharpTokenNode(CSharpTokenNode cSharpTokenNode, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitIdentifier(Identifier identifier, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitNullNode(AstNode nullNode, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitErrorNode(AstNode errorNode, BodyContext data)
        {
            throw new NotImplementedException();
        }

        public ICode VisitPatternPlaceholder(AstNode placeholder, Pattern pattern, BodyContext data)
        {
            throw new NotImplementedException();
        }
    }
}