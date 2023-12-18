using Npgsql;
using System.Diagnostics.Metrics;
using System.Net;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Holidaymaker3;

public class DatabaseHelper
{
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
        date_of_birth,
        customer_id
        ) 
        VALUES ($1, $2, $3, $4, $5, $6)";

        string[] customerArray = File.ReadAllLines("../../../DATA/CUSTOMERS_DATA.csv");

        for (int i = 1; i < customerArray.Length; i++)
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
                cmd.Parameters.AddWithValue(int.Parse(customerInfo[0]));
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
        rating,
        hotel_id
        ) 
        VALUES ($1, $2, $3, $4, $5, $6, $7, $8)"
        ;

        string[] hotelArray = File.ReadAllLines("../../../DATA/HOTEL_DATA.csv");

        for (int i = 1; i < hotelArray.Length; i++)
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
                cmd.Parameters.AddWithValue(int.Parse(hotelInfo[0]));

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task PopulateAmenityTable()
    {
        const string query = @"INSERT INTO amenities(
        label,
        amenity_id
        ) 
        VALUES ($1, $2)"
        ;

        string[] amenityArray = File.ReadAllLines("../../../DATA/AMENITY_DATA.csv");

        for (int i = 1; i < amenityArray.Length; i++)
        {
            string[] amenityInfo = amenityArray[i].Split(",");

            await using (var cmd = _db.CreateCommand(query))
            {
                cmd.Parameters.AddWithValue(amenityInfo[1]);
                cmd.Parameters.AddWithValue(int.Parse(amenityInfo[0]));

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task PopulateExtraTable()
    {
        const string query = @"INSERT INTO extras(
        label,
        extra_id
        ) 
        VALUES ($1, $2)"
        ;

        string[] extraArray = File.ReadAllLines("../../../DATA/EXTRA_DATA.csv");

        for (int i = 1; i < extraArray.Length; i++)
        {
            string[] extraInfo = extraArray[i].Split(",");

            await using (var cmd = _db.CreateCommand(query))
            {
                cmd.Parameters.AddWithValue(extraInfo[1]);
                cmd.Parameters.AddWithValue(int.Parse(extraInfo[0]));

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task PopulateRoomsTable()
    {
        const string query = @"INSERT INTO rooms(
        type,
        room_id
        ) 
        VALUES ($1, $2)"
        ;

        string[] roomsArray = File.ReadAllLines("../../../DATA/ROOMS_DATA.csv");

        for (int i = 1; i < roomsArray.Length; i++)
        {
            string[] roomsInfo = roomsArray[i].Split(",");

            await using (var cmd = _db.CreateCommand(query))
            {
                cmd.Parameters.AddWithValue(roomsInfo[1]);
                cmd.Parameters.AddWithValue(int.Parse(roomsInfo[0]));

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

        string[] hotelxRoomArray = File.ReadAllLines("../../../DATA/HOTELxROOM_DATA.csv");

        for (int i = 1; i < hotelxRoomArray.Length; i++)
        {
            string[] hotelxRoomInfo = hotelxRoomArray[i].Split(",");
            
            await using (var cmd = _db.CreateCommand(query))
            {
                cmd.Parameters.AddWithValue(int.Parse(hotelxRoomInfo[0]));
                cmd.Parameters.AddWithValue(int.Parse(hotelxRoomInfo[1]));
                cmd.Parameters.AddWithValue(decimal.Parse(hotelxRoomInfo[2].Replace('.', ',')));

                await cmd.ExecuteNonQueryAsync();
            }
        }

    }
     public async Task PopulateHotelsxExtras()
        {
            const string query = @"INSERT INTO hotels_x_extras(
        hotel_id, 
        extra_id, 
        price
        ) 
        VALUES ($1, $2, $3)"
            ;

            string[] hotelxExtraArray = File.ReadAllLines("../../../DATA/HOTELxEXTRA_DATA.csv");

            for (int i = 1; i < hotelxExtraArray.Length; i++)
            {
                string[] hotelxExtraInfo = hotelxExtraArray[i].Split(",");
                
                await using (var cmd = _db.CreateCommand(query))
                {
                    cmd.Parameters.AddWithValue(int.Parse(hotelxExtraInfo[0]));
                    cmd.Parameters.AddWithValue(int.Parse(hotelxExtraInfo[1]));
                    cmd.Parameters.AddWithValue(decimal.Parse(hotelxExtraInfo[2].Replace('.', ',')));

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
}