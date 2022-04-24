using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace Boboom.Js;

public static class JsServiceExtends
{
    public static IServiceCollection AddBoboomJsService(this IServiceCollection services)
    {
        services.AddScoped(sp => new JsService(sp.GetRequiredService<IJSRuntime>()));
        services.AddScoped(typeof(JsPart<>), typeof(JsPart<>));
        services.AddScoped(typeof(JsPartLibrary<>), typeof(JsPartLibrary<>));
        return services;
    }
}
