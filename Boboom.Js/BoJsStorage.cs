using System.Text.Json;
using Microsoft.JSInterop;

namespace Boboom.Js;

public class BoJsStorage
{
    public string Sign { get; init; }
    public BoJsModule Storage { get; init; }
    public Lazy<Task<IJSObjectReference>> LocalTask { get; init; }
    public Lazy<Task<IJSObjectReference>> SessionTask { get; init; }

    public BoJsStorage(IJSRuntime jsRuntime, string sign)
    {
        Sign = sign;
        var assembly = typeof(BoJsStorage).Assembly;
        Storage = new BoJsModule(jsRuntime, assembly, "storage.js");
        LocalTask = new(() => Storage.InvokeAsync<IJSObjectReference>("newBoLocalStorage", Sign).AsTask());
        SessionTask = new(() => Storage.InvokeAsync<IJSObjectReference>("newBoSessionStorage", Sign).AsTask());
    }

    public async ValueTask<bool> HasLocalAsync(string name)
    {
        IJSObjectReference local = await LocalTask.Value;
        return await local.InvokeAsync<bool>("has", name);
    }

    public async ValueTask<T?> GetLocalAsync<T>(string name, T? defaultValue=default)
    {
        IJSObjectReference local = await LocalTask.Value;
        string? text = await local.InvokeAsync<string>("get", name, null);
        return text is null ? defaultValue : JsonSerializer.Deserialize<T>(text);
    }

    public async ValueTask SetLocalAsync<T>(string name, T? value, TimeSpan? duration=null)
    {
        IJSObjectReference local = await LocalTask.Value;
        int? durationMs = duration is null ? null : (int)duration.Value.TotalMilliseconds;
        string? text = value is null ? null : JsonSerializer.Serialize(value);
        await local.InvokeVoidAsync("set", name, text, durationMs);
    }

    public async ValueTask<bool> AddLocalExpireAsync(string name, TimeSpan span)
    {
        long stamp = (long)span.TotalMilliseconds;
        IJSObjectReference local = await LocalTask.Value;
        return await local.InvokeAsync<bool>(name, stamp);
    }

    public async ValueTask<bool> SetLocalExpireAsync(string name, DateTime time)
    {
        long stamp = (long)(time - new DateTime(1970, 1, 1)).TotalMilliseconds;
        IJSObjectReference local = await LocalTask.Value;
        return await local.InvokeAsync<bool>(name, stamp);
    }

    public async ValueTask<bool> HasSessionAsync(string name)
    {
        IJSObjectReference session = await SessionTask.Value;
        return await session.InvokeAsync<bool>("has", name);
    }

    public async ValueTask<T?> GetSessionAsync<T>(string name, T? defaultValue = default)
    {
        IJSObjectReference session = await SessionTask.Value;
        string? text = await session.InvokeAsync<string>("get", name, null);
        return text is null ? defaultValue : JsonSerializer.Deserialize<T>(text);
    }

    public async ValueTask SetSessionAsync<T>(string name, T? value, TimeSpan? duration = null)
    {
        IJSObjectReference session = await SessionTask.Value;
        int? durationMs = duration is null ? null : (int)duration.Value.TotalMilliseconds;
        string? text = value is null ? null : JsonSerializer.Serialize(value);
        await session.InvokeVoidAsync("set", name, text, durationMs);
    }

    public async ValueTask<bool> AddSessionExpireAsync(string name, TimeSpan span)
    {
        long stamp = (long)span.TotalMilliseconds;
        IJSObjectReference session = await SessionTask.Value;
        return await session.InvokeAsync<bool>(name, stamp);
    }

    public async ValueTask<bool> SetSessionExpireAsync(string name, DateTime time)
    {
        long stamp = (long)(time - new DateTime(1970, 1, 1)).TotalMilliseconds;
        IJSObjectReference session = await SessionTask.Value;
        return await session.InvokeAsync<bool>(name, stamp);
    }
}
