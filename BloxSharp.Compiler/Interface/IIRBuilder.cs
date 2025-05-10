using BloxSharp.Compiler.IR;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BloxSharp.Compiler.Interface;

public interface IIRBuilder
{
    IRClass? Build(Type type);
}