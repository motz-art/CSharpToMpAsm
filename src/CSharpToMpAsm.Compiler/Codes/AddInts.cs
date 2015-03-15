using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class AddInts : ICode
    {
        private readonly ICode _left;
        private readonly ICode _right;
        private static ResultLocation _staticAddress;
        private ResultLocation _address;

        public AddInts(ICode left, ICode right)
        {
            _left = left;
            _right = right;
        }

        public TypeDefinition ResultType
        {
            get { return TypeDefinitions.Int32; }
        }

        public ResultLocation Location
        {
            get {
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

            writer.Comment("\tADD A, B");
        }

        public void Optimize()
        {
            
        }
    }
}