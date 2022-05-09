using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace Boboom.Js;

public static class BoJsServiceExtends
{
    public static IServiceCollection AddBoboomJsService(this IServiceCollection services)
    {
        services.AddScoped(sp => new BoJsService(sp.GetRequiredService<IJSRuntime>()));
        services.AddScoped(typeof(BoJsPart<>), typeof(BoJsPart<>));
        services.AddScoped(typeof(BoJsPartLibrary<>), typeof(BoJsPartLibrary<>));
        return services;
    }
}
