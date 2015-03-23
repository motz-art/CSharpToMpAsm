using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class EqualityCode : ICode
    {
        public ICode Left { get; private set; }
        public ICode Right { get; private set; }

        public EqualityCode(ICode left, ICode right)
        {
            if (left == null) throw new ArgumentNullException("left");
            if (right == null) throw new ArgumentNullException("right");

            Left = left;
            Right = right;
        }

        public bool Equals(ICode other)
        {
            throw new NotImplementedException();
        }

        public TypeDefinition ResultType
        {
            get { throw new NotImplementedException(); }
        }

        public ResultLocation Location
        {
            get { throw new NotImplementedException(); }
        }

        public void WriteMpAsm(IMpAsmWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}