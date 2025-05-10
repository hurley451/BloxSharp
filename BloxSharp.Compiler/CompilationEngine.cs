using BloxSharp.Compiler.Interface;
using BloxSharp.Compiler.IR;

namespace BloxSharp.Compiler
{
    public class CompilationEngine : IDisposable
    {
        private readonly IIRBuilder _builder;
        private readonly ICodeEmitter _emitter;

        public CompilationEngine(IIRBuilder builder, ICodeEmitter emitter)
        {
            _builder = builder;
            _emitter = emitter;
        }

        public IRClass? Analyze(Type type)
        {
            return _builder.Build(type);
        }

        public string GenerateCode(IRClass ir)
        {
            return _emitter.Emit(ir);
        }

        public void Compile(Type type, string outputPath)
        {
            var ir = Analyze(type);
            if (ir == null)
                throw new InvalidOperationException($"Unable to analyze type: {type.FullName}");

            var code = GenerateCode(ir);
            File.WriteAllText(outputPath, code);
        }

        public void CompileAll(IEnumerable<Type> types, string outputDirectory)
        {
            foreach (var type in types)
            {
                var fileName = Path.Combine(outputDirectory, type.Name + ".lua");
                Compile(type, fileName);
            }
        }

        public void Dispose()
        {
            
        }
    }
}
