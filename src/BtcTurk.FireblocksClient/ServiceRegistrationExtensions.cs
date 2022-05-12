namespace BtcTurk.FireblocksClient;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddFireblocksClient(this IServiceCollection services, string apikey,  string privateKey, string baseAddress = "https://api.fireblocks.io")
    {
        services.AddHttpClient<FireblocksClient>(c =>
        {
            c.BaseAddress = new Uri(baseAddress);
            c.DefaultRequestHeaders.Add("Accept", "application/json");
        }).AddHttpMessageHandler<AuthDelegatingHandler>();
        
        services.AddTransient<AuthDelegatingHandler>(s => {
            var bearerTokenProvider = s.GetRequiredService<BearerTokenProvider>();
            return new AuthDelegatingHandler(bearerTokenProvider);
        });
        
        services.AddTransient(_ => new BearerTokenProvider(apikey, privateKey));

        return services;
    }
}