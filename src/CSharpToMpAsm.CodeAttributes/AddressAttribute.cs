using System;

namespace CSharpToMpAsm.CodeAttributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
    public class AddressAttribute : Attribute
    {
        public int Address { get; set; }

        public AddressAttribute(int address)
        {
            Address = address;
        }
    }
}
