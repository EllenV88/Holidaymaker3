using Npgsql;
namespace makedatabase;

public class PopulateTables
{
    public static async Task PopulateDatabase()
    {

        string dbUri = "Host=localhost;Port=5455;Username=postgres;Password=postgres;Database=holidaymaker;";

        await using var db = NpgsqlDataSource.Create(dbUri);

        string[] customerArray = File.ReadAllLines("CUSTOMERS_DATA.csv");

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

            var sqlcommand = @"INSERT INTO customers 
            (first_name, last_name, email, phone_number, date_of_birth) 
            VALUES ($1, $2, $3, $4, $5)";

            await using (var cmd = db.CreateCommand(sqlcommand))
            {
                cmd.Parameters.AddWithValue(firstName);
                cmd.Parameters.AddWithValue(lastName);
                cmd.Parameters.AddWithValue(customerEmail);
                cmd.Parameters.AddWithValue(phoneNumberInt);
                cmd.Parameters.AddWithValue(parsedDate);
                await cmd.ExecuteNonQueryAsync();

            }
        }

        string[] hotelArray = File.ReadAllLines("HOTEL_DATA.csv");

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

            var sqlcommand2 = @"INSERT INTO hotels (
        name,
        address, 
        city, 
        country, 
        beach_distance, 
        center_distance, 
        rating) VALUES (
$1, $2, $3, $4, $5, $6, $7)";

            await using (var cmd = db.CreateCommand(sqlcommand2))
            {
                cmd.Parameters.AddWithValue(hotelName);
                cmd.Parameters.AddWithValue(hotelAddress);
                cmd.Parameters.AddWithValue(hotelCity);
                cmd.Parameters.AddWithValue(hotelCountry);
                cmd.Parameters.AddWithValue(beachDistance);
                cmd.Parameters.AddWithValue(centerDistance);
                cmd.Parameters.AddWithValue(hotelRatingDecimal);
                await cmd.ExecuteNonQueryAsync();

            }

        }
    }
}