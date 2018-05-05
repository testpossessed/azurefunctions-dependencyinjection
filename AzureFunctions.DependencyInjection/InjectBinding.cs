using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Protocols;

namespace AzureFunctions.DependencyInjection
{
    public class InjectBinding : IBinding
    {
        private readonly Type targetType;
        private readonly IServiceProvider serviceProvider;

        public InjectBinding(IServiceProvider serviceProvider, Type targetType)
        {
            this.targetType = targetType;
            this.serviceProvider = serviceProvider;
        }

        public bool FromAttribute => true;

        public Task<IValueProvider> BindAsync(object value, ValueBindingContext context) =>
            Task.FromResult((IValueProvider)new InjectValueProvider(value));

        public async Task<IValueProvider> BindAsync(BindingContext context)
        {
            await Task.Yield();
            var value = this.serviceProvider.GetService(this.targetType);
            return await this.BindAsync(value, context.ValueContext);
        }

        public ParameterDescriptor ToParameterDescriptor() => new ParameterDescriptor();

        private class InjectValueProvider : IValueProvider
        {
            private readonly object value;

            public InjectValueProvider(object value) => this.value = value;

            public Type Type => this.value.GetType();

            public Task<object> GetValueAsync() => Task.FromResult(this.value);

            public string ToInvokeString() => this.value.ToString();
        }
    }
}
