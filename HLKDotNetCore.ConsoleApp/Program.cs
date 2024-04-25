using HLKDotNetCore.ConsoleApp;


DapperExample dapperExample = new DapperExample();
dapperExample.Run();

EFCoreExample eFCoreExample = new EFCoreExample();
eFCoreExample.Run();






// ####### FULLY FUNCTIONING CRUD ####### 

//using HLKDotNetCore.ConsoleApp;

//bool programStatus = true;
//while (programStatus)
//{
//    //AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//    //DapperExample dapperExample = new DapperExample();
//    EFCoreExample eFCoreExample = new EFCoreExample();  
//    string? title, author, conntent, ID;

//    Console.WriteLine("Enter an operation....");
//    string? userInput = Console.ReadLine();

//    switch (userInput)
//    {
//        case "read":
//            Console.WriteLine("Reading data...");
//            //adoDotNetExample.Read();
//            //dapperExample.Run("Read", 0, "", "", "");
//            eFCoreExample.Run("Read", 0, "", "", "");
//            break;

//        case "create":

//            Console.Write("Title ==>");
//            title = Console.ReadLine();
//            Console.Write("Author ==>");
//            author = Console.ReadLine();
//            Console.Write("Content ==>");
//            conntent = Console.ReadLine();

//            Console.WriteLine("Creating data...");
//            //adoDotNetExample.Create(title, author, conntent);
//            //dapperExample.Run("Create", 0, title, author, conntent);
//            eFCoreExample.Run("Create", 0, title, author, conntent);
//            break;

//        case "update":

//            Console.Write("ID ==>");
//            ID = Console.ReadLine();
//            Console.Write("Title ==>");
//            title = Console.ReadLine();
//            Console.Write("Author ==>");
//            author = Console.ReadLine();
//            Console.Write("Content ==>");
//            conntent = Console.ReadLine();

//            Console.WriteLine("updating data...");

//            if (int.TryParse(ID, out int uNumber))
//            {
//                //adoDotNetExample.Update(uNumber, title, author, conntent);
//                //dapperExample.Run("Update", uNumber, title, author, conntent);
//                eFCoreExample.Run("Update", uNumber, title, author, conntent);
//            }
//            else
//            {
//                Console.WriteLine("ID must be number value!");
//                break;
//            }
//            break;

//        case "delete":

//            Console.Write("ID ==>");
//            ID = Console.ReadLine();

//            Console.WriteLine("deleting data...");
//            if (int.TryParse(ID, out int dNumber))
//            {
//                //adoDotNetExample.Delete(dNumber);
//                //dapperExample.Run("Delete", dNumber, "", "", "");
//                eFCoreExample.Run("Delete", dNumber, "", "", "");
//            }
//            else
//            {
//                Console.WriteLine("ID must be number value!");
//                break;
//            }
//            break;

//        case "edit":

//            Console.Write("ID ==>");
//            ID = Console.ReadLine();

//            Console.WriteLine("editing data...");
//            if (int.TryParse(ID, out int eNumber))
//            {
//                //adoDotNetExample.Edit(eNumber);
//                //dapperExample.Run("Edit", eNumber, "", "", "");
//                eFCoreExample.Run("Edit", eNumber, "", "", "");
//            }
//            else
//            {
//                Console.WriteLine("ID must be number value!");
//                break;
//            }
//            break;

//        case "exit":

//            Console.WriteLine("exiting program...");
//            programStatus = false;
//            break;

//        default:
//            Console.WriteLine("Invalid input.\n" +
//                                "Input should be\n" +
//                                "=> read\n" +
//                                "=> create\n" +
//                                "=> update\n" +
//                                "=> delete\n" +
//                                "=> edit\n" +
//                                "=> exit");
//            break;
//    }
//    //Console.ReadKey();
//}
