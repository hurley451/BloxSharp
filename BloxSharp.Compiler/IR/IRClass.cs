namespace BloxSharp.Compiler.IR;

public class IRClass
{
    public string Name { get; set; } = string.Empty;
    public List<IRField> Fields { get; set; } = new();
    public List<IRMethod> Methods { get; set; } = new();
}