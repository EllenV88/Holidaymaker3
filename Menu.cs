using Npgsql;
using System;

namespace Holidaymaker3;
public class Menu
{
    private readonly NpgsqlDataSource _db;
    public Menu(NpgsqlDataSource db)
    {
        _db = db;
    }
    public async Task MainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            Console.WriteLine("|    Welcome to Holidaymaker3!    |\n");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            Console.WriteLine("\n");
            Console.WriteLine("1 - Customers\n");
            Console.WriteLine("2 - Bookings\n");
            Console.WriteLine("0 - Exit\n");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CustomerMenu();
                    break;
                case "2":
                    await BookingsMenu();
                    break;
                case "0":
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("Pick something valid");
                    Console.ReadKey();
                    break;
            }
        }
    }

    public async Task CustomerMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            Console.WriteLine("|     Customer Registration!      |\n");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            Console.WriteLine("1 - Register new customer\n");
            Console.WriteLine("2 - View customer bookings\n");
            Console.WriteLine("0 - Go back\n");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await RegisterNewCustomer();
                    break;
                case "2":

                    break;
                case "0":
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("Pick something valid");
                    Console.ReadKey();
                    break;
            }
        }
    }

    public async Task RegisterNewCustomer()
    {
        Console.Clear();
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
        Console.WriteLine("|      Enter a new customer!      |\n");
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");

        Console.Write("Enter first name: ");
        string firstName = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter last name: ");
        string lastName = Console.ReadLine() ?? string.Empty;

        // Saves to SQL database

        const string query = @"INSERT INTO customers (first_name, last_name) VALUES (@firstName, @lastName)";
        await using (var cmd = _db.CreateCommand(query))
        {
            cmd.Parameters.AddWithValue("firstName", firstName);
            cmd.Parameters.AddWithValue("lastName", lastName);

            await cmd.ExecuteNonQueryAsync();
        }


        Console.Clear();
        Console.WriteLine($"{firstName} {lastName} has been registered as a new customer.\n");
        Console.ReadKey();
    }

    public async Task BookingsMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            Console.WriteLine("|      Bookings management!       |\n");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            Console.WriteLine("1 - Create new booking\n");
            Console.WriteLine("0 - Go back\n");

            string choice = Console.ReadLine();
            SearchPage searchPage = new(_db);

            switch (choice)
            {
                case "1":
                    Console.WriteLine("\nPlease enter a city: "); //casesensitive
                    string city = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine(await searchPage.HotelsByCity(city));
                    Thread.Sleep(1000);
                    var bookingfunction = new BookingFunction(_db);
                    await bookingfunction.NewBooking();
                    break;
                case "0":
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("Pick something valid");
                    Console.ReadKey();
                    break;
            }
        }
    }

    public async Task HotelsAndRoomsMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            Console.WriteLine("|       Our Hotels & Rooms!       |\n");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
            Console.WriteLine("1 - Search Engine\n");
            Console.WriteLine("0 - Go back\n");

            string choice = Console.ReadLine();
            SearchPage searchPage = new(_db);

            switch (choice)
            {

                case "1":
                    Console.WriteLine("\nPlease enter a city: "); //casesensitive
                    string city = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine(await searchPage.HotelsByCity(city));
                    Console.ReadLine();
                    break;
                case "0":
                    return;
                default:
                    Console.Clear();
                    Console.WriteLine("Pick something valid");
                    Console.ReadKey();
                    break;
            }
        }
    }
}