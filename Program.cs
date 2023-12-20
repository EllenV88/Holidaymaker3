using Npgsql;
using Holidaymaker3;
using System.Runtime.CompilerServices;

const string dbUri = "Host=localhost;Port=5455;Username=postgres;Password=postgres;Database=holidaymaker;";
await using var db = NpgsqlDataSource.Create(dbUri);

var databaseCreator = new DatabaseCreator(db);
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

var Menu = new Menu(db);
await Menu.MainMenu();

SearchPage searchPage = new(db);

var bookingfunction = new BookingFunction(db);
await bookingfunction.NewBooking(); 