using InformationProcessSupport.Web;
using InformationProcessSupport.Web.Services;
using InformationProcessSupport.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44394") })
    .AddScoped<IScheduleServices, ScheduleServices>();
builder.Services.AddScoped(sp => new ChangeEventArgs());

await builder.Build().RunAsync();
