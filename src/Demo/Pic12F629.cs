using CSharpToMpAsm.CodeAttributes;

namespace Demo
{
    public abstract class Pic12F629
    {
        [Address(0x00), Address(0x80)]
        protected byte File { get; set; }

        [Address(0x05)]
        protected byte GPIO { get; set; }

        [Address(0)]
        private void Start()
        {
            DoWork();
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