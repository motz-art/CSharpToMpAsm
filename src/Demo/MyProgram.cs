namespace Demo
{
    public class MyProgram : Pic12F675  
    {
        protected override void DoWork()
        {
            var a = 0x01;
            var b = 0x0B;
            var c = a + b;
            var d = (byte) c;
            WriteInt(c);
            Write(d);
        }
    }
}
