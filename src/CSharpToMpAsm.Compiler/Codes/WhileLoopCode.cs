using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class WhileLoopCode : ICode
    {
        public ICode Condition { get; private set; }
        public ICode LoopBody { get; private set; }

        public WhileLoopCode(ICode condition, ICode loopBody)
        {
            if (condition == null) throw new ArgumentNullException("condition");
            if (loopBody == null) throw new ArgumentNullException("loopBody");

            Condition = condition;
            LoopBody = loopBody;
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

        public void WriteMpAsm(IMpAsmWriter writer)
        {
            var constantCondition = Condition as BoolValue;
            if (constantCondition != null)
            {
                if (constantCondition.Value)
                {
                    var lbl = writer.CreateLabel();
                    writer.WriteLabel(lbl);
                    LoopBody.WriteMpAsm(writer);
                    writer.GoTo(lbl);
                }
                return;
            }
            throw new NotImplementedException();
        }
    }
}