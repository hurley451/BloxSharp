using BloxSharp.Compiler.Interface;

namespace BloxSharp.Compiler.Utils;

public class DefaultSourceLocator : ISourceLocator
{
    private readonly string _basePath;

    public DefaultSourceLocator(string basePath)
    {
        _basePath = basePath;
    }

    public string? Locate(Type type, string? fallbackFileName = null)
    {
        var fileName = fallbackFileName ?? type.Name + ".cs";

        // Attempt exact match at root
        var rootPath = Path.Combine(_basePath, fileName);
        if (File.Exists(rootPath)) return rootPath;

        // Otherwise, search recursively under basePath
        var files = Directory.GetFiles(_basePath, fileName, SearchOption.AllDirectories);
        return files.Length > 0 ? files[0] : null;
    }

    public static string GetProjectRoot()
    {

#if DEBUG
        var dir = AppContext.BaseDirectory;
        var parent = Directory.GetParent(dir)?.Parent?.Parent?.Parent;
        if (parent == null)
            throw new InvalidOperationException("Unable to determine project root from base directory.");
        return parent.FullName;
#else
            return AppContext.BaseDirectory;
#endif
    }
}