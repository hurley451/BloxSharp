namespace BloxSharp.Compiler.Interface
{
    public interface ISourceLocator
    {
        string? Locate(Type type, string? fallbackFileName = null);
    }
}