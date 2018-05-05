using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctions.DependencyInjection
{
    public class InjectBindingProvider : IBindingProvider
    {
        public static readonly ConcurrentDictionary<Guid, IServiceScope> Scopes =
            new ConcurrentDictionary<Guid, IServiceScope>();

        private readonly IServiceProvider serviceProvider;

        public InjectBindingProvider(IServiceProvider serviceProvider) =>
            this.serviceProvider = serviceProvider;

        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            IBinding binding = new InjectBinding(this.serviceProvider, context.Parameter.ParameterType);
            return Task.FromResult(binding);
        }
    }
}