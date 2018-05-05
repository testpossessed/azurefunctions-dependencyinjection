using System;
using Microsoft.Azure.WebJobs.Description;

namespace AzureFunctions.DependencyInjection
{
    [Binding]
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class InjectAttribute : Attribute
    {
    }
}
