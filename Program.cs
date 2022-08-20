using Microsoft.Extensions.DependencyInjection;
using TGWHomeTask.Services;

Console.WriteLine("Starting TGW Home Task...");

// 1. Create a dictionary for the configurations
IDictionary<string, string> configs = new Dictionary<string, string>();

// 2. Create service collection for DI and add dictionary and the config service
var services = new ServiceCollection();

services.AddSingleton<IDictionary<string, string>>(configs);
services.AddSingleton<IConfigService, ConfigService>();

IServiceProvider provider = services.BuildServiceProvider();


// 3. With the provider, you can pass the service to others classes
var service = provider.GetRequiredService<IConfigService>();

// 4. Test sample
TestLayers(service);

static void TestLayers(IConfigService service)
{
    string[] layers = new string[]
    {
        Path.Combine(Directory.GetCurrentDirectory(), "TestLayers", "layer_0.txt"),
        Path.Combine(Directory.GetCurrentDirectory(), "TestLayers", "layer_1.txt"),
        Path.Combine(Directory.GetCurrentDirectory(), "TestLayers", "layer_2.txt")
    };

    string[] ids = new string[]
    {
        "NumberOfSystems",
        "OrdersPerHour",
        "OrderLinesPerOrder",
        "ResultStartTime"
    };

    foreach (string layer in layers)
    {
        service.ParseConfigText(File.ReadAllText(layer));
    }

    foreach (string id in ids)
    {
        try
        {
            string val = service.GetConfig(id);
            Console.WriteLine($"{id}:{val}");
        }
        catch (System.Exception)
        {
            Console.WriteLine($"{id}:Error");
        }
    }
}





