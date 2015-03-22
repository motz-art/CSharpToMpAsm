using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class IfElseCode : ICode
    {
        public ICode Condition { get; private set; }
        public ICode TrueCode { get; private set; }
        public ICode FalseCode { get; private set; }

        public IfElseCode(ICode condition, ICode trueCode, ICode falseCode)
        {
            if (condition == null) throw new ArgumentNullException("condition");
            if (trueCode == null) throw new ArgumentNullException("trueCode");
            if (falseCode == null) throw new ArgumentNullException("falseCode");

            Condition = condition;
            TrueCode = trueCode;
            FalseCode = falseCode;
        }

        public bool Equals(ICode other)
        {
            throw new NotImplementedException();
        }

        public TypeDefinition ResultType
        {
            get { return TypeDefinitions.Void; }
        }

        public ResultLocation Location
        {
            get { throw new NotImplementedException(); }
        }

        public void WriteMpAsm(IMpAsmWriter writer, IMemoryManager memManager)
        {
            var equality = Condition as EqualityCode;
            if (equality != null)
            {
                var value = equality.Left as IntValue;
                if (value != null)
                {
                    WriteWithConstantValue(equality.Right, value, writer, memManager);
                    return;
                }
                value = equality.Right as IntValue;
                if (value != null)
                {
                    WriteWithConstantValue(equality.Left, value, writer, memManager);
                    return;
                }
                throw new NotImplementedException();

            }
            Condition.WriteMpAsm(writer, memManager);
            throw new NotImplementedException();
        }

        private void WriteWithConstantValue(ICode right, IntValue value, IMpAsmWriter writer, IMemoryManager memManager)
        {
            if (value.Value == 0)
            {
                if (right.ResultType == TypeDefinitions.Byte)
                {
                    right.WriteMpAsm(writer, memManager);
                    if (right.Location.IsWorkRegister)
                    {
                        throw new NotImplementedException();
                    }
                    else
                    {
                        writer.MoveFileToFile(right.Location);
                        memManager.Dispose(right.Location, right.ResultType.Size);
                        writer.BitTestSkipSet(CommonCodes.Status, CommonCodes.StatusZ);
                        
                        if (FalseCode is NullCode)
                        {
                            var ifEndLabl = writer.CreateLabel();
                            writer.GoTo(ifEndLabl);
                            TrueCode.WriteMpAsm(writer,memManager);
                            writer.WriteLabel(ifEndLabl);
                        }
                        else
                        {
                            var ifEndLabl = writer.CreateLabel();
                            var falseLabl = writer.CreateLabel();

                            writer.GoTo(falseLabl);
                            
                            TrueCode.WriteMpAsm(writer, memManager);
                            
                            writer.GoTo(ifEndLabl);
                            writer.WriteLabel(falseLabl);
                            
                            FalseCode.WriteMpAsm(writer, memManager);
                            
                            writer.WriteLabel(ifEndLabl);                            
                        }
                        return;
                    }
                }
            }
            throw new NotImplementedException();
        }
    }
}