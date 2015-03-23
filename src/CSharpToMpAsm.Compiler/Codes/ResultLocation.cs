using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public class ResultLocation
    {
        public bool IsWorkRegister{get { return Address == -1; }}

        public int Address { get; set; }
        public int Bank { get { return (Address & 0x80)>>7; } }

        public ResultLocation(int address)
        {
            Address = address;
        }

        private static ResultLocation _workRegister;
        public static ResultLocation WorkRegister
        {
            get { return _workRegister ?? (_workRegister = new ResultLocation(-1)); }
        }

        public override string ToString()
        {
            if (IsWorkRegister) return "w";
            return string.Format("0x{0:X4}", Address);
        }

        public static ResultLocation operator + (ResultLocation location, int offset)
        {
            if (location == null) throw new ArgumentNullException("location");
            return new ResultLocation(location.Address + offset);
        }
    }
}