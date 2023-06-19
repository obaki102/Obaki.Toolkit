using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Obaki.Toolkit.Client;
using MudBlazor.Services;
using Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;
using Obaki.Toolkit.Application.Features.D4Timer;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<ID4HttpClient, D4HttpClient>();
builder.Services.AddTextEditorServiceAsSingleton();
builder.Services.AddMudServices();
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader());
// }); 
var app = builder.Build();
// app.UseCors("Open");
await app.RunAsync();
