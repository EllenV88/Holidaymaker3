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

    public async Task<string> HotelsByCity(string choiceOfCity)
    {
        string result = string.Empty;

        const string query = "SELECT hotel_id, name, address FROM hotels WHERE city = $1";
        var cmd = _db.CreateCommand(query);
        cmd.Parameters.AddWithValue(choiceOfCity);

        var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            result += "\n";
            result += "ID: " + reader.GetInt32(0);
            result += "\n";
            result += "HOTEL: " + reader.GetString(1);
            result += "\n";
            result += "ADDRESS: " + reader.GetString(2);
            result += "\n";
        }
        result += "----------------------------------------------";
        result += "\n";

        return result;
    }
}

