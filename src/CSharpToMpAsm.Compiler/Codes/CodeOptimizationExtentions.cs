using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CSharpToMpAsm.Compiler.Codes
{
    public static class CodeOptimizationExtentions
    {
        public static ICode Optimize(this ICode code)
        {
            if (code is NullCode)
            {
                return code;
            }
            if (code is ReturnCode)
            {
                return Optimize((ReturnCode)code);
            }
            if (code is Call)
            {
                return Optimize((Call) code);
            }
            if (code is BlockCode)
            {
                return Optimize((BlockCode) code);
            }
            if (code is Assign)
            {
                return Optimize((Assign) code);
            }
            if (code is CastCode)
            {
                return Optimize((CastCode) code);
            }
            return code;
        }

        private static ICode Optimize(Call call)
        {
            return new Call(call.Method, call.Args.Select(x=>x.Optimize()).ToArray());
        }

        private static ICode Optimize(ReturnCode returnCode)
        {
            return new ReturnCode(returnCode.Method, returnCode.Value);
        }

        private static ICode Optimize(Assign assign)
        {
            return new Assign(assign.Destination, assign.Code.Optimize());
        }

        private static ICode Optimize(CastCode castCode)
        {
            if (castCode.ResultType.Size < castCode.Code.ResultType.Size)
            {
                return castCode.Code.ReduceType(castCode.ResultType);
            }
            return castCode.Optimize();
        }

        private static ICode Optimize(BlockCode blockCode)
        {
            var codes = blockCode.Codes.Select(x => x.Optimize()).Where(x=> !(x is NullCode)).ToArray();
            if (codes.Length==0) return new NullCode();
            if (codes.Length == 1) return codes[0];
            return new BlockCode(codes);
        }

        public static ICode ReduceType(this ICode code, TypeDefinition toType)
        {
            if (code is CastCode)
            {
                return ReduceType((CastCode)code, toType);
            }
            if (code is ShiftLeft)
            {
                return ReduceType((ShiftLeft)code, toType);
            }
            if (code is ShiftRight)
            {
                return ReduceType((ShiftRight)code, toType);
            }
            if (code is BitwiseOr)
            {
                return ReduceType((BitwiseOr) code, toType);
            }            
            if (code is BitwiseAnd)
            {
                return ReduceType((BitwiseAnd)code, toType);
            }

            return new CastCode(toType, code);
        }

        public static ICode ReduceType(BitwiseOr code, TypeDefinition toType)
        {
            return new BitwiseOr(code.Left.ReduceType(toType), code.Right.ReduceType(toType));
        }

        public static ICode ReduceType(BitwiseAnd code, TypeDefinition toType)
        {
            return new BitwiseAnd(code.Left.ReduceType(toType), code.Right.ReduceType(toType));
        }

        public static ICode ReduceType(ShiftLeft code, TypeDefinition toType)
        {
            return new ShiftLeft(code.Left.ReduceType(toType), code.Right, toType);
        }

        public static ICode ReduceType(ShiftRight code, TypeDefinition toType)
        {
            return new ShiftRight(code.Left.ReduceType(toType), code.Right, toType);
        }

        public static ICode ReduceType(CastCode castCode, TypeDefinition toType)
        {
            if (castCode.Code.ResultType == toType)
                return castCode.Code;
            return new CastCode(toType, castCode.Code);
        }
    }
}