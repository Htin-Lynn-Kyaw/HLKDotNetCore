using HLKDotNetCore.ConsoleApp;

bool programStatus = true;
while(programStatus)
{
    AdoDotNetExample adoDotNetExample = new AdoDotNetExample();

    Console.WriteLine("Enter an operation....");
    string? userInput = Console.ReadLine();

    switch (userInput)
    {
        case "read":
            Console.WriteLine("Reading data...");
            adoDotNetExample.Read();
            break;
        case "create":
            Console.WriteLine("Creating data...");
            adoDotNetExample.Create("Java", "Mosh", "Java Web Devlopement");
            break;
        case "update":
            Console.WriteLine("updating data...");
            adoDotNetExample.Update(1, "C#", "Kumar", "Asp.net Core");
            break;
        case "delete":
            Console.WriteLine("deleting data...");
            adoDotNetExample.Delete(4);
            break;
        case "edit":
            Console.WriteLine("editing data...");
            adoDotNetExample.Edit(5);
            break;
        case "exit":
            Console.WriteLine("exiting program...");
            programStatus = false;
            break;
        default:
            Console.WriteLine("Invalid input.\n" +
                                "Input should be\n" +
                                "=> read\n" +
                                "=> create\n" +
                                "=> update\n" +
                                "=> delete\n" +
                                "=> edit\n"+
                                "=> exit");
            break;
    }
    Console.ReadKey();
}
