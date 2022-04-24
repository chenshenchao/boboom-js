﻿using Microsoft.JSInterop;

namespace Boboom.Js;

public class JsPartLibrary<T> : JsModule
{
    public JsPartLibrary(IJSRuntime jsRuntime) : base(jsRuntime, ResolvePartFilePath()) { }

    public static string ResolvePartFilePath()
    {
        Type type = typeof(T);
        string assembly = type.Assembly.GetName()!.Name!;
        string location = type.FullName!.Substring(assembly.Length).Trim('.').Replace('.', '/');
        return $"./_content/{assembly}/{location}.razor.js";
    }
}
