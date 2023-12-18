using System.Data.Common;
using System.Runtime.CompilerServices;
using Npgsql;

namespace Holidaymaker3;

public class BookingFunction{

    private readonly NpgsqlDataSource _db;

    public BookingFunction(NpgsqlDataSource db)
    {
        _db = db;
    }

    public async Task NewBooking(){
        var insertBooking = "INSERT INTO bookings(hotels_id, rooms_id, customer_id, children, adults, check_in_date, check_out_date) VALUES ($1, $2, $3, $4, $5, $6, $7)";

        
        
    }

}