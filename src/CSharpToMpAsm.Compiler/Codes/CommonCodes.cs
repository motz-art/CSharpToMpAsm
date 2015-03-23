using System;

namespace CSharpToMpAsm.Compiler.Codes
{
    public static class CommonCodes
    {
        private static ResultLocation _status;

        public static ResultLocation Status
        {
            get { return _status ?? (_status = new ResultLocation(0x03)); }
        }

        public static int StatusZ { get { return 2; } }

        public static void Copy(this IMpAsmWriter writer, ResultLocation fromLocation, ResultLocation toLocation, int size)
        {
            if (writer == null) throw new ArgumentNullException("writer");
            if (fromLocation == null) throw new ArgumentNullException("fromLocation");
            if (toLocation == null) throw new ArgumentNullException("toLocation");

            if (fromLocation == toLocation) return;
            for (int i = 0; i < size; i++)
            {
                writer.MoveFileToW(fromLocation + i);
                writer.MoveWToFile(toLocation + i);
            }
        }
    }
}