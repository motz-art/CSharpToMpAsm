using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class BlockCode : ICode
    {
        public ICode[] Codes { get; private set; }

        public BlockCode(ICode[] codes)
        {
            Codes = codes;
        }

        public TypeDefinition ResultType
        {
            get { return TypeDefinitions.Void; }
        }

        public ResultLocation Location
        {
            get { throw new InvalidOperationException("BlobkCode has Void result type."); }
        }

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
            foreach (var code in Codes)
            {
                code.WriteMpAsm(writer, memManager);
                if (code.ResultType != TypeDefinitions.Void)
                {
                    memManager.Dispose(code.Location, code.ResultType.Size);
                }
            }
        }

        protected bool Equals(BlockCode other)
        {
            if (Codes.Length != other.Codes.Length) return false;

            for (int i = 0; i < Codes.Length; i++)
            {
                if (!Codes[i].Equals(other.Codes[i])) return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BlockCode)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = Codes.Length.GetHashCode();
                foreach (var code in Codes)
                {
                    hash = hash * 397 ^ code.GetHashCode();
                }
                return hash;
            }
        }

        public bool Equals(ICode other)
        {
            return Equals((object)other);
        }
    }
}