using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class ReturnCode : ICode
    {
        public ICode Value { get; set; }

        public MethodDefinition Method { get; private set; }

        public TypeDefinition ResultType
        {
            get { return TypeDefinitions.Void; }
        }

        public ResultLocation Location
        {
            get { throw new InvalidOperationException("Void do not have location."); }
        }

        public ReturnCode(MethodDefinition method, ICode value)
        {
            if (method == null) throw new ArgumentNullException("method");
            Method = method;
            Value = value;
        }

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
            if (Value != null && Value.ResultType != TypeDefinitions.Void)
            {
                if (Value.ResultType == TypeDefinitions.Byte && Method.ReturnValueLocation.IsWorkRegister)
                {
                    var value = Value;
                    var cast = value as CastCode;
                    if (cast != null && cast.ResultType == TypeDefinitions.Byte)
                    {
                        value = cast.Code;
                    }
                    var literal = value as IntValue;
                    if (literal != null)
                    {
                        writer.Return((byte)literal.Value);
                    }
                }
                Value.WriteMpAsm(writer, memManager);

                for (int i = 0; i < ResultType.Size; i++)
                {
                    writer.MoveFileToW(Value.Location + i);
                    writer.MoveWToFile(Method.ReturnValueLocation + i);
                }

                memManager.Dispose(Value.Location, Value.ResultType.Size);
            }
            writer.Return();
        }
    }
}