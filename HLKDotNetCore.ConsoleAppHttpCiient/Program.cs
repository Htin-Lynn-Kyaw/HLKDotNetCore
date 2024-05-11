using HLKDotNetCore.ConsoleAppHttpCiient;

HttpClientExample httpClientExample = new HttpClientExample();
//httpClientExample.ReadAsync().Wait();
httpClientExample.RunAsync().Wait();
Console.ReadLine();



