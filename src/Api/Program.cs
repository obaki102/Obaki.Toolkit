
using Obaki.Toolkit.Application.Features.D4Timer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient<ID4HttpClient, D4HttpClient>();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseRouting();

app.MapHealthChecks("/health");
app.MapGet("/", (ID4HttpClient client) => client.GetUpcomingBoss());

app.Run();
