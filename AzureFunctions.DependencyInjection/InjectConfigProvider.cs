using System;
using System.Linq;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctions.DependencyInjection
{
    public class InjectConfigProvider : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            var servicesCollection = new ServiceCollection();
            var serviceRegistrarLoader = new ServiceRegistrarLoader();
            var serviceRegistrars = serviceRegistrarLoader.Load();

            if(!serviceRegistrars.Any())
            {
                throw new InvalidOperationException("No Service Regristrars found, have you forgotten to add at least one to your host project");
            }

            foreach(var serviceRegistrar in serviceRegistrars)
            {
                serviceRegistrar.Register(servicesCollection);
            }

            var serviceProvider = servicesCollection.BuildServiceProvider();

            context.AddBindingRule<InjectAttribute>()
                   .Bind(new InjectBindingProvider(serviceProvider));


            var registry = context.Config.GetService<IExtensionRegistry>();
            var filter = new ScopeCleanupFilter();
            registry.RegisterExtension(typeof(IFunctionInvocationFilter), filter);
            registry.RegisterExtension(typeof(IFunctionExceptionFilter), filter);
        }
    }
}