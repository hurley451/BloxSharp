using BloxSharp.Compiler.Interface;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace BloxSharp.Compiler.Engine;

public class LuauStatementTranslator : IStatementTranslator
{
    private readonly IExpressionTranslator _expr;

    public LuauStatementTranslator(IExpressionTranslator expr)
    {
        _expr = expr;
    }

    public string Translate(StatementSyntax stmt)
    {
        return stmt switch
        {
            ReturnStatementSyntax ret => "return " + _expr.Translate(ret.Expression!),
            ExpressionStatementSyntax exprStmt => _expr.Translate(exprStmt.Expression),
            IfStatementSyntax ifStmt => TranslateIf(ifStmt),
            _ => "-- stmt"
        };
    }

    private string TranslateIf(IfStatementSyntax ifStmt)
    {
        var cond = _expr.Translate(ifStmt.Condition);
        var thenStmt = Translate(ifStmt.Statement);
        var elseStmt = ifStmt.Else != null ? Translate(ifStmt.Else.Statement) : null;

        var sb = new StringBuilder();
        sb.AppendLine($"if {cond} then");
        sb.AppendLine("    " + thenStmt);
        if (elseStmt != null)
        {
            sb.AppendLine("else");
            sb.AppendLine("    " + elseStmt);
        }
        sb.Append("end");
        return sb.ToString();
    }
}
