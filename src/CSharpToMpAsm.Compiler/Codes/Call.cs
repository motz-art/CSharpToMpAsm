using System;
using System.Text;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class Call : ICode
    {
        public MethodDefinition Method { get; private set; }
        public ICode[] Args { get; private set; }

        public Call(MethodDefinition method, ICode[] args)
        {
            Method = method;
            Args = args;
        }

        public TypeDefinition ResultType
        {
            get
            {
                return Method.ReturnType;
            }
        }

        public ResultLocation Location
        {
            get { return Method.ReturnValueLocation; }
        }

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
            if (Args.Length != Method.Parameters.Length)
                throw new InvalidOperationException("Method parameters count do not match actually supplied.");

            if (Args.Length > 0)
            {
                for (int i = 0; i < Args.Length; i++)
                {
                    var code = Args[i];
                    if (code.ResultType != Method.Parameters[i].Type)
                    {
                        throw new InvalidOperationException("Argument type do not match required by method.");
                    }
                    code = new Assign(Method.Parameters[i], code);

                    code.WriteMpAsm(writer, memManager);
                }
            }

            if (ResultType != TypeDefinitions.Void && Method.ReturnValueLocation == null)
            {
                var location = memManager.Alloc(ResultType.Size);
                Method.ReturnValueLocation = location;
            }

            writer.Call(Method.Label);
        }

        public bool Equals(ICode other)
        {
            throw new NotImplementedException();
        }

        protected bool Equals(Call other)
        {
            if (Args.Length != other.Args.Length) return false;
            for (int i = 0; i < Args.Length; i++)
            {
                if (!Args[i].Equals(other.Args[i])) return false;
            }
            return Equals(Method, other.Method);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Call) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Method != null ? Method.GetHashCode() : 0)*397) ^ (Args != null ? Args.GetHashCode() : 0);
            }
        }
    }
}