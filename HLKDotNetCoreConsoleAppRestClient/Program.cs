using HLKDotNetCore.ConsoleAppRestClient;

Console.WriteLine("Hello, World!");

RestClientExample restClientExample = new RestClientExample();
await restClientExample.RunAsync();

Console.ReadKey();