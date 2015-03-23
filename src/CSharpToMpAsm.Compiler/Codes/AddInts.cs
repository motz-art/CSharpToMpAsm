using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class AddInts : ICode
    {
        public ICode Left { get; private set; }
        public ICode Right { get; private set; }

        public AddInts(ICode left, ICode right)
        {
            Left = left;
            Right = right;
            ResultType = TypeDefinitions.Int32;
        }

        public TypeDefinition ResultType { get; private set; }

        public ResultLocation Location { get; private set; }

        public void WriteMpAsm(IMpAsmWriter writer)
        {
            throw new NotImplementedException();
        }

        public void Optimize()
        {
            
        }

        protected bool Equals(AddInts other)
        {
            return Equals(Left, other.Left) && Equals(Right, other.Right);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AddInts) obj);
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
            var add = other as AddInts;
            if (add == null) return false;
            return Equals(add);
        }
    }
}