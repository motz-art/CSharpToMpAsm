using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public interface ICode : IEquatable<ICode>
    {
        TypeDefinition ResultType { get; }
        ResultLocation Location { get; }
        void WriteMpAsm(IMpAsmWriter writer);
    }
}