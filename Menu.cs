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
            Console.WriteLine("1 - Customers\n");
            Console.WriteLine("2 - Bookings\n");
            Console.WriteLine("0 - Exit\n");

            switch (Console.ReadLine())
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
                    Thread.Sleep(1000);
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

            switch (Console.ReadLine())
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
                    Thread.Sleep(1000);
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

        Console.Write("Enter email: ");
        string email = Console.ReadLine();

        Console.Write("Enter phone number: ");
        string phoneNumber = Console.ReadLine();

        Console.Write("Enter date of birth (yyyy-mm-dd): ");
        string dateOfBirth = Console.ReadLine();

        // Saves to SQL database

        const string query1 =
           @" SELECT setval('customers_customer_id_seq', (SELECT MAX(customer_id) FROM customers));";

        await using (var cmd = _db.CreateCommand(query1))
        {
            await cmd.ExecuteNonQueryAsync();
        }

        const string query2 =
       @" INSERT INTO customers (first_name, last_name, email, phone_number, date_of_birth)
           VALUES ($1, $2, $3, $4, $5)";

        await using (var cmd = _db.CreateCommand(query2))
        {
            Console.WriteLine(firstName);
            cmd.Parameters.AddWithValue(firstName);
            cmd.Parameters.AddWithValue(lastName);
            cmd.Parameters.AddWithValue(email);
            cmd.Parameters.AddWithValue(phoneNumber);
            cmd.Parameters.AddWithValue(DateTime.Parse(dateOfBirth));

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
            Console.WriteLine("2 - List of hotels\n");
            Console.WriteLine("0 - Go back\n");

            switch (Console.ReadLine())
            {
                case "1":
                    SearchPage searchPage = new(_db);
                    Console.WriteLine("\nPlease enter a city: "); //casesensitive
                    string city = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine(await searchPage.HotelsByCity(city));

                    var bookingfunction = new BookingFunction(_db);
                    await bookingfunction.NewBooking();
                    break;

                case "2":
                    Console.Clear();
                    await DisplayHotels();
                    Console.ReadKey();
                    break;

                case "0":
                    return;

                default:
                    Console.Clear();
                    Console.WriteLine("Pick something valid");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

    public async Task DisplayHotels()
      
    {
        Console.WriteLine("\n| ID | Hotel | Address | City | Country | Distance to Beach | Distance to Center | Rating |");
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");

        string[] hotelArray = await File.ReadAllLinesAsync("../../../DATA/HOTEL_DATA.csv");
        
        for (int i = 1; i < hotelArray.Length; i++)
        {

            string[] hotelInfo = hotelArray[i].Split(",");

            int hotelId = int.Parse(hotelInfo[0]);
            string name = hotelInfo[1];
            string address = hotelInfo[2];
            string city = hotelInfo[3];
            string country = hotelInfo[4];
            double beachDistance = double.Parse(hotelInfo[5]);
            double centerDistance = double.Parse(hotelInfo[6]);
            double rating = double.Parse(hotelInfo[7].Replace('.', ','));
           
            Console.WriteLine($"{hotelId} - {name}, {address}, {city}, {country},  {beachDistance}m,  {centerDistance}m, *{rating}\n");
        }
    }
}

