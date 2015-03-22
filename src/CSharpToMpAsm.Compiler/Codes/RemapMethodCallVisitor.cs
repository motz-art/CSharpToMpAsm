using System;
using System.Linq;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class RemapMethodCallVisitor : CodeOptimisationVisitor
    {
        private readonly MethodDefinition _from;
        private readonly MethodDefinition _to;

        public RemapMethodCallVisitor(MethodDefinition from, MethodDefinition to)
        {
            if (@from == null) throw new ArgumentNullException("from");
            if (to == null) throw new ArgumentNullException("to");

            _from = @from;
            _to = to;
        }

        protected override ICode Optimize(Call call)
        {
            if (call.Method == _from)
            {
                return new Call(_to, call.Args.Select(x=>Visit(x)).ToArray());
            }
            return base.Optimize(call);
        }
    }
}