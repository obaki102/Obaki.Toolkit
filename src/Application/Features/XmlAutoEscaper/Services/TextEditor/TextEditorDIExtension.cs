using Microsoft.Extensions.DependencyInjection;
namespace Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;
public static class DataCheckerDIExtensions
{
    public static IServiceCollection AddTextEditorServiceAsScoped(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddScoped(typeof(ITextEditor<>), typeof(TextEditor<>));
        return services;
    }

    public static IServiceCollection AddTextEditorServiceAsSingleton(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddSingleton(typeof(ITextEditor<>), typeof(TextEditor<>));
        return services;

    }
}