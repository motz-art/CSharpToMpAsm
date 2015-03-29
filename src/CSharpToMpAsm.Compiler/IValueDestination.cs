using CSharpToMpAsm.Compiler.Codes;

namespace CSharpToMpAsm.Compiler
{
    public interface IValueDestination
    {
        string Name { get; }
        ICode CreateGetValueCode();
        TypeDefinition Type { get; }
        ResultLocation Location { get; set; }
    }
}