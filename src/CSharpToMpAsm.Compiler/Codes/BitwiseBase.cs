using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public abstract class BitwiseBase : ICode
    {
        private readonly ICode _left;
        private readonly ICode _right;
        private ResultLocation _location;

        public BitwiseBase(ICode left, ICode right)
        {
            if (left == null) throw new ArgumentNullException("left");
            if (right == null) throw new ArgumentNullException("right");

            if (!left.ResultType.IsNumeric() || !right.ResultType.IsNumeric())
            {
                throw new NotSupportedException("Bitwise or operator is only supported for numeric types.");
            }

            if (left.ResultType.Size > right.ResultType.Size)
            {
                right = new CastCode(left.ResultType, right);
            }
            else if (right.ResultType.Size > left.ResultType.Size)
            {
                left = new CastCode(right.ResultType, left);
            }

            _left = left;
            _right = right;
        }

        public TypeDefinition ResultType
        {
            get { return _left.ResultType; }
        }

        public ResultLocation Location
        {
            get { return _location; }
        }

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
            _left.WriteMpAsm(writer, memManager);
            _location = memManager.Alloc(ResultType.Size);
            if (_left.Location.IsWorkRegister)
            {
                if (ResultType.Size != 1)
                {
                    throw new InvalidOperationException("Result sizes do not match.");
                }
                writer.MoveWToFile(_location);
            }
            else
            {
                for (int i = 0; i < ResultType.Size; i++)
                {
                    writer.MoveFileToW(_left.Location + i);
                    writer.MoveWToFile(_location + i);
                }
            }

            _right.WriteMpAsm(writer, memManager);
            
            if (_right.Location.IsWorkRegister)
            {
                if (_right.ResultType.Size != 1)
                {
                    throw new InvalidOperationException("Result sizes do not match.");
                }

                WriteBitwiseOperation(writer, _location);
            }
            else
            {
                for (int i = 0; i < ResultType.Size; i++)
                {
                    writer.MoveFileToW(_right.Location + i);
                    WriteBitwiseOperation(writer, _location + i);
                }
            }
        }

        protected abstract void WriteBitwiseOperation(IMpAsmWriter writer, ResultLocation location);
    }
}