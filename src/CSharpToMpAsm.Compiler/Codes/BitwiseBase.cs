using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public abstract class BitwiseBase : ICode
    {
        public ICode Left { get; private set; }
        public ICode Right { get; private set; }

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

            Left = left;
            Right = right;
        }

        public TypeDefinition ResultType
        {
            get { return Left.ResultType; }
        }

        public ResultLocation Location
        {
            get { return _location; }
        }

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
            Left.WriteMpAsm(writer, memManager);
            _location = memManager.Alloc(ResultType.Size);
            if (Left.Location.IsWorkRegister)
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
                    writer.MoveFileToW(Left.Location + i);
                    writer.MoveWToFile(_location + i);
                }
            }

            Right.WriteMpAsm(writer, memManager);
            
            if (Right.Location.IsWorkRegister)
            {
                if (Right.ResultType.Size != 1)
                {
                    throw new InvalidOperationException("Result sizes do not match.");
                }

                WriteBitwiseOperation(writer, _location);
            }
            else
            {
                for (int i = 0; i < ResultType.Size; i++)
                {
                    writer.MoveFileToW(Right.Location + i);
                    WriteBitwiseOperation(writer, _location + i);
                }
            }
        }

        protected abstract void WriteBitwiseOperation(IMpAsmWriter writer, ResultLocation location);
    }
}