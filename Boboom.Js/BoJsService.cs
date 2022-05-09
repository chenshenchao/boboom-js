using System.Reflection;
using Microsoft.JSInterop;

namespace Boboom.Js;

public class BoJsService
{
    public IJSRuntime JsRuntime { get; init; }
    public BoJsService(IJSRuntime jsRuntime)
    {
        JsRuntime = jsRuntime;
    }

    public BoJsModule Import(string source)
    {
        return new BoJsModule(JsRuntime, source);
    }

    public BoJsModule Import(Assembly assembly, string source)
    {
        return new BoJsModule(JsRuntime, assembly, source);
    }
}
