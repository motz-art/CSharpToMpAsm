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

        protected bool Equals(IfElseCode other)
        {
            return Equals(Condition, other.Condition) && Equals(TrueCode, other.TrueCode) && Equals(FalseCode, other.FalseCode);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((IfElseCode) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Condition != null ? Condition.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (TrueCode != null ? TrueCode.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (FalseCode != null ? FalseCode.GetHashCode() : 0);
                return hashCode;
            }
        }

        public bool Equals(ICode other)
        {
            return Equals((object) other);
        }

        public TypeDefinition ResultType
        {
            get { return TypeDefinitions.Void; }
        }

        public ResultLocation Location
        {
            get { throw new InvalidOperationException(); }
        }

        public void WriteMpAsm(IMpAsmWriter writer)
        {
            var equality = Condition as EqualityCode;
            if (equality != null)
            {
                var value = equality.Left as IntValue;
                if (value != null)
                {
                    WriteWithConstantValue(equality.Right, value, writer);
                    return;
                }
                value = equality.Right as IntValue;
                if (value != null)
                {
                    WriteWithConstantValue(equality.Left, value, writer);
                    return;
                }
                throw new NotImplementedException();

            }
            Condition.WriteMpAsm(writer);
            throw new NotImplementedException();
        }

        private void WriteWithConstantValue(ICode right, IntValue value, IMpAsmWriter writer)
        {
            if (value.Value == 0)
            {
                if (right.ResultType == TypeDefinitions.Byte)
                {
                    right.WriteMpAsm(writer);
                    if (right.Location.IsWorkRegister)
                    {
                        throw new NotImplementedException();
                    }
                    else
                    {
                        writer.MoveFileToFile(right.Location);
                        writer.BitTestSkipSet(CommonCodes.Status, CommonCodes.StatusZ);
                        
                        if (FalseCode is NullCode)
                        {
                            var ifEndLabl = writer.CreateLabel();
                            writer.GoTo(ifEndLabl);
                            TrueCode.WriteMpAsm(writer);
                            writer.WriteLabel(ifEndLabl);
                        }
                        else
                        {
                            var ifEndLabl = writer.CreateLabel();
                            var falseLabl = writer.CreateLabel();

                            writer.GoTo(falseLabl);
                            
                            TrueCode.WriteMpAsm(writer);
                            
                            writer.GoTo(ifEndLabl);
                            writer.WriteLabel(falseLabl);
                            
                            FalseCode.WriteMpAsm(writer);
                            
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