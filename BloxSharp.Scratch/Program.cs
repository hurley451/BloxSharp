using System.Runtime.CompilerServices;
using BloxSharp.Compiler;
using BloxSharp.Compiler.Engine;
using BloxSharp.Compiler.Interface;
using BloxSharp.Compiler.Utils;
using Samples;

namespace BloxSharp.Scratch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            ICodeEmitter emitter = new LuauCodeEmitter();
            ISourceLocator sourceLocator = new DefaultSourceLocator(DefaultSourceLocator.GetProjectRoot());
            IIRBuilder builder = new ReflectionIRBuilder(new LuauStatementTranslator(new LuauExpressionTranslator()),sourceLocator);

            using (var compiler = new CompilationEngine(builder, emitter))
            {
                compiler.Compile(typeof(Person),"person.luau");
            }
        }
    }
}
