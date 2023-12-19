using Npgsql;
using System.Collections.Generic;
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
        VALUES ($1, $2, $3, $4, $5, $6)
        ON CONFLICT (customer_id) DO UPDATE SET
        first_name = EXCLUDED.first_name,
        last_name = EXCLUDED.last_name,
        email = EXCLUDED.email,
        phone_number = EXCLUDED.phone_number,
        date_of_birth = EXCLUDED.date_of_birth;";

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
        VALUES ($1, $2, $3, $4, $5, $6, $7, $8)
        ON CONFLICT(hotel_id) DO UPDATE SET
        name = EXCLUDED.name,
        address = EXCLUDED.address,
        city = EXCLUDED.city,
        country = EXCLUDED.country,
        beach_distance = EXCLUDED.beach_distance,
        center_distance = EXCLUDED.center_distance,
        rating = EXCLUDED.rating;" 
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
        VALUES ($1, $2)
        ON CONFLICT(amenity_id) DO UPDATE SET
        label = EXCLUDED.label"
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
        VALUES ($1, $2)
        ON CONFLICT(extra_id) DO UPDATE SET
        label = EXCLUDED.label"
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
        VALUES ($1, $2)
        ON CONFLICT(room_id) DO UPDATE SET
        type = EXCLUDED.type"
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

    public async Task PopulateBookingsTable()
    {
        const string query = @"INSERT INTO bookings(
        booking_id, 
        children,
        adults,
        check_in_date,
        check_out_date
        ) 
        VALUES ($1, $2, $3, $4, $5)
        ON CONFLICT(booking_id) DO UPDATE SET
        children = EXCLUDED.children,
        adults = EXCLUDED.adults,
        check_in_date = EXCLUDED.check_in_date,
        check_out_date = EXCLUDED.check_out_date"
        ;

        string[] bookingArray = File.ReadAllLines("../../../DATA/BOOKINGS_DATA.csv");

        for (int i = 1; i < bookingArray.Length; i++)
        {
            string[] bookingInfo = bookingArray[i].Split(",");
            //TODO change phone_number type from long to string because thats what they are

            await using (var cmd = _db.CreateCommand(query))
            {
                cmd.Parameters.AddWithValue(int.Parse(bookingInfo[0]));
                cmd.Parameters.AddWithValue(int.Parse(bookingInfo[1]));
                cmd.Parameters.AddWithValue(int.Parse(bookingInfo[2]));
                cmd.Parameters.AddWithValue(DateTime.Parse(bookingInfo[3]));
                cmd.Parameters.AddWithValue(DateTime.Parse(bookingInfo[4]));

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

    public async Task PopulateHotelsxAmenities()
    {
        const string query = @"INSERT INTO hotels_x_amenities(
        hotel_id, 
        amenity_id
        ) 
        VALUES ($1, $2)"
        ;

        string[] hotelxAmenityArray = File.ReadAllLines("../../../DATA/HOTELxAMENITY_DATA.csv");

        for (int i = 1; i < hotelxAmenityArray.Length; i++)
        {
            string[] hotelxAmenityInfo = hotelxAmenityArray[i].Split(",");
           
            await using (var cmd = _db.CreateCommand(query))
            {
                cmd.Parameters.AddWithValue(int.Parse(hotelxAmenityInfo[0]));
                cmd.Parameters.AddWithValue(int.Parse(hotelxAmenityInfo[1]));

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

}