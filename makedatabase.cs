using System.Runtime.CompilerServices;
using Npgsql;

namespace makedatabase;
class Script 
{
    
    public static async Task CreateDatabase(){
        string dbUri = "Host=localhost;Port=5455;Username=postgres;Password=postgres;";
        
        await using var db = NpgsqlDataSource.Create(dbUri);

        await using (var cmd = db.CreateCommand("CREATE DATABASE holidaymaker"))
        { 
            await cmd.ExecuteNonQueryAsync();
        }
    }
    public static async Task MakeTables()
    { 
        string dbUri = "Host=localhost;Port=5455;Username=postgres;Password=postgres;Database=holidaymaker;";

        await using var db = NpgsqlDataSource.Create(dbUri);

        await using (var cmd = db.CreateCommand(@"CREATE TABLE IF NOT EXISTS customers (
            id SERIAL PRIMARY KEY, 
            first_name TEXT, 
            last_name TEXT, 
            email TEXT, 
            number BIGINT, 
            date_of_birth DATE)"))
        {
           await cmd.ExecuteNonQueryAsync();
        }

        await using (var cmd = db.CreateCommand(@"CREATE TABLE IF NOT EXISTS hotels (
            id SERIAL PRIMARY KEY,
            name TEXT, 
            address TEXT, 
            city TEXT, 
            country TEXT, 
            beach_distance NUMERIC, 
            center_distance NUMERIC, 
            rating NUMERIC, 
            restaurant BOOLEAN, 
            pool BOOLEAN, 
            evening_entertainment BOOLEAN, 
            kids_club BOOLEAN,
            price_extrabeds MONEY, 
            price_halfboard_child MONEY, 
            price_halfboard_adult MONEY, 
            price_allinclusive_child MONEY, 
            price_allinclusive_adult MONEY
        )")) 
        {
            await cmd.ExecuteNonQueryAsync();
        }

        await using (var cmd = db.CreateCommand(@"CREATE TABLE IF NOT EXISTS additions(
            id SERIAL PRIMARY KEY, 
            number_of_extrabeds INT DEFAULT NULL CHECK (number_of_extrabeds <= 1), 
            number_of_halfboard_child INT DEFAULT NULL, 
            number_of_halfboard_adult INT DEFAULT NULL, 
            number_of_allinclusive_child INT DEFAULT NULL, 
            number_of_allinclusive_adult INT DEFAULT NULL
            )")) 
        {
            await cmd.ExecuteNonQueryAsync();
        }
    }    

    public static async Task PopulateDatabase(){
        
        string dbUri = "Host=localhost;Port=5455;Username=postgres;Password=postgres;Database=holidaymaker;";

        await using var db = NpgsqlDataSource.Create(dbUri);

        string[] customerArray = File.ReadAllLines("CUSTOMERS_DATA.csv");

        for(int i = 1; i < 100; i++){
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
            (first_name, last_name, email, number, date_of_birth) 
            VALUES ($1, $2, $3, $4, $5)";

            await using (var cmd = db.CreateCommand(sqlcommand)) {
                cmd.Parameters.AddWithValue(firstName);
                cmd.Parameters.AddWithValue(lastName);
                cmd.Parameters.AddWithValue(customerEmail);
                cmd.Parameters.AddWithValue(phoneNumberInt);
                cmd.Parameters.AddWithValue(parsedDate);
                await cmd.ExecuteNonQueryAsync();
                
            }

        }
        

    }
}
