using Microsoft.JSInterop;

namespace Boboom.Js;

public class BoJsPart<T> : BoJsModule
{
    public BoJsPart(IJSRuntime jsRuntime) : base(jsRuntime, ResolvePartSource()) { }

    public static string ResolvePartSource()
    {
        Type type = typeof(T);
        string assembly = type.Assembly.GetName()!.Name!;
        string location = type.FullName!.Substring(assembly.Length).Trim('.').Replace('.', '/');
        return $"./{location}.razor.js";
    }
}
