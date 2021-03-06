using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public abstract class ShiftBase : ICode
    {
        public ICode Left { get; private set; }
        public ICode Right { get; private set; }

        public ShiftBase(ICode left, ICode right) : this(left, right, TypeDefinitions.Int32)
        {
            
        }

        public ShiftBase(ICode left, ICode right, TypeDefinition resultType)
        {
            if (left == null) throw new ArgumentNullException("left");
            if (right == null) throw new ArgumentNullException("right");
            if (resultType == null) throw new ArgumentNullException("resultType");

            var literal = right as IntValue;
            if (literal==null)
                throw new NotSupportedException("Shifting on custom bit's count is not supported.");

            Left = left;
            Right = right;
            ResultType = resultType; 
        }

        protected ShiftBase(ICode left, ICode right, TypeDefinition resultType, ResultLocation location) : this(left, right, resultType)
        {
            Location = location;
        }

        public TypeDefinition ResultType { get; private set; }

        public ResultLocation Location { get; private set; }

        public void WriteMpAsm(IMpAsmWriter writer)
        {
            Left.WriteMpAsm(writer);
            writer.Copy(Left.Location,Location,ResultType.Size);
            
            var literal = Right as IntValue;
            if (literal != null)
            {
                var cnt = literal.Value;
                for (int i = 0; i < cnt; i++)
                {
                    RotateOnce(writer);
                }
            }
            else
            {
                throw new NotSupportedException("Shifting on custom bit's count is not supported.");
            }
        }

        protected abstract void RotateOnce(IMpAsmWriter writer);

        protected bool Equals(ShiftBase other)
        {
            return Equals(Left, other.Left) && Equals(Right, other.Right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ShiftBase) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Left != null ? Left.GetHashCode() : 0)*397) ^ (Right != null ? Right.GetHashCode() : 0);
            }
        }

        public bool Equals(ICode other)
        {
            return Equals((object)other);
        }
    }
}