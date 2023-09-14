using Blazored.LocalStorage;
using IgniteUI.Blazor.Controls;
using InformationProcessSupport.Web;
using InformationProcessSupport.Web.AuthProviders;
using InformationProcessSupport.Web.Services;
using InformationProcessSupport.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44394") })
    .AddScoped<IScheduleServices, ScheduleServices>()
    .AddScoped<IDatabaseServices, DatabaseServices>()
    .AddScoped<IStatisticServices, StatisticServices>()
    .AddScoped<IModalService, ModalService>()
    .AddBlazoredLocalStorage()
    .AddScoped<AuthenticationStateProvider, AuthStateProvider>()
    .AddScoped<IAuthenticationService, AuthenticationService>()
    .AddAuthorizationCore();
builder.Services.AddScoped(sp => new ChangeEventArgs());
builder.Services.AddIgniteUIBlazor(typeof(IgbCircularProgressModule));

await builder.Build().RunAsync();
