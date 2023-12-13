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
            phone_number INTEGER, 
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
        
    }
}
