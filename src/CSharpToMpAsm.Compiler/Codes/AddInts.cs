using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class AddInts : ICode
    {
        public ICode Left { get; private set; }
        public ICode Right { get; private set; }
        private static ResultLocation _staticAddress;
        private ResultLocation _address;

        public AddInts(ICode left, ICode right)
        {
            Left = left;
            Right = right;
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