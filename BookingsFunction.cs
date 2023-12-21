using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Npgsql;

namespace Holidaymaker3;

public class BookingFunction
{

    private readonly NpgsqlDataSource _db;
    public BookingFunction(NpgsqlDataSource db)
    {
        _db = db;
    }

    public async Task AllBookings()
    {
        Console.Clear();
        const string query = @"select * from bookings";
        var cmd = _db.CreateCommand(query);
        var reader = await cmd.ExecuteReaderAsync();

        Console.WriteLine("\n| bookingID | hotelID | roomID | customerID | children | adults |  CHECK-IN  |  CHECK-OUT  |\n");
        while (await reader.ReadAsync())
        {
            string convertCheckInDate = reader.GetDateTime(7).ToShortDateString();
            string convertCheckOutDate = reader.GetDateTime(8).ToShortDateString();

            Console.WriteLine($"| {reader.GetInt32(0),-9} | {reader.GetInt32(1),-7} | {reader.GetInt32(2),-6} | {reader.GetInt32(4),-10} | {reader.GetInt32(5),-8} | {reader.GetInt32(6),-6} | {convertCheckInDate} | {convertCheckOutDate}  |");
        }
        Console.ReadLine();
    }

    public async Task ConfirmationMessage()
    {
        Console.Clear();
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
        Console.WriteLine($"| Your booking has successfully been created and added to the database! |\n");
        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
        Thread.Sleep(2500);
    }
  
    public async Task NewBooking()
    {

        var insertBooking = @"INSERT INTO bookings(
            hotel_id, 
            room_id, 
            customer_id, 
            children, 
            adults, 
            check_in_date, 
            check_out_date) 
            VALUES ($1, $2, $3, $4, $5, $6, $7)";

        int hotelID = 0, roomID = 0, customerID = 0, numberOfKids = 0, numberOfAdults = 0;
        DateTime checkInDate = new DateTime(1999, 1, 1);
        DateTime checkOutDate = new DateTime(1999, 1, 1);

        bool insertLoop = true;

        while(true == insertLoop){
            Console.Write("\nPlease enter the hotel id: ");
            bool success = int.TryParse(Console.ReadLine(), out hotelID);
            if (true == success)
            {
                insertLoop = false;
            }
        }

        insertLoop = true;
        while(true == insertLoop){
            Console.WriteLine("\nRoom Standards: \n");
            Console.WriteLine("1. Single");
            Console.WriteLine("2. Double");
            Console.WriteLine("3. Triple");
            Console.WriteLine("4. Suite");
            Console.Write("\nPlease enter the desired room type: ");
            bool success = int.TryParse(Console.ReadLine(), out roomID);

            if (true == success){
                insertLoop = false;
            }
        }

        insertLoop = true;
        while(true == insertLoop)
        {
            Console.Write("\nPlease enter customer id: ");
            bool success = int.TryParse(Console.ReadLine(), out customerID);

            if (true == success)
            {
                insertLoop = false;
            }
        }

        insertLoop = true;
        while(true == insertLoop)
        {
            Console.Write("\nPlease enter the number of children: ");
            bool success = int.TryParse(Console.ReadLine(), out numberOfKids);

            if (true == success)
            {
                insertLoop = false;
            }
        }
      
        insertLoop = true;
        while(true == insertLoop)
        {
            Console.Write("\nPlease enter the number of adults: ");
            bool success = int.TryParse(Console.ReadLine(), out numberOfAdults);

            if (true == success)
            {
                insertLoop = false;
            }
        }

        insertLoop = true;
        while(true == insertLoop)
        {
            Console.WriteLine("\nPlease enter the check in date (yyyy-mm-dd): ");
            bool success = DateTime.TryParse(Console.ReadLine(), out checkInDate);

            Console.WriteLine("\nPlease enter the check out date (yyyy-mm-dd): ");
            bool success2 = DateTime.TryParse(Console.ReadLine(), out checkOutDate);

            if (true == success || true == success2)
            {
                insertLoop = false;
            }
        }
        
        await using (var cmd = _db.CreateCommand(insertBooking))
        {
            cmd.Parameters.AddWithValue(hotelID);
            cmd.Parameters.AddWithValue(roomID);
            cmd.Parameters.AddWithValue(customerID);
            cmd.Parameters.AddWithValue(numberOfKids);
            cmd.Parameters.AddWithValue(numberOfAdults);
            cmd.Parameters.AddWithValue(checkInDate);
            cmd.Parameters.AddWithValue(checkOutDate);
            await cmd.ExecuteNonQueryAsync();
        }

        Console.WriteLine();
        Console.WriteLine("Booking Additions: \n");
        Console.WriteLine("1. Extra bed");
        Console.WriteLine("2. Half board child");
        Console.WriteLine("3. Half board adult");
        Console.WriteLine("4. All-inclusive child");
        Console.WriteLine("5. All-inclusive adult\n");

        string chosenExtrasString = Console.ReadLine();
        int booking = 0;
        string[] chosenExtras = chosenExtrasString.Split(", ");
        
        await using var command = _db.CreateCommand("SELECT booking_id  FROM bookings ORDER BY booking_id DESC LIMIT 1");
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        { 
        booking = reader.GetInt32(0);
        }
        
        var insertBookingsXExtras = @"INSERT INTO bookings_x_extras(booking_id, extra_id) VALUES ($1, $2)";
        foreach (string c in chosenExtras)
        {
            int chosenExtrasInt = Int32.Parse(c);
            switch (c)
            {
                case "1":
                    await using (var cmd = _db.CreateCommand(insertBookingsXExtras)){ 
                        cmd.Parameters.AddWithValue(booking); 
                        cmd.Parameters.AddWithValue(chosenExtrasInt); 
                        await cmd.ExecuteNonQueryAsync(); }
                    await ConfirmationMessage();
                    break;

                case "2":
                    await using (var cmd = _db.CreateCommand(insertBookingsXExtras)){ 
                        cmd.Parameters.AddWithValue(booking); 
                        cmd.Parameters.AddWithValue(chosenExtrasInt); 
                        await cmd.ExecuteNonQueryAsync(); }
                    await ConfirmationMessage();

                    break;

                case "3":
                    await using (var cmd = _db.CreateCommand(insertBookingsXExtras)){ 
                        cmd.Parameters.AddWithValue(booking); 
                        cmd.Parameters.AddWithValue(chosenExtrasInt); 
                        await cmd.ExecuteNonQueryAsync();}
                    await ConfirmationMessage();
                    break;

                case "4":
                    await using (var cmd = _db.CreateCommand(insertBookingsXExtras)){ 
                        cmd.Parameters.AddWithValue(booking); 
                        cmd.Parameters.AddWithValue(chosenExtrasInt); 
                        await cmd.ExecuteNonQueryAsync();}
                    await ConfirmationMessage();
                    break;

                case "5":
                    await using (var cmd = _db.CreateCommand(insertBookingsXExtras)){ 
                       cmd.Parameters.AddWithValue(booking); 
                       cmd.Parameters.AddWithValue(chosenExtrasInt); 
                    await cmd.ExecuteNonQueryAsync();}
                    await ConfirmationMessage();
                    break;

                default:
                   Console.Clear();
                   Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                   Console.WriteLine($" Invalid choice, try again!\n");
                   Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");
                   Thread.Sleep(3000);
                break;
            }
        }
    }
}
