using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CSharpToMpAsm.Compiler.Codes
{
    public static class CodeOptimizationExtentions
    {
        public static ICode Optimize(this ICode code)
        {
            var castOptimisation = new CastCodeOptimisationVisitor();
            code = castOptimisation.Visit(code);
            code = new SwapfOptimisationVisitor().Visit(code);
            return code;
        }
    }
}