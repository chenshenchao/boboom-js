using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.JSInterop;

namespace Boboom.Js;

public class JsPart<T> : JsModule
{
    public JsPart(IJSRuntime jsRuntime) : base(jsRuntime, ResolvePartSource()) { }

    public static string ResolvePartSource()
    {
        Type type = typeof(T);
        string assembly = type.Assembly.GetName()!.Name!;
        string location = type.FullName!.Substring(assembly.Length).Trim('.').Replace('.', '/');
        return $"./{location}.razor.js";
    }
}
