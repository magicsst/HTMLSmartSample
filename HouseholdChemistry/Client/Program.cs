global using Microsoft.AspNetCore.Components.Authorization;
global using HouseholdChemistry.Shared;
global using System.Net.Http.Json;

using HouseholdChemistry.Client.Services.MeetingService;
using HouseholdChemistry.Client;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.SessionStorage;

using Smart.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(
    sp => new HttpClient { 
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
    }
);

builder.Services.AddBlazoredSessionStorage();

builder.Services.AddOptions();

builder.Services.AddScoped<IMeetingService, MeetingService>();

builder.Services.AddSmart();

await builder.Build().RunAsync();