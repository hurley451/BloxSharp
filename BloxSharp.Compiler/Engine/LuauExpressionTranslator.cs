using BloxSharp.Compiler.Interface;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BloxSharp.Compiler.Engine;

public class LuauExpressionTranslator : IExpressionTranslator
{
    public string Translate(ExpressionSyntax expr)
    {
        return expr switch
        {
            IdentifierNameSyntax id => id.Identifier.Text,
            MemberAccessExpressionSyntax ma when ma.Expression.ToString() == "this" => $"self.{ma.Name.Identifier.Text}",
            MemberAccessExpressionSyntax ma => $"{ma.Expression}.{ma.Name.Identifier.Text}",
            LiteralExpressionSyntax lit => lit.Token.Text,
            BinaryExpressionSyntax bin => $"{Translate(bin.Left)} {bin.OperatorToken.Text} {Translate(bin.Right)}",
            InvocationExpressionSyntax call => TranslateCall(call),
            _ => "-- expr"
        };
    }

    private string TranslateCall(InvocationExpressionSyntax call)
    {
        if (call.Expression is MemberAccessExpressionSyntax ma)
        {
            var baseExpr = Translate(ma.Expression);
            var method = ma.Name.Identifier.Text;
            var args = string.Join(", ", call.ArgumentList.Arguments.Select(a => Translate(a.Expression)));
            return $"{baseExpr}:{method}({args})";
        }
        return "-- call";
    }
}