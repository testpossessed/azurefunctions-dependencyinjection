using System;

namespace AzureFunctions.DependencyInjection.Example.NetStandard
{
    public class Greeter : IGreeter
    {
        public string Greet(string name)
        {
            return $"Hello {name}!";
        }
    }
}
