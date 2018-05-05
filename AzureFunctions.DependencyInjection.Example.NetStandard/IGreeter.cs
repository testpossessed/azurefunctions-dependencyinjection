using System;

namespace AzureFunctions.DependencyInjection.Example.NetStandard
{
    public interface IGreeter
    {
        string Greet(string name);
    }
}
