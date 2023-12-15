using Npgsql;
using System.Diagnostics.Metrics;
using System.Net;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Holidaymaker3;

public class DatabaseHelper
{

    string[] hotelArray = File.ReadAllLines("../../../DATA/CUSTOMERS_DATA.csv");

    private readonly NpgsqlDataSource _db;
    public DatabaseHelper(NpgsqlDataSource db)
    {
        _db = db;
    }


    public async Task PopulateCustomersTable()
    {
        const string query = @"INSERT INTO customers(
        first_name, 
        last_name, 
        email, 
        phone_number, 
        date_of_birth
        ) 
        VALUES ($1, $2, $3, $4, $5)";

        string[] customerArray = File.ReadAllLines("../../../DATA/CUSTOMERS_DATA.csv");

        for (int i = 1; i < 100; i++)
        {
            string[] customerInfo = customerArray[i].Split(",");
            //TODO change phone_number type from long to string because thats what they are

            await using (var cmd = _db.CreateCommand(query))
            {
                cmd.Parameters.AddWithValue(customerInfo[1]);
                cmd.Parameters.AddWithValue(customerInfo[2]);
                cmd.Parameters.AddWithValue(customerInfo[3]);
                cmd.Parameters.AddWithValue(customerInfo[4].Replace("-", ""));
                cmd.Parameters.AddWithValue(DateTime.Parse(customerInfo[5]));
                await cmd.ExecuteNonQueryAsync();
            }
        }

    }

    public async Task PopulateHotelsTable()
    {
        const string query = @"INSERT INTO hotels(
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

        string[] hotelArray = File.ReadAllLines("../../../DATA/HOTEL_DATA.csv");

        for (int i = 1; i < 10; i++)
        {
            string[] hotelInfo = hotelArray[i].Split(",");

            await using (var cmd = _db.CreateCommand(query))
            {
                cmd.Parameters.AddWithValue(hotelInfo[1]);
                cmd.Parameters.AddWithValue(hotelInfo[2]);
                cmd.Parameters.AddWithValue(hotelInfo[3]);
                cmd.Parameters.AddWithValue(hotelInfo[4]);
                cmd.Parameters.AddWithValue(int.Parse(hotelInfo[5]));
                cmd.Parameters.AddWithValue(int.Parse(hotelInfo[6]));
                cmd.Parameters.AddWithValue(decimal.Parse(hotelInfo[7].Replace('.',',')));

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task PopulateAnemityTable()
    {
        const string query = @"INSERT INTO anemities(
        label
        ) 
        VALUES ($1)"
        ;

        string[] anemityArray = File.ReadAllLines("../../../DATA/ANEMITY_DATA.csv");

        for (int i = 1; i < 10; i++)
        {
            string[] anemityInfo = anemityArray[i].Split(",");

            await using (var cmd = _db.CreateCommand(query))
            {
                cmd.Parameters.AddWithValue(anemityInfo[1]);

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task PopulateExtraTable()
    {
        const string query = @"INSERT INTO extras(
        label
        ) 
        VALUES ($1)"
        ;

        string[] extraArray = File.ReadAllLines("../../../DATA/EXTRA_DATA.csv");

        for (int i = 1; i < 10; i++)
        {
            string[] extraInfo = extraArray[i].Split(",");

            await using (var cmd = _db.CreateCommand(query))
            {
                cmd.Parameters.AddWithValue(extraInfo[1]);

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task PopulateRoomTable()
    {
        const string query = @"INSERT INTO rooms(
        type
        ) 
        VALUES ($1)"
        ;

        string[] roomArray = File.ReadAllLines("../../../DATA/ROOM_DATA.csv");

        for (int i = 1; i < 10; i++)
        {
            string[] roomInfo = roomArray[i].Split(",");

            await using (var cmd = _db.CreateCommand(query))
            {
                cmd.Parameters.AddWithValue(roomInfo[1]);

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task PopulateHotelxRooms()
    {
        const string query = @"INSERT INTO hotels_x_rooms(
        hotel_id, 
        room_id, 
        price
        ) 
        VALUES ($1, $2, $3)"
        ;

        string[] hotelxRoomArray = File.ReadAllLines("../../../DATA/HOTELxROOMS_DATA.csv");

        for (int i = 1; i < 10; i++)
        {
            string[] hotelxRoomInfo = hotelxRoomArray[i].Split(",");

            await using (var cmd = _db.CreateCommand(query))
            {
                cmd.Parameters.AddWithValue(int.Parse(hotelxRoomInfo[1]));
                cmd.Parameters.AddWithValue(int.Parse(hotelxRoomInfo[2]));
                cmd.Parameters.AddWithValue(int.Parse(hotelxRoomInfo[3]));

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}