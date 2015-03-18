using CSharpToMpAsm.CodeAttributes;

namespace Demo
{
    public abstract class Pic12F629
    {
        [Address(0x00), Address(0x80)]
        protected byte File { get; set; }

        [Address(0x05)]
        protected byte GPIO { get; set; }

        [Address(0x03)]
        protected byte _STATUS { get; set; }

        [Address(0)]
        public virtual void Start()
        {
            Startup();

            begin: 
            
            DoWork();
            
            goto begin;
        }

        public virtual void Startup() { }

        [Address(4)]
        protected void _Interrupt(ref byte w)
        {
            var wTemp = w;
            var statusTemp = SWAPFW(_STATUS);
            
            Interrupt();
            
            _STATUS = SWAPFW(statusTemp);
            SWAPF(ref wTemp);
            w = SWAPFW(wTemp);
        }

        protected virtual void Interrupt()
        {
        }

        public byte SWAPFW(byte value)
        {
            return (byte)(((value << 4) & 0xF0) | ((value >> 4) & 0x0F));
        }

        public void SWAPF(ref byte value)
        {
            value = (byte)(((value << 4) & 0xF0) | ((value >> 4) & 0x0F));
        }

        protected abstract void DoWork();

        protected void WriteInt(int data)
        {
            GPIO = (byte)data;
        }
        protected void Write(byte data)
        {
            GPIO = data;
        }
    }
}