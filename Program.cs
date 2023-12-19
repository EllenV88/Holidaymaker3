using Npgsql;
using Holidaymaker3;
using System.Runtime.CompilerServices;

const string dbUri = "Host=localhost;Port=5455;Username=postgres;Password=postgres;Database=holidaymaker;";
await using var db = NpgsqlDataSource.Create(dbUri);

var databaseCreator = new DatabaseCreator(db);
//await databaseCreator.CreateDatabase();

var databasehelper = new DatabaseHelper(db);
Console.WriteLine("Would you like to reset the database? y/N");
if(Console.ReadLine()?.ToLower() == "y")
{
    await databasehelper.ResetTables();
    await databaseCreator.CreateTables();
    await databasehelper.PopulateCustomersTable();
    await databasehelper.PopulateHotelsTable();
    await databasehelper.PopulateRoomsTable();
    await databasehelper.PopulateHotelxRooms();
    await databasehelper.PopulateAmenityTable();
    await databasehelper.PopulateExtraTable();
    await databasehelper.PopulateHotelsxAmenities();
    await databasehelper.PopulateHotelsxExtras();
    Console.WriteLine("Done populating tables.\n");
}


SearchPage searchPage = new(db);
Console.WriteLine("Please enter a city: "); //casesensitive
string city = Console.ReadLine() ?? string.Empty;
Console.WriteLine(await searchPage.HotelsByCity(city));
Console.ReadLine();

var Menu = new Menu(db);
Menu.MainMenu();

//var bookingfunction = new BookingFunction(db);
//await bookingfunction.NewBooking();



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

