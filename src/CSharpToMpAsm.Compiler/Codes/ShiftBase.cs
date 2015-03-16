using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public abstract class ShiftBase : ICode
    {
        public ICode Left { get; private set; }
        public ICode Right { get; private set; }
        private ResultLocation _location;

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

        public TypeDefinition ResultType { get; private set; }

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
    }
}