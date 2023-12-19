using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Holidaymaker3;

public class SearchPage
{
    private readonly NpgsqlDataSource _db;

    public SearchPage(NpgsqlDataSource db)
    {
        _db = db;
    }

    public async Task<string> AllInfoByHotels()
    {
        string result = string.Empty;

        const string query = @"select * from hotels";
        var reader = await _db.CreateCommand(query).ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            //result += reader.GetInt32(0);//id
            //result += ", ";
            result += "Hotelname: " + reader.GetString(1);//name
            result += ", ";
            result += reader.GetString(2);//address
            result += ", ";
            result += reader.GetString(3);//city
            result += ", ";
            result += reader.GetString(4);//country
            result += ", ";
            result += "To beach: " + reader.GetInt32(5) + "km";//beach distance
            result += ", ";
            result += "To centre: " + reader.GetInt32(6) + "km";//centre distance
            result += ", ";
            result += "Rating: " + reader.GetInt32(7);//rating
        }
        return result;
    }

    public async Task<string> HotelsByCity(string city)
    {
        string result = string.Empty;

        const string query = "select hotel_id, name, city from hotels where city = $1";
        var cmd = _db.CreateCommand(query);
        cmd.Parameters.AddWithValue(city);
        var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            result += reader.GetInt32(0);//hämta>id
            result += ",";
            result += reader.GetString(1);//hämta>name
            result += ",";
            result += reader.GetString(2);//hämta>city
            result += "\n";
        }
        result += "----------------------------------------------";
        result += "\n";

        return result;
    }
}

