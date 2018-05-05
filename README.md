### AzureFunction.DependencyInjection

First all credit goes to Boris Wilhelms and Holger Leichsenring for their research and blogs.  This library combines their work to provide support for Dependency Injection in the .NET Core version of Azure Functions.

#### Important!!!
This library will not currently work in an Azure Functions project that targets .NET Framework.  A .NETFx project still depends on WebJobs 2.0 while .NET Core projects  depend on the new WebJobs 3.* betas.  Once Azure Functions .NET Core is released and tooling is updated we will test and updated this project as necessary to make sure it works in both flavours.

#### Installing
Whilst you are welcome to fork or clone this project the best way to make use of it is via the NuGet package either by using your IDE or one of the following:

##### NuGet Package Manager
```bash
	install-package AzureFunctions.DependencyInjection
```

##### .NET Cli
```bash
	dotnet add package AzureFunctions.DependencyInjection
```

#### Registering Services
The InectConfigProvider included in the library will automatically discover and register your services through one or more implementations of IServiceRegistrar that you need to create.  From the example project in this repository:

```csharp
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

```

#### Usage
With an implementation of IServiceRegistrar in place you can now use the Inject attribute to inject components into your Azure Function.  From the sample project in this repository:

```csharp
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace AzureFunctions.DependencyInjection.Example.NetStandard
{
    public static class GreeterFunction
    {
        [FunctionName("Greeter")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req,
			 TraceWriter log,
			[Inject]IGreeter greeter)
        {
            log.Info("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            return name != null
                ? (ActionResult)new OkObjectResult(greeter.Greet(name))
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}

```

Hope you find this useful, if you have any questions or issues please feel free to post in this repository.
