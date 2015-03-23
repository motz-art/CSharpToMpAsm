using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class PostIncrementCode : ICode
    {
        public IValueDestination Destination { get; private set; }

        public PostIncrementCode(IValueDestination destination)
        {
            Destination = destination;
        }

        public PostIncrementCode(IValueDestination destination, ResultLocation location)
            : this(destination)
        {
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
                writer.IncrementFile(Destination.Location);
                return;
            }
            throw new NotImplementedException();
        }
    }
}