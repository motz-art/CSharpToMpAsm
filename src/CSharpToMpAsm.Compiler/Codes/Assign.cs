using System;
using System.Text;

namespace CSharpToMpAsm.Compiler.Codes
{
    internal class Assign : ICode
    {
        public IValueDestination Destination { get; set; }
        public ICode Code { get; set; }

        public Assign(IValueDestination destination, ICode code)
        {
            Destination = destination;
            Code = code;
        }

        public TypeDefinition ResultType
        {
            get { throw new NotImplementedException(); }
        }

        public ResultLocation Location
        {
            get { throw new NotImplementedException(); }
        }

        private static TypeDefinition Dereference(TypeDefinition type)
        {
            if (!type.IsReference) return type;
            if (type.TypeParameters == null || type.TypeParameters.Count != 1)
            {
                throw new InvalidOperationException("Invalid reference type.");
            }
            return type.TypeParameters[0];
        }

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
            var destinationType = Dereference(Destination.Type);
            var codeType = Dereference(Code.ResultType);

            if (destinationType != codeType)
            {
                throw new InvalidOperationException("DestinationType do not match expression type.");
            }
            
            Code.WriteMpAsm(writer, memManager);

            writer.Comment(string.Format("; Assign {0} at {1} to {2} ({3})",
                    Code.ResultType,
                    Code.Location,
                    Destination.Location, Destination.Name));

            if (destinationType == TypeDefinitions.Byte)
            {
                if (!Code.Location.IsWorkRegister)
                {
                    writer.MoveFileToW(Code.Location);
                }
                if (!Destination.Location.IsWorkRegister)
                {
                    writer.MoveWToFile(Destination.Location);
                }
                return;
            }
            if (destinationType == TypeDefinitions.Int32)
            {
                for (int i = 0; i < 4; i++)
                {
                    writer.MoveFileToW(Code.Location + i);
                    writer.MoveWToFile(Destination.Location + i);
                }
                return;
            }
            throw new NotImplementedException();
        }
    }
}