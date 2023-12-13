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
            phone_number BIGINT, 
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
            rating NUMERIC
        )")) 
        {
            await cmd.ExecuteNonQueryAsync();
        }

        await using (var cmd = db.CreateCommand(@"CREATE TABLE IF NOT EXISTS anemities(
            id SERIAL PRIMARY KEY, 
            label TEXT
            )"))
        {
            await cmd.ExecuteNonQueryAsync();
        }

        await using (var cmd = db.CreateCommand(@"CREATE TABLE IF NOT EXISTS extras(
            id SERIAL PRIMARY KEY, 
            label TEXT
            )"))
        {
            await cmd.ExecuteNonQueryAsync();
        }

        await using (var cmd = db.CreateCommand(@"CREATE TABLE IF NOT EXISTS rooms(
            id SERIAL PRIMARY KEY, 
            type TEXT
            )"))
        {
            await cmd.ExecuteNonQueryAsync();
        }

        await using (var cmd = db.CreateCommand(@"CREATE TABLE IF NOT EXISTS bookings(
            id SERIAL PRIMARY KEY, 
            hotels_id INTEGER REFERENCES hotels(id),
            rooms_id INTEGER REFERENCES rooms(id),
            customers_id INTEGER REFERENCES customers(id),
            children INTEGER,
            adults INTEGER,
            check_in_date DATE,
            check_out_date DATE
            )"))
        {
            await cmd.ExecuteNonQueryAsync();
        }

        await using (var cmd = db.CreateCommand(@"CREATE TABLE IF NOT EXISTS hotels_x_rooms(
            hotels_id INTEGER REFERENCES hotels(id),
            rooms_id INTEGER REFERENCES rooms(id),
            price MONEY
            )"))
        {
            await cmd.ExecuteNonQueryAsync();
        }

        await using (var cmd = db.CreateCommand(@"CREATE TABLE IF NOT EXISTS hotels_x_anemities(
            hotels_id INTEGER REFERENCES hotels(id),
            anemities_id INTEGER REFERENCES anemities(id)
            )"))
        {
            await cmd.ExecuteNonQueryAsync();
        }

        await using (var cmd = db.CreateCommand(@"CREATE TABLE IF NOT EXISTS hotels_x_extras(
            hotels_id INTEGER REFERENCES hotels(id),
            extras_id INTEGER REFERENCES extras(id),
            price MONEY
            )"))
        {
            await cmd.ExecuteNonQueryAsync();
        }

        await using (var cmd = db.CreateCommand(@"CREATE TABLE IF NOT EXISTS bookings_x_extras(
            bookings_id INTEGER REFERENCES bookings(id),
            extras_id INTEGER REFERENCES extras(id)
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
            (first_name, last_name, email, phone_number, date_of_birth) 
            VALUES ($1, $2, $3, $4, $5)";

            await using (var cmd = db.CreateCommand(sqlcommand)) {
                cmd.Parameters.AddWithValue(firstName);
                cmd.Parameters.AddWithValue(lastName);
                cmd.Parameters.AddWithValue(customerEmail);
                cmd.Parameters.AddWithValue(phoneNumberInt);
                cmd.Parameters.AddWithValue(parsedDate);
                await cmd.ExecuteNonQueryAsync();
                
            }

            customerArray = File.ReadAllLines("HOTEL_DATA.csv");
            
            
            
            
            

        }
        

    }
}
