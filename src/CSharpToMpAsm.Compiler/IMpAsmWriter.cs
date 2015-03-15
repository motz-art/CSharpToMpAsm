using System;
using System.IO;
using CSharpToMpAsm.Compiler.Codes;

namespace CSharpToMpAsm.Compiler
{
    public interface IMpAsmWriter
    {
        ILabel CreateLabel(string name);
        ILabel CreateLabel(string name, int codeAddress);

        void Comment(string text);
        void Comment(string format, params object[] args);

        void WriteLabel(ILabel label);
        void MoveFileToW(ResultLocation location);
        void MoveWToFile(ResultLocation location);
        void LoadLiteralToW(byte literal);
        void Call(ILabel label);
        void Return();
        void GoTo(ILabel label);
        void Return(byte value);
        void OrWFile(ResultLocation location);
        void RotateLeftFileToW(ResultLocation location);
        void RotateRightFileToW(ResultLocation location);
        void AndWFile(ResultLocation location);
        void ClearFile(ResultLocation location);
    }

    class TextLabel : ILabel
    {
        public string Name { get; set; }
        public int Address { get; set; }
    }

    class MpAsmTextWriter : IMpAsmWriter
    {
        private readonly TextWriter _writer;

        public MpAsmTextWriter(TextWriter writer)
        {
            _writer = writer;
        }

        public ILabel CreateLabel(string name)
        {
            return new TextLabel { Name = name, Address = -1 };
        }

        public ILabel CreateLabel(string name, int address)
        {
            if (address < 0)
                throw new ArgumentException("Label address should be greater or equal to 0.");

            return new TextLabel { Name = name, Address = address };
        }

        public void Comment(string text)
        {
            _writer.WriteLine(text);
        }

        public void Comment(string format, params object[] args)
        {
            _writer.WriteLine(format, args);
        }

        public void WriteLabel(ILabel label)
        {
            var textLabel = GetLabel(label);

            if (textLabel.Address == -1)
            {
                _writer.WriteLine("{0}", textLabel.Name);
            }
            else
            {
                _writer.WriteLine("{0} CODE {1}", textLabel.Name, textLabel.Address);
            }
        }


        public void MoveFileToW(ResultLocation location)
        {
            if (location.IsWorkRegister)
                return;

            _writer.WriteLine("\tMOVF 0x{0:X2}, w", location.Address);
        }

        public void MoveWToFile(ResultLocation location)
        {
            if (location.IsWorkRegister)
                return;

            _writer.WriteLine("\tMOVWF 0x{0:X2}", location.Address);
        }

        public void LoadLiteralToW(byte literal)
        {
            _writer.WriteLine("\tMOVLW 0x{0:X2}", literal);
        }

        public void Call(ILabel label)
        {
            var textLabel = GetLabel(label);

            _writer.WriteLine("\tCALL {0}", textLabel.Name);
        }

        public void Return()
        {
            _writer.WriteLine("\tRETURN");
        }

        public void Return(byte value)
        {
            _writer.WriteLine("\tRETLW 0x{0:X2}", value);
        }

        public void OrWFile(ResultLocation location)
        {
            _writer.WriteLine("\tIORWF 0x{0:X2}", location);
        }

        public void RotateLeftFileToW(ResultLocation location)
        {
            _writer.WriteLine("\tRLF {0},w", location);
        }

        public void RotateRightFileToW(ResultLocation location)
        {
            _writer.WriteLine("\tRRF {0},w", location);
        }

        public void AndWFile(ResultLocation location)
        {
            _writer.WriteLine("\tANDWF {0},f", location);
        }

        public void ClearFile(ResultLocation location)
        {
            _writer.WriteLine("\tCLRF {0}");
        }

        public void GoTo(ILabel label)
        {
            var textLabel = GetLabel(label);

            _writer.WriteLine("\tGOTO {0}", textLabel.Name);
        }

        private static TextLabel GetLabel(ILabel label)
        {
            if (label == null) throw new ArgumentNullException("label");

            var textLabel = label as TextLabel;
            if (textLabel == null)
                throw new InvalidOperationException("Label wasn't correctly generated.");
            return textLabel;
        }
    }
}