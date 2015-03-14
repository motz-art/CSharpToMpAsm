using CSharpToMpAsm.Compiler.Codes;

namespace CSharpToMpAsm.Compiler
{
    public interface IMemoryManager
    {
        ResultLocation Alloc(int size);
        void Dispose(ResultLocation location, int size);

        ResultLocation Alloc(IValueDestination destination);
        void DAlloc(IValueDestination destination);
    }
}