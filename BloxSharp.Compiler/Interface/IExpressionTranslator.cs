using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BloxSharp.Compiler.Interface;

public interface IExpressionTranslator
{
    string Translate(ExpressionSyntax expression);
}
