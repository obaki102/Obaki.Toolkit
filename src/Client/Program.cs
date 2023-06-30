using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Obaki.Toolkit.Client;
using MudBlazor.Services;
using Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;
using Obaki.Toolkit.Application.Features.D4Timer;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<ID4HttpClient, D4HttpClientWithProxy>();
builder.Services.AddTextEditorServiceAsSingleton();
builder.Services.AddMudServices();
if (builder.HostEnvironment.Environment == "Development")
{
    builder.Logging.SetMinimumLevel(LogLevel.Debug);
}
else
{
    builder.Logging.SetMinimumLevel(LogLevel.None);
}
var app = builder.Build();
await app.RunAsync();
