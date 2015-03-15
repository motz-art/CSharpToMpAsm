using System;
using System.Text;

namespace CSharpToMpAsm.Compiler.Codes
{
    internal class Call : ICode
    {
        private readonly MethodDefinition _method;
        private readonly ICode[] _args;

        public Call(MethodDefinition method, ICode[] args)
        {
            _method = method;
            _args = args;
        }

        public TypeDefinition ResultType
        {
            get
            {
                return _method.ReturnType;
            }
        }

        public ResultLocation Location
        {
            get { return _method.ReturnValueLocation; }
        }

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
            if (_args.Length != _method.Parameters.Length)
                throw new InvalidOperationException("Method parameters count do not match actually supplied.");

            if (_args.Length > 0)
            {
                for (int i = 0; i < _args.Length; i++)
                {
                    var code = _args[i];
                    if (code.ResultType != _method.Parameters[i].Type)
                    {
                        throw new InvalidOperationException("Argument type do not match required by method.");
                    }
                    code = new Assign(_method.Parameters[i], code);

                    code.WriteMpAsm(writer, memManager);
                }
            }

            if (ResultType != TypeDefinitions.Void && _method.ReturnValueLocation == null)
            {
                var location = memManager.Alloc(ResultType.Size);
                _method.ReturnValueLocation = location;
            }

            writer.Call(_method.Label);
        }
    }
}