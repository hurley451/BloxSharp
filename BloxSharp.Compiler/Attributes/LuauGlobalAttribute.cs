namespace BloxSharp.Compiler.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class LuauGlobalAttribute : Attribute
{
    public string Expression { get; }
    public LuauGlobalAttribute(string expression) => Expression = expression;
}