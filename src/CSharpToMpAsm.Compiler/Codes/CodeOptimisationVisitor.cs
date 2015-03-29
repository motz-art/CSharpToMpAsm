using System;
using System.Linq;

namespace CSharpToMpAsm.Compiler.Codes
{
    public abstract class CodeOptimisationVisitor
    {
        public virtual ICode Visit(ICode code)
        {
            var nullCode = code as NullCode;
            if (nullCode != null) return Optimize(nullCode);
            var returnCode = code as ReturnCode;
            if (returnCode != null) return Optimize(returnCode);
            var call = code as Call;
            if (call != null) return Optimize(call);
            var blockCode = code as BlockCode;
            if (blockCode != null) return Optimize(blockCode);
            var assign = code as Assign;
            if (assign != null) return Optimize(assign);
            var castCode = code as CastCode;
            if (castCode != null) return Optimize(castCode);
            var bitwiseAnd = code as BitwiseAnd;
            if (bitwiseAnd != null) return Optimize(bitwiseAnd);
            var bitwiseOr = code as BitwiseOr;
            if (bitwiseOr != null) return Optimize(bitwiseOr);
            var shiftLeft = code as ShiftLeft;
            if (shiftLeft != null) return Optimize(shiftLeft);
            var shiftRight = code as ShiftRight;
            if (shiftRight != null) return Optimize(shiftRight);
            var intValue = code as IntValue;
            if (intValue != null) return Optimize(intValue);
            var getValue = code as GetValue;
            if (getValue != null) return Optimize(getValue);
            var labelCode = code as LabelCode;
            if (labelCode != null) return Optimize(labelCode);
            var gotoCode = code as GotoCode;
            if (gotoCode != null) return Optimize(gotoCode);
            var getReference = code as GetReference;
            if (getReference != null) return Optimize(getReference);
            var addInts = code as AddInts;
            if (addInts != null) return Optimize(addInts);
            var swapf = code as SwapfCode;
            if (swapf!=null) return Optimize(swapf);
            var postDecrement = code as PostDecrementCode;
            if (postDecrement != null) return Optimize(postDecrement);
            var postIncrement = code as PostIncrementCode;
            if (postIncrement != null) return Optimize(postIncrement);
            var ifElseCode = code as IfElseCode;
            if (ifElseCode != null) return Optimize(ifElseCode);
            var equalityCode = code as EqualityCode;
            if (equalityCode != null) return Optimize(equalityCode);
            var whileCode = code as WhileLoopCode;
            if (whileCode != null) return Optimize(whileCode);
            var boolValue = code as BoolValue;
            if (boolValue != null) return Optimize(boolValue);
            throw new NotImplementedException();
        }

        private ICode Optimize(BoolValue boolValue)
        {
            return boolValue;
        }

        protected virtual ICode Optimize(WhileLoopCode whileCode)
        {
            var condition = Visit(whileCode.Condition);
            var body = Visit(whileCode.LoopBody);
            if (ReferenceEquals(condition, whileCode.Condition) && ReferenceEquals(body, whileCode.LoopBody))
                return whileCode;
            
            return new WhileLoopCode(condition, body);
        }

        protected virtual ICode Optimize(EqualityCode equalityCode)
        {
            var left = Visit(equalityCode.Left);
            var right = Visit(equalityCode.Right);
            if (ReferenceEquals(left, equalityCode.Left)
                && ReferenceEquals(right, equalityCode.Right))
                return equalityCode;
            return new EqualityCode(left, right);
        }

        protected virtual ICode Optimize(IfElseCode ifElseCode)
        {
            var condition = Visit(ifElseCode.Condition);
            var trueCode = Visit(ifElseCode.TrueCode);
            var falseCode = Visit(ifElseCode.FalseCode);

            if (ReferenceEquals(condition, ifElseCode.Condition)
                && ReferenceEquals(trueCode, ifElseCode.TrueCode)
                && ReferenceEquals(falseCode, ifElseCode.FalseCode))
                return ifElseCode;

            return new IfElseCode(condition, trueCode, falseCode);
        }

        protected virtual ICode Optimize(PostIncrementCode postIncrement)
        {
            return postIncrement;
        }

        protected virtual ICode Optimize(PostDecrementCode postDecrement)
        {
            return postDecrement;
        }

