using Npgsql;
using System.Diagnostics.Metrics;
using System.Net;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Holidaymaker3;

public class DatabaseHelper
{
    string[] customerArray = File.ReadAllLines("C:\\Users\\Lilly\\Source\\Repos\\Holidaymaker3\\DATA\\CUSTOMERS_DATA.csv");
    string[] hotelArray = File.ReadAllLines("C:\\Users\\Lilly\\Source\\Repos\\Holidaymaker3\\DATA\\HOTEL_DATA.csv");
    
    private readonly NpgsqlDataSource _db;
    public DatabaseHelper(NpgsqlDataSource db)
    {
        _db = db;
    }

    public void CustomersConvertFromCSV()
    {

        for (int i = 1; i < 100; i++)
        {
            string[] customerInfo = customerArray[i].Split(",");

            string firstName = customerInfo[1];
            string lastName = customerInfo[2];
            string customerEmail = customerInfo[3];
            string phoneNumberString = customerInfo[4];
            phoneNumberString = phoneNumberString.Replace("-", "");
            string dateOfBirth = customerInfo[5];

            Int64.TryParse(phoneNumberString, out long phoneNumberInt);

            var parsedDate = DateTime.Parse(dateOfBirth);

            /*
            Console.WriteLine(parsedDate);

            Console.WriteLine("first name: " + firstName);
            Console.WriteLine("last name: " + lastName);
            Console.WriteLine("email: " + customerEmail);
            Console.WriteLine("phone number: " + phoneNumberString + " " + phoneNumberInt);
            Console.WriteLine("date of birth: " +  dateOfBirth);
            */
        }
    }

    public void HotelsConvertFromCSV()
    {
        for (int i = 1; i < 10; i++)
        {
            string[] hotelinfo = hotelArray[i].Split(",");

            string hotelName = hotelinfo[1];
            string hotelAddress = hotelinfo[2];
            string hotelCity = hotelinfo[3];
            string hotelCountry = hotelinfo[4];
            string hotelBeach = hotelinfo[5];
            string hotelCenter = hotelinfo[6];
            string hotelRating = hotelinfo[7];

            int.TryParse(hotelBeach, out int beachDistance);
            int.TryParse(hotelCenter, out int centerDistance);
            decimal.TryParse(hotelRating, out decimal hotelRatingDecimal);
        }
    }

    public async Task PopulateCustomersTable()
    {
        var sqlcommand = @"INSERT INTO customers(
        first_name, 
        last_name, 
        email, 
        phone_number, 
        date_of_birth
        ) 
        VALUES ($1, $2, $3, $4, $5)";

        await using (var cmd = _db.CreateCommand(sqlcommand))
        {
            cmd.Parameters.AddWithValue(1);
            cmd.Parameters.AddWithValue(2);
            cmd.Parameters.AddWithValue(3);
            cmd.Parameters.AddWithValue(4);
            cmd.Parameters.AddWithValue(5);
            await cmd.ExecuteNonQueryAsync();
        }

    }

    public async Task PopulateHotelsTable()
    {
        var sqlcommand2 = @"INSERT INTO hotels(
        name,
        address, 
        city, 
        country, 
        beach_distance, 
        center_distance, 
        rating
        ) 
        VALUES ($1, $2, $3, $4, $5, $6, $7)"
        ;

        await using (var cmd = _db.CreateCommand(sqlcommand2))
        {
        cmd.Parameters.AddWithValue(1);
        cmd.Parameters.AddWithValue(2);
        cmd.Parameters.AddWithValue(3);
        cmd.Parameters.AddWithValue(4);
        cmd.Parameters.AddWithValue(5);
        cmd.Parameters.AddWithValue(6);
        cmd.Parameters.AddWithValue(7);
        await cmd.ExecuteNonQueryAsync();
        }
    }
}