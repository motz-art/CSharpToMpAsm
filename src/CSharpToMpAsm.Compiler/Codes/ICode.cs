namespace CSharpToMpAsm.Compiler.Codes
{
    public interface ICode
    {
        TypeDefinition ResultType { get; }
        ResultLocation Location { get; }
        void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager);
    }
}