        protected virtual ICode Optimize(SwapfCode swapf)
        {
            var value = Visit(swapf.Value);
            if (ReferenceEquals(value, swapf.Value))
                return swapf;
            return new SwapfCode(value);
        }

        protected virtual ICode Optimize(AddInts addInts)
        {
            var left = Visit(addInts.Left);
            var right = Visit(addInts.Right);
            if (ReferenceEquals(left, addInts.Left) && ReferenceEquals(right, addInts.Right))
                return addInts;
            return new AddInts(left, right);
        }

        protected virtual ICode Optimize(GetReference getReference)
        {
            return getReference;
        }

        protected virtual ICode Optimize(GotoCode gotoCode)
        {
            return gotoCode;
        }

        protected virtual ICode Optimize(LabelCode labelCode)
        {
            return labelCode;
        }

        protected virtual ICode Optimize(GetValue getValue)
        {
            return getValue;
        }

        protected virtual ICode Optimize(IntValue intValue)
        {
            return intValue;
        }

        protected virtual ICode Optimize(NullCode nullCode)
        {
            return nullCode;
        }

        protected virtual ICode Optimize(ShiftRight shiftRight)
        {
            var left = Visit(shiftRight.Left);
            var right = Visit(shiftRight.Right);
            if (ReferenceEquals(left, shiftRight.Left) && ReferenceEquals(right, shiftRight.Right))
                return shiftRight;
            return new ShiftRight(left, right);
        }

        protected virtual ICode Optimize(ShiftLeft shiftLeft)
        {
            var left = Visit(shiftLeft.Left);
            var right = Visit(shiftLeft.Right);
            if (ReferenceEquals(left, shiftLeft.Left) && ReferenceEquals(right, shiftLeft.Right))
                return shiftLeft;
            return new ShiftLeft(left, right);
        }

        protected virtual ICode Optimize(BitwiseOr bitwiseOr)
        {
            var left = Visit(bitwiseOr.Left);
            var right = Visit(bitwiseOr.Right);
            if (ReferenceEquals(left, bitwiseOr.Left) && ReferenceEquals(right, bitwiseOr.Right))
                return bitwiseOr;
            return new BitwiseOr(left, right);
        }

        protected virtual ICode Optimize(BitwiseAnd bitwiseAnd)
        {
            var left = Visit(bitwiseAnd.Left);
            var right = Visit(bitwiseAnd.Right);
            if (ReferenceEquals(left, bitwiseAnd.Left) && ReferenceEquals(right, bitwiseAnd.Right))
                return bitwiseAnd;
            return new BitwiseAnd(left, right);
        }

        protected virtual ICode Optimize(ReturnCode returnCode)
        {
            var newCode = Visit(returnCode.Value);
            if (ReferenceEquals(newCode, returnCode.Value))
                return returnCode;
            return new ReturnCode(returnCode.Method, newCode);
        }

        protected virtual ICode Optimize(Call call)
        {
            var array = call.Args.Select(x => Visit(x)).ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (!ReferenceEquals(array[i], call.Args[i]))
                    return new Call(call.Method, array);
            }
            return call;
        }

        protected virtual ICode Optimize(BlockCode blockCode)
        {
            var codes = blockCode.Codes.Select(x => Visit(x)).Where(x => !(x is NullCode)).ToArray();
            if (codes.Length == 0) return new NullCode();
            if (codes.Length == 1) return codes[0];
            if (codes.Length == blockCode.Codes.Length)
            {
                for (int i = 0; i < codes.Length; i++)
                {
                    if (!ReferenceEquals(codes[i], blockCode.Codes[i]))
                        return new BlockCode(codes);
                }
                return blockCode;
            }
            return new BlockCode(codes);
        }

        protected virtual ICode Optimize(Assign assign)
        {
            var code = Visit(assign.Code);
            if (ReferenceEquals(code, assign.Code))
            {
                return assign;
            }
            return new Assign(assign.Destination, code);
        }

        protected virtual ICode Optimize(CastCode castCode)
        {
            var code = Visit(castCode.Code);
            if (ReferenceEquals(code, castCode.Code))
                return castCode;
            return new CastCode(castCode.ResultType, code);
        }
    }
}