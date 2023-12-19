using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holidaymaker3;

    public class Customers
    {
    private readonly NpgsqlDataSource _db;
    public Customers(NpgsqlDataSource db)
    {
        _db = db;
    }
}

