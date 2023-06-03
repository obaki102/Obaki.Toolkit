using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Obaki.Toolkit.Client;
using MudBlazor.Services;
using Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTextEditorServiceAsSingleton();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
