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

    public async Task<string> HotelsByCity(string city)
    {
        string result = string.Empty;

        const string query = "select hotel_id, name, city from hotels where city = $1";
        var cmd = _db.CreateCommand(query);
        cmd.Parameters.AddWithValue(city);

        var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            result += "\n";
            result += "ID: " + reader.GetInt32(0);//hämta>id
            result += "\n";
            result += "HOTEL: " + reader.GetString(1);//hämta>name
            result += "\n";
            result += "CITY: " + reader.GetString(2);//hämta>city
            result += "\n";
        }
        result += "----------------------------------------------";
        result += "\n";

        return result;
    }
}

