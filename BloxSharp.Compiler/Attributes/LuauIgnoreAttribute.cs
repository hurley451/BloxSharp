namespace BloxSharp.Compiler.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method)]
public class LuauIgnoreAttribute : Attribute { }