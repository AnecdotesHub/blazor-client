using Jevstafjev.Anecdotes.BlazorClient;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient(AppData.AnecdoteClientName,
    client => client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:AnecdoteApi"]!));

builder.Services.AddHttpClient(AppData.AnecdoteAuthClientName, client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:AnecdoteApi"]!);
}).AddHttpMessageHandler(sp => sp.GetRequiredService<AuthorizationMessageHandler>()
    .ConfigureHandler(
        authorizedUrls: [builder.Configuration["ServiceUrls:AnecdoteApi"]!]));

builder.Services.AddHttpClient(AppData.GeneratorAuthClientName, client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:AnecdoteGenerator"]!);
}).AddHttpMessageHandler(sp => sp.GetRequiredService<AuthorizationMessageHandler>()
    .ConfigureHandler(
        authorizedUrls: [builder.Configuration["ServiceUrls:AnecdoteGenerator"]!]));

builder.Services.AddHttpClient(AppData.SubscriberClientName,
    client => client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:SubscriberApi"]!));

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = builder.Configuration["OpenIDConnectSettings:Authority"];
    options.ProviderOptions.ClientId = builder.Configuration["OpenIDConnectSettings:ClientId"];
    options.ProviderOptions.DefaultScopes.Add("openid");
    options.ProviderOptions.DefaultScopes.Add("profile");
    options.ProviderOptions.DefaultScopes.Add("email");
    options.ProviderOptions.DefaultScopes.Add("AnecdoteApi");
    options.ProviderOptions.DefaultScopes.Add("AnecdoteGenerator");
    options.ProviderOptions.ResponseType = "code";
    options.UserOptions.RoleClaim = "role";
});

await builder.Build().RunAsync();
