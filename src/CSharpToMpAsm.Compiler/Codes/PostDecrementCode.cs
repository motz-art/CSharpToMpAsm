using System;
using System.Runtime.InteropServices;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class PostDecrementCode : ICode
    {
        private readonly IValueDestination _destination;

        public PostDecrementCode(IValueDestination destination)
        {
            _destination = destination;
        }

        public bool Equals(ICode other)
        {
            throw new NotImplementedException();
        }

        public TypeDefinition ResultType
        {
            get { return _destination.Type; }
        }

        public ResultLocation Location { get; private set; }

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
            Location = memManager.Alloc(ResultType.Size);
            writer.Copy(_destination.Location, Location, ResultType.Size);

            if (ResultType == TypeDefinitions.Byte)
            {
                writer.DecrementFile(_destination.Location);
            }

        }
    }
}