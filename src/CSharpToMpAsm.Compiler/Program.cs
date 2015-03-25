using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using ICSharpCode.NRefactory.CSharp;

namespace CSharpToMpAsm.Compiler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Specify project folder to compile.");
                return;
            }
            var projectDirectory = new DirectoryInfo(args[0]);
            
            var files = projectDirectory.GetFiles("*.cs");

            var parser = new CSharpParser();

            var parseResults = files.Select(x => parser.Parse(File.OpenText(x.FullName), x.Name)).ToArray();

            var compilationContext = Compile(parseResults);
            var entry = compilationContext.FindEntry();

            Console.WriteLine("Entry is {0}.", entry.Name);

            var output = projectDirectory.CreateSubdirectory("CSharpToMpAsm.Compiler");
            var path = Path.ChangeExtension(Path.Combine(output.FullName, entry.Name), "asm");
            
            var code = compilationContext.CompileEntry(entry);

            File.WriteAllText(path, code);

            Console.WriteLine("Done");
        }

        private static CompilationContext Compile(SyntaxTree[] parseResults)
        {
            var context = new CompilationContext();
            context.Compile(parseResults);
            return context;
        }
    }
}
