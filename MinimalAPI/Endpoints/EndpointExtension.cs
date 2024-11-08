using System.Reflection;

namespace MinimalAPI.Endpoints;

public static class EndpointExtension
{
    public static void AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        var endpointTypes = assembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && typeof(IEndpoint).IsAssignableFrom(t));

        foreach (var type in endpointTypes)
        {
            services.AddScoped(typeof(IEndpoint), type);
        }
    }

    public static void MapEndpoints(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var scopeServices = scope.ServiceProvider;
        var endpoints = scopeServices.GetServices<IEndpoint>();
        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(app);
        }
    }
}
