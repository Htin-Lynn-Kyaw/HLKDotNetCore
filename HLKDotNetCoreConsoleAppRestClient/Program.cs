using HLKDotNetCore.ConsoleAppRestClient;

Console.WriteLine("Hello, World!");

RestClientExample restClientExample = new RestClientExample();
restClientExample.ReadAsync().Wait();
