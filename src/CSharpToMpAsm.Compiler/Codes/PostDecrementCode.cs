using System;
using System.Runtime.InteropServices;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class PostDecrementCode : ICode
    {
        public IValueDestination Destination { get; private set; }

        public PostDecrementCode(IValueDestination destination)
        {
            if (destination == null) throw new ArgumentNullException("destination");
            Destination = destination;
        }

        public PostDecrementCode(IValueDestination destination, ResultLocation location) : this(destination)
        {
            if (location == null) throw new ArgumentNullException("location");
            Location = location;
        }

        public bool Equals(ICode other)
        {
            throw new NotImplementedException();
        }

        public TypeDefinition ResultType
        {
            get { return Destination.Type; }
        }

        public ResultLocation Location { get; private set; }

        public void WriteMpAsm(IMpAsmWriter writer)
        {
            writer.Copy(Destination.Location, Location, ResultType.Size);

            if (ResultType == TypeDefinitions.Byte)
            {
                writer.DecrementFile(Destination.Location);
                return;
            }
            throw new NotImplementedException();
        }
    }
}