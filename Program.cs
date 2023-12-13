using Npgsql;
using makedatabase;
using System.Runtime.CompilerServices;

#region CreateDatabaseMenu
Console.Clear();
Console.WriteLine("do you want to create the database?");
string userinput = Console.ReadLine();
if ("y" == userinput || "" == userinput){
    Console.WriteLine("testtest");
    await Script.CreateDatabase();
}
Console.WriteLine("do you want to create all the tables?");
userinput = Console.ReadLine();
if ("y" == userinput || "" == userinput){
    Console.WriteLine("testtest");
    await Script.MakeTables();
}
Console.WriteLine("do you want to populate the database");
userinput = Console.ReadLine();
if ("y" == userinput || "" == userinput){
    Console.WriteLine("testtest");
    await Script.PopulateDatabase();
}
#endregion

