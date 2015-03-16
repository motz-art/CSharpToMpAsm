using System;
using System.Text;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class IntValue : ICode
    {
        private ResultLocation _address;
        private TypeDefinition _type;
        public int Value { get; private set; }

        public IntValue(int value)
            : this(value, TypeDefinitions.Int32)
        {
        }

        public IntValue(int value, TypeDefinition type)
        {
            Value = value;
            _type = type;
        }

        public TypeDefinition ResultType
        {
            get { return _type; }
        }

        public ResultLocation Location
        {
            get
            {
                if (_address == null)
                    throw new InvalidOperationException("GetMpAsm should be called first.");

                return _address;                
            }
        }

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
            if (_address == null)
            {
                _address = ResultType.Size==1 ? ResultLocation.WorkRegister : memManager.Alloc(ResultType.Size);
            }

            var bytes = BitConverter.GetBytes(Value);

            writer.Comment(string.Format("; IntLiteral {0}(0x{0:X8}) to {1}.", Value, _address));

            for (int i = 0; i < ResultType.Size; i++)
            {
                if (i >= bytes.Length)
                    throw new InvalidOperationException("Literal value is too small.");

                var b = bytes[i];
                writer.LoadLiteralToW(b);
                writer.MoveWToFile(_address + i);
            }
        }
    }
}