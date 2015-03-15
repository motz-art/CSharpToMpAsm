using System;
using System.Text;

namespace CSharpToMpAsm.Compiler.Codes
{
    internal class Call : ICode
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
    }
}