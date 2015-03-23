using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class InlineMethodVisitor : CodeOptimisationVisitor
    {
        private MethodDefinition _method;

        public InlineMethodVisitor(MethodDefinition method)
        {
            _method = method;
        }

        protected override ICode Optimize(Assign assign)
        {
            ICode value;
            var code = TryOptimize(assign.Code, out value);
            if (ReferenceEquals(code, assign.Code))
                return base.Optimize(assign);

            if (value == null) 
                throw new InvalidOperationException(
                    string.Format("Can't assign void to a desstination {0} of type {1}.", 
                        assign.Destination.Name, 
                        assign.Destination.Type));

            var newCode = new BlockCode(new []{code, new Assign(assign.Destination, value), });
            return Visit(newCode);
        }

        protected override ICode Optimize(Call call)
        {
            if (call.Method.ShouldInline)
            {
                ICode value;
                var newCode = TryOptimize(call, out value);
                if (value!=null)
                    throw new InvalidOperationException("Warning! Check parent code.");

                return Visit(newCode);
            }
            return base.Optimize(call);
        }

        private ICode TryOptimize(ICode code, out ICode methodResult)
        {
            methodResult = null;
            var call = code as Call;
            if (call == null || !call.Method.ShouldInline) return code;
            var method = call.Method;

            var destinations =
                method.Parameters.Select(x => new Variable(x.Name, x.Type, x.Location)).ToDictionary(x => x.Name);

            Variable result=null;
            if (method.ReturnType != TypeDefinitions.Void)
            {
                result = new Variable(method.Name+"Result",method.ReturnType);
                methodResult = new GetValue(result);
            } 
            
            var translated = new MethodBodyVisitor(destinations, result).Visit(method.Body);
            var codes = new List<ICode>();
            for (int i = 0; i < method.Parameters.Length; i++)
            {
                codes.Add(new Assign(destinations[method.Parameters[i].Name], call.Args[i]));
            }
            codes.Add(translated);
            
            return new BlockCode(codes.ToArray());
        }

        private class MethodBodyVisitor : CodeOptimisationVisitor
        {
            private readonly Dictionary<string, Variable> _variables;
            private readonly Variable _result;

            public MethodBodyVisitor(Dictionary<string, Variable> variables, Variable result)
            {
                _variables = variables;
                _result = result;
            }

            protected override ICode Optimize(ReturnCode returnCode)
            {
                if (_result!=null)
                    return new Assign(_result, returnCode.Value);
                return returnCode;
            }

            protected override ICode Optimize(Assign assign)
            {
                Variable variable;
                if (_variables.TryGetValue(assign.Destination.Name, out variable))
                {
                    return new Assign(variable, Visit(assign.Code));
                }
                return base.Optimize(assign);
            }

            protected override ICode Optimize(GetValue getValue)
            {
                Variable variable;
                if (_variables.TryGetValue(getValue.Variable.Name, out variable))
                {
                    return new GetValue(variable, getValue.ResultType);
                }
                return base.Optimize(getValue);
            }
        }
    }
}