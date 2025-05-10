using BloxSharp.Compiler.Interface;
using BloxSharp.Compiler.IR;
using System.Text;

namespace BloxSharp.Compiler.Engine;

public class LuauCodeEmitter : ICodeEmitter
{
    public string Emit(IRClass ir)
    {
        var sb = new StringBuilder();
        var name = ir.Name;

        sb.AppendLine($"local {name} = {{}}");
        sb.AppendLine($"{name}.__index = {name}\n");

        var args = string.Join(", ", ir.Fields.Select(f => f.Name.ToLower()));
        sb.AppendLine($"function {name}.new({args})");
        sb.AppendLine($"    local self = setmetatable({{}}, {name})");
        foreach (var field in ir.Fields)
            sb.AppendLine($"    self.{field.Name} = {field.Name.ToLower()}");
        sb.AppendLine("    return self");
        sb.AppendLine("end\n");

        foreach (var method in ir.Methods)
        {
            sb.AppendLine($"function {name}:{method.Name}({string.Join(", ", method.Parameters)})");
            if (!string.IsNullOrWhiteSpace(method.Body))
            {
                foreach (var line in method.Body.Split('\n'))
                    sb.AppendLine("    " + line.Trim());
            }
            else
            {
                sb.AppendLine("    -- TODO: Transpile method body");
            }
            sb.AppendLine("end\n");
        }

        sb.AppendLine($"return {name}");
        return sb.ToString();
    }
}