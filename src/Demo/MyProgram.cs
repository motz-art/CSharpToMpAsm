namespace Demo
{
    public class MyProgram : Pic12F675
    {
        private byte cnt { get; set; }
        private byte value { get; set; }

        public override void Setup()
        {
            cnt = 255;
            value = 0;
            Write(value);
        }

        protected override void DoWork()
        {
            cnt--;
            if (cnt == 0)
            {
                cnt=255;
                value++;
                if (value == 0)
                    return;

                Write(value);
            }
        }
    }
}
