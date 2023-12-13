using Npgsql;
using Holidaymaker3;
using System.Runtime.CompilerServices;

string dbUri = "Host=localhost;Port=5455;Username=postgres;Password=postgres;Database=holidaymaker;";
        
await using var db = NpgsqlDataSource.Create(dbUri);


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
    
    //PopulateTables populate
    var PopulateDatabase = new PopulateDatabase(db);
    Console.WriteLine(await PopulateTables.PopulateDatabase());
    //await Script.MakeTables();

}
#endregion

