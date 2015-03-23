using CSharpToMpAsm.Compiler.Codes;

namespace CSharpToMpAsm.Compiler
{
    public interface IValueDestination
    {
        string Name { get; }
        ICode GetValue { get; }
        TypeDefinition Type { get; }
        ResultLocation Location { get; set; }
    }
}