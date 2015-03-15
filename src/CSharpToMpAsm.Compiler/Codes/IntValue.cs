using System;
using System.Text;

namespace CSharpToMpAsm.Compiler.Codes
{
    internal class IntValue : ICode
    {
        private static ResultLocation _staticAddress;
        private ResultLocation _address;
        public int Value { get; private set; }

        public IntValue(int value)
        {
            Value = value;
        }

        public TypeDefinition ResultType
        {
            get { return TypeDefinitions.Int32; }
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
                if (_staticAddress == null)
                {
                    _staticAddress = memManager.Alloc(ResultType.Size);
                }
                _address = _staticAddress;
            }
            
            var bytes = BitConverter.GetBytes(Value);

            writer.Comment(string.Format("; IntLiteral {0}(0x{0:X8}) to 0x{1:X2}.", Value, _address));

            for (int i = 0; i < bytes.Length; i++)
            {
                var b = bytes[i];
                writer.LoadLiteralToW(b);
                writer.MoveWToFile(_address + i);
            }
        }
    }
}