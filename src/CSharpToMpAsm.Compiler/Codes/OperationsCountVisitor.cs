using System.Linq;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class OperationsCountVisitor : CodeOptimisationVisitor
    {
        public int Count { get; private set; }
        public bool HasBranhes { get; set; }

        protected override ICode Optimize(BitwiseAnd bitwiseAnd)
        {
            Count++;
            return base.Optimize(bitwiseAnd);
        }

        protected override ICode Optimize(BitwiseOr bitwiseOr)
        {
            Count++;
            return base.Optimize(bitwiseOr);
        }

        protected override ICode Optimize(Call call)
        {
            Count++;
            if (call.ResultType != TypeDefinitions.Void) Count++;
            Count += call.Method.Parameters.Count(x => x.Type.IsReference);
            return base.Optimize(call);
        }

        protected override ICode Optimize(GotoCode gotoCode)
        {
            HasBranhes = true;
            return base.Optimize(gotoCode);
        }

        protected override ICode Optimize(IntValue intValue)
        {
            Count++;
            return base.Optimize(intValue);
        }

        protected override ICode Optimize(ShiftLeft shiftLeft)
        {
            Count++;
            return base.Optimize(shiftLeft);
        }

        protected override ICode Optimize(ShiftRight shiftRight)
        {
            Count++;
            return base.Optimize(shiftRight);
        }

    }
}