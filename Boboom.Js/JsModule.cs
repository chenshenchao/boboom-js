using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.JSInterop;

namespace Boboom.Js;

/// <summary>
/// 
/// </summary>
public class JsModule : IAsyncDisposable
{
    public string Source { get; init; }
    public Lazy<Task<IJSObjectReference>> ModuleTask { get; init; }

    public JsModule(IJSRuntime jsRuntime, string source)
    {
        Source = source;
        ModuleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", Source).AsTask());
    }

    public JsModule(IJSRuntime jsRuntime, Assembly assembly, string filePath)
        : this(jsRuntime, ResolveSourceLibrary(assembly, filePath)) { }

    public async ValueTask DisposeAsync()
    {
        if (ModuleTask.IsValueCreated)
        {
            var module = await ModuleTask.Value;
            await module.DisposeAsync();
        }
    }

    public static string ResolveSourceLibrary(Assembly assembly, string filePath)
    {
        return $"./_content/{assembly.GetName().Name}/{filePath}";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="R"></typeparam>
    /// <param name="identifier"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async ValueTask<R> InvokeAsync<R>(string identifier, params object?[]? args)
    {
        IJSObjectReference module = await ModuleTask.Value;
        return await module.InvokeAsync<R>(identifier, args);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async ValueTask InvokeVoidAsync(string identifier, params object?[]? args)
    {
        IJSObjectReference module = await ModuleTask.Value;
        await module.InvokeVoidAsync(identifier, args);
    }
}
