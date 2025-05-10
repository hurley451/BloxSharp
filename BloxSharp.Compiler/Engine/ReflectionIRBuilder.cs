using System.Reflection;
using BloxSharp.Compiler.Attributes;
using BloxSharp.Compiler.Interface;
using BloxSharp.Compiler.IR;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BloxSharp.Compiler.Engine;

public class ReflectionIRBuilder : IIRBuilder
{
    private readonly IStatementTranslator _statementTranslator;

    public ReflectionIRBuilder(IStatementTranslator statementTranslator)
    {
        _statementTranslator = statementTranslator;
    }

    public IRClass? Build(Type type)
    {
        var classAttr = type.GetCustomAttribute<LuauClassAttribute>();
        if (classAttr == null) return null;

        var irClass = new IRClass
        {
            Name = classAttr.Name,
            Fields = type.GetProperties()
                .Where(p => p.IsDefined(typeof(LuauFieldAttribute), true))
                .Select(p => new IRField { Name = p.Name, Type = p.PropertyType.Name })
                .ToList(),
            Methods = type.GetMethods()
                .Where(m => m.IsDefined(typeof(LuauMethodAttribute), true))
                .Select(m => new IRMethod
                {
                    Name = m.Name,
                    Parameters = m.GetParameters().Select(p => p.Name ?? "arg").ToList(),
                    Body = TranslateMethodBody(type, m)
                })
                .ToList()
        };

        return irClass;
    }

    private string? TranslateMethodBody(Type type, MethodInfo method)
    {
        var fileName = type.Name + ".cs";
        if (!File.Exists(fileName)) return "-- TODO: Missing source file";

        var tree = CSharpSyntaxTree.ParseText(File.ReadAllText(fileName));
        var root = tree.GetRoot();

        var methodSyntax = root.DescendantNodes().OfType<MethodDeclarationSyntax>()
            .FirstOrDefault(m => m.Identifier.Text == method.Name);

        if (methodSyntax?.Body == null) return "-- TODO: No method body";

        var sb = new System.Text.StringBuilder();
        foreach (var stmt in methodSyntax.Body.Statements)
        {
            sb.AppendLine(_statementTranslator.Translate(stmt));
        }

        return sb.ToString().TrimEnd();
    }
}