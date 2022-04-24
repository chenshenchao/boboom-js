using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.JSInterop;

namespace Boboom.Js;

public class JsService
{
    public IJSRuntime JsRuntime { get; init; }
    public JsService(IJSRuntime jsRuntime)
    {
        JsRuntime = jsRuntime;
    }

    public JsModule Import(string source)
    {
        return new JsModule(JsRuntime, source);
    }

    public JsModule Import(Assembly assembly, string source)
    {
        return new JsModule(JsRuntime, assembly, source);
    }
}
