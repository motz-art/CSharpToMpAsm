namespace CSharpToMpAsm.Compiler.Codes
{
    public interface ICode
    {
        TypeDefinition ResultType { get; }
        ResultLocation Location { get; }
        string GetMpAsm(CompilationContext compilationContext);
        void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager);
    }
}