using BlogApplication;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlogApplication.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using Blazored.TextEditor;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register HTTP Client for API Calls
builder.Services.AddHttpClient("BlogApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7204"); // Change if your API is hosted elsewhere
});

// Ensure only one instance of HttpClient is registered
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register MudBlazor
builder.Services.AddMudServices();

// Register Authentication Services
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProvider>(); // Set Scoped
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

// Register Supabase Service as Scoped to avoid Singleton issues
builder.Services.AddScoped<SupabaseService>();

await builder.Build().RunAsync();
