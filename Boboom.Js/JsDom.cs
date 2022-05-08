using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Boboom.Js.Dom;

namespace Boboom.Js;

public class JsDom
{
    public JsModule Anchor { get; init; }
    public JsDom(IJSRuntime jsRuntime)
    {
        var assembly = typeof(JsDom).Assembly;
        Anchor = new JsModule(jsRuntime, assembly, "anchor.js");
    }

    public async ValueTask<JsPosition> GetPosition(IJSObjectReference element)
    {
        return await Anchor.InvokeAsync<JsPosition>("getPosition", element);
    }

    public async ValueTask<JsPosition> GetPosition(ElementReference element)
    {
        return await Anchor.InvokeAsync<JsPosition>("getPosition", element);
    }

    public async ValueTask SetPosition(IJSObjectReference element, JsPosition position)
    {
        await Anchor.InvokeVoidAsync("setPosition", element, position);
    }

    public async ValueTask SetPosition(ElementReference element, JsPosition position)
    {
        await Anchor.InvokeVoidAsync("setPosition", element, position);
    }

    public async ValueTask<JsSize> GetSize(IJSObjectReference element)
    {
        return await Anchor.InvokeAsync<JsSize>("getSize", element);
    }

    public async ValueTask<JsSize> GetSize(ElementReference element)
    {
        return await Anchor.InvokeAsync<JsSize>("getSize", element);
    }

    public async ValueTask SetSize(IJSObjectReference element, JsSize size)
    {
        await Anchor.InvokeVoidAsync("setSize", element, size);
    }

    public async ValueTask SetSize(ElementReference element, JsSize size)
    {
        await Anchor.InvokeVoidAsync("setSize", element, size);
    }

    public async ValueTask<JsSize> GetContentSize(ElementReference element)
    {
        return await Anchor.InvokeAsync<JsSize>("getContentSize", element);
    }

    public async ValueTask<JsSize> GetContentPosition(ElementReference element)
    {
        return await Anchor.InvokeAsync<JsSize>("getContentPosition", element);
    }
}
