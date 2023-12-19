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

        public async Task<string> AllPlacesByHotels() //fixa> ta emot ints
        {
            string result = string.Empty;

            const string query = @"select * from hotels";
            var reader = await _db.CreateCommand(query).ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result += reader.GetInt32(0);
                result += ", ";
                result += reader.GetString(1);
                result += ", ";
                result += reader.GetString(2);
                result += ", ";
                result += reader.GetString(3);
                result += ", ";
                result += reader.GetString(4);
                result += ", ";
                result += reader.GetInt32(5);
                result += ", ";
                result += reader.GetInt32(6);
                result += ", ";
                result += reader.GetInt32(7); //numeric issue
            }
            return result;
        }
    }

