using Npgsql;
using Holidaymaker3;
using System.Runtime.CompilerServices;

const string dbUri = "Host=localhost;Port=5455;Username=postgres;Password=postgres;Database=holidaymaker;";
await using var db = NpgsqlDataSource.Create(dbUri);

var databaseCreator = new DatabaseCreator(db);
//await databaseCreator.CreateDatabase();
await databaseCreator.CreateTables();

var databasehelper = new DatabaseHelper(db);
await databasehelper.PopulateCustomersTable();
await databasehelper.PopulateHotelsTable();
await databasehelper.PopulateRoomsTable();
await databasehelper.PopulateHotelxRooms();
await databasehelper.PopulateAmenityTable();
await databasehelper.PopulateExtraTable();
await databasehelper.PopulateHotelsxAmenities();

//#region CreateDatabaseMenu
//Console.Clear();
//Console.WriteLine("do you want to create the database?");
//string userinput = Console.ReadLine();
//if ("y" == userinput || "" == userinput){
//    Console.WriteLine("testtest");
//    await Script.CreateDatabase();
//}
//Console.WriteLine("do you want to create all the tables?");
//userinput = Console.ReadLine();
//if ("y" == userinput || "" == userinput){
//    Console.WriteLine("testtest");
//    await Script.MakeTables();
//}
//Console.WriteLine("do you want to populate the database");
//userinput = Console.ReadLine();
//if ("y" == userinput || "" == userinput){
//    Console.WriteLine("testtest");
//await Script.MakeTables();
//}
//#endregion

