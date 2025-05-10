using System.Runtime.CompilerServices;
using BloxSharp.Compiler;
using BloxSharp.Compiler.Engine;
using BloxSharp.Compiler.Interface;

namespace BloxSharp.Scratch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            ICodeEmitter emitter = new LuauCodeEmitter();
            IIRBuilder builder = new ReflectionIRBuilder(new LuauStatementTranslator(new LuauExpressionTranslator()));

            using (var compiler = new CompilationEngine(builder, emitter))
            {

            }
        }
    }
}
