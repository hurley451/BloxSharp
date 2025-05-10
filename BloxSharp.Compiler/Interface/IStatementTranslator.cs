using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BloxSharp.Compiler.Interface;

public interface IStatementTranslator
{
    string Translate(StatementSyntax statement);
}