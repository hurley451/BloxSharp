namespace BloxSharp.Compiler.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class LuauNamespaceAttribute : Attribute
{
    public string Path { get; }
    public LuauNamespaceAttribute(string path) => Path = path;
}