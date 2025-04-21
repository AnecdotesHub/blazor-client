using Jevstafjev.Anecdotes.BlazorClient;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = builder.Configuration["OpenIDConnectSettings:Authority"];
    options.ProviderOptions.ClientId = builder.Configuration["OpenIDConnectSettings:ClientId"];
    options.ProviderOptions.DefaultScopes.Add("openid");
    options.ProviderOptions.DefaultScopes.Add("profile");
    options.ProviderOptions.DefaultScopes.Add("email");
    options.ProviderOptions.DefaultScopes.Add("AnecdoteApi");
    options.ProviderOptions.ResponseType = "code";
    options.UserOptions.RoleClaim = "role";
});

await builder.Build().RunAsync();
