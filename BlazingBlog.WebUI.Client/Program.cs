using BlazorCleanArchitecture.Application.Articles;
using BlazorCleanArchitecture.WebUI.Client.Features.Articles;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IArticlesViewService, MyArticlesService>();

await builder.Build().RunAsync();
