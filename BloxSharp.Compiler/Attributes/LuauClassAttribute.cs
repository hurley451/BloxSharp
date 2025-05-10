namespace BloxSharp.Compiler.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class LuauClassAttribute : Attribute
{
    public string Name { get; }
    public LuauClassAttribute(string name) => Name = name;
}