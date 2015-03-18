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

            if (Left.Location.IsWorkRegister)
            {
                _location = memManager.Alloc(ResultType.Size);
                writer.MoveWToFile(_location);
            }
            else
            {
                _location = Left.Location;
            }

            Right.WriteMpAsm(writer, memManager);
            
            if (Right.Location.IsWorkRegister)
            {
                WriteBitwiseOperation(writer, _location);
            }
            else
            {
                for (int i = 0; i < ResultType.Size; i++)
                {
                    writer.MoveFileToW(Right.Location + i);
                    WriteBitwiseOperation(writer, _location + i);
                }
                memManager.Dispose(Right.Location, Right.ResultType.Size);
            }
        }

        protected abstract void WriteBitwiseOperation(IMpAsmWriter writer, ResultLocation location);
        public bool Equals(ICode other)
        {
            return Equals((object) other);
        }

        protected bool Equals(BitwiseBase other)
        {
            return Equals(Left, other.Left) && Equals(Right, other.Right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BitwiseBase) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Left != null ? Left.GetHashCode() : 0)*397) ^ (Right != null ? Right.GetHashCode() : 0);
            }
        }
    }
}