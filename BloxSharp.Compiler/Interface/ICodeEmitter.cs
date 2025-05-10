using BloxSharp.Compiler.IR;

namespace BloxSharp.Compiler.Interface;

public interface ICodeEmitter
{
    string Emit(IRClass ir);
}