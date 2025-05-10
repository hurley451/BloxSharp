namespace BloxSharp.Compiler.IR;

public class IRMethod
{
    public string Name { get; set; } = string.Empty;
    public List<string> Parameters { get; set; } = new();
    public string? Body { get; set; } = null; // Optional translated body
}