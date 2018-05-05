using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AzureFunctions.DependencyInjection
{
    internal class ServiceRegistrarLoader
    {
        public IList<IServiceRegistrar> Load()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
                .Where(x => typeof(IServiceRegistrar).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IServiceRegistrar>()
                .ToList();
        }
    }
}
