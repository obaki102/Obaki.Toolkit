using Microsoft.Extensions.DependencyInjection;
using Obaki.Toolkit.Application.Features.XmlAutoEscaper.Services.TextEditor;
namespace Obaki.Toolkit.Test;
 public class TextEditorDiExtensionTest
    {
        [Fact]
        public void DependencyInjection_CheckIfScopedServiceIsProperlyRegistered_ShouldNotReturnNull()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddTextEditorServiceAsScoped();
            var serviceProvider = services.BuildServiceProvider();

            var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
            var scope = scopeFactory.CreateScope();
        
            // Act
            var myService = serviceProvider.GetService<ITextEditor<string>>();
            var scopeService = scope.ServiceProvider.GetService<ITextEditor<string>>();

            // Assert
            Assert.NotNull(myService);
            Assert.IsType(typeof(TextEditor<string>),myService);
            //Not equals means the service that was injected is a scope service
            Assert.NotEqual(scopeService, myService);
        }

        [Fact]
        public void DependencyInjection_CheckIfSingletonServiceIsProperlyRegistered_ShouldNotReturnNull()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddTextEditorServiceAsSingleton();
            var serviceProvider = services.BuildServiceProvider();

            // Act
            var myService = serviceProvider.GetService<ITextEditor<string>>();
            var singletonService = serviceProvider.GetService<ITextEditor<string>>();

            // Assert
            Assert.NotNull(myService);
            Assert.IsType(typeof(TextEditor<string>), myService);
            //Equals means the service that was injected is a singleton service
            Assert.Equal(singletonService, myService);
        }
    }