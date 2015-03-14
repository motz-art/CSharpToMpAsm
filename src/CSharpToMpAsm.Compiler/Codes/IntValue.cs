using System;
using System.Text;

namespace CSharpToMpAsm.Compiler.Codes
{
    internal class IntValue : ICode
    {
        private readonly int _data;
        private static ResultLocation _staticAddress;
        private ResultLocation _address;

        public IntValue(int data)
        {
            _data = data;
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

        public string GetMpAsm(CompilationContext compilationContext)
        {
            if (_address == null)
            {
                if (_staticAddress == null)
                {
                    _staticAddress = compilationContext.MemAllocate(ResultType.Size);
                }
                _address = _staticAddress;
            }
            var sb = new StringBuilder();

            var bytes = BitConverter.GetBytes(_data);

            sb.AppendLine(string.Format("; IntLiteral {0}(0x{0:X8}) to 0x{1:X2}.", _data, _address));

            for (int i = 0; i < bytes.Length; i++)
            {
                var b = bytes[i];
                sb.AppendLine(string.Format("\tMOVLW 0x{0:X2}", b));
                sb.AppendLine(string.Format("\tMOVWF 0x{0:X2}", _address.Address + i));
            }

            return sb.ToString();
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
            
            var bytes = BitConverter.GetBytes(_data);

            writer.Comment(string.Format("; IntLiteral {0}(0x{0:X8}) to 0x{1:X2}.", _data, _address));

            for (int i = 0; i < bytes.Length; i++)
            {
                var b = bytes[i];
                writer.LoadLiteralToW(b);
                writer.MoveWToFile(_address + i);
            }
        }
    }
}