using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GhsflUtils;
using GhsflUtils.Services;
using GhsflUtils.Shared;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<AuthProvider>();
builder.Services.AddScoped<CsvService>();
builder.Services.AddScoped<PointsService>();
builder.Services.AddScoped<ClubService>();
builder.Services.AddScoped<RosterService>();
builder.Services.AddScoped<RoundService>();
builder.Services.AddScoped<FencerService>();

builder.Services.AddMudServices();

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Okta", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
});

await builder.Build().RunAsync();