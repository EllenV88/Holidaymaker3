using System.Data.Common;
using System.Runtime.CompilerServices;
using Npgsql;

namespace Holidaymaker3;

public class DatabaseCreator
{

    private readonly NpgsqlDataSource _db;

    public DatabaseCreator(NpgsqlDataSource db)
    {
        _db = db;
    }

    public async Task CreateDatabase()
    {
        const string newDatabase = "CREATE DATABASE holidaymaker";
        await _db.CreateCommand(newDatabase).ExecuteNonQueryAsync();
    }

    public async Task CreateTables()
    {
        const string customersTable = @"CREATE TABLE IF NOT EXISTS customers(
        id SERIAL PRIMARY KEY, 
        first_name TEXT, 
        last_name TEXT, 
        email TEXT, 
        phone_number TEXT, 
        date_of_birth DATE
        )";
        await _db.CreateCommand(customersTable).ExecuteNonQueryAsync();

        const string hotelsTable = @"CREATE TABLE IF NOT EXISTS hotels (
        id SERIAL PRIMARY KEY,
        name TEXT, 
        address TEXT, 
        city TEXT, 
        country TEXT, 
        beach_distance NUMERIC, 
        center_distance NUMERIC, 
        rating NUMERIC
        )";
        await _db.CreateCommand(hotelsTable).ExecuteNonQueryAsync();

        const string anemities = @"CREATE TABLE IF NOT EXISTS anemities(
        id SERIAL PRIMARY KEY, 
        label TEXT
        )";
        await _db.CreateCommand(anemities).ExecuteNonQueryAsync();

        const string extras = @"CREATE TABLE IF NOT EXISTS extras(
        id SERIAL PRIMARY KEY, 
        label TEXT
        )";
        await _db.CreateCommand(extras).ExecuteNonQueryAsync();

        const string rooms = @"CREATE TABLE IF NOT EXISTS rooms(
        id SERIAL PRIMARY KEY, 
        type TEXT
        )";
        await _db.CreateCommand(rooms).ExecuteNonQueryAsync();

        const string bookings = @"CREATE TABLE IF NOT EXISTS bookings(
        id SERIAL PRIMARY KEY, 
        hotels_id INTEGER REFERENCES hotels(id),
        rooms_id INTEGER REFERENCES rooms(id),
        customers_id INTEGER REFERENCES customers(id),
        children INTEGER,
        adults INTEGER,
        check_in_date DATE,
        check_out_date DATE
        )";
        await _db.CreateCommand(bookings).ExecuteNonQueryAsync();

        const string bookingsExtras = @"CREATE TABLE IF NOT EXISTS bookings_x_extras(
        bookings_id INTEGER REFERENCES bookings(id),
        extras_id INTEGER REFERENCES extras(id)
        )";
        await _db.CreateCommand(bookingsExtras).ExecuteNonQueryAsync();

        const string hotelsRooms = @"CREATE TABLE IF NOT EXISTS hotels_x_rooms(
        hotels_id INTEGER REFERENCES hotels(id),
        rooms_id INTEGER REFERENCES rooms(id),
        price MONEY
        )";
        await _db.CreateCommand(hotelsRooms).ExecuteNonQueryAsync();

        const string hotelAnemitites = @"CREATE TABLE IF NOT EXISTS hotels_x_anemities(
        hotels_id INTEGER REFERENCES hotels(id),
        anemities_id INTEGER REFERENCES anemities(id)
        )";
        await _db.CreateCommand(hotelAnemitites).ExecuteNonQueryAsync();

        const string hotelsExtras = @"CREATE TABLE IF NOT EXISTS hotels_x_extras(
        hotels_id INTEGER REFERENCES hotels(id),
        extras_id INTEGER REFERENCES extras(id),
        price MONEY
        )";
        await _db.CreateCommand(hotelsExtras).ExecuteNonQueryAsync();
    }

}