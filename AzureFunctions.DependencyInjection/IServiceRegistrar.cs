using System;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctions.DependencyInjection
{
    public interface IServiceRegistrar
    {
        void Register(IServiceCollection serviceCollection);
    }
}
