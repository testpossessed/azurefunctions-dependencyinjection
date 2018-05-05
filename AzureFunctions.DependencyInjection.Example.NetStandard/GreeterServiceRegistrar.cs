using System;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctions.DependencyInjection.Example.NetStandard
{
    public class GreeterServiceRegistrar: IServiceRegistrar
    {
        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IGreeter, Greeter>();
        }
    }
}
