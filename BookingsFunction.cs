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
        /*
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
        while(true == insertLoop){ //insert hotel id
            //potentiellt byta ut/importera från search page funktion
            Console.WriteLine("please enter a hotel id: ");
            bool success = int.TryParse(Console.ReadLine(), out hotelID);
            if(true == success){
                insertLoop = false;
            }
        }

        insertLoop = true;
        while(true == insertLoop){ //insert room id
            Console.WriteLine("please enter room id: ");
            //ska vi kunna ha flera rum i en bokning eller? 
            //antar inte det för jag tror inte man kan ha flera values i en column (om man inte gör det till en string)
            bool success = int.TryParse(Console.ReadLine(), out roomID);
            if(true == success){
                insertLoop = false;
            }
        }

        insertLoop = true;
        while(true == insertLoop){ //insert customer_id
            Console.WriteLine("please enter customer id: ");
            bool success = int.TryParse(Console.ReadLine(), out customerID);
            if(true == success){
                insertLoop = false;
            }
        }

        insertLoop = true;
        while(true == insertLoop){ //children
            Console.WriteLine("please enter the number of kids: ");
            bool success = int.TryParse(Console.ReadLine(), out numberOfKids);
            if(true == success){
                insertLoop = false;
            }
        }

        insertLoop = true;
        while(true == insertLoop){ //adults
            Console.WriteLine("please enter the number of adults: ");
            bool success = int.TryParse(Console.ReadLine(), out numberOfAdults);
            if(true == success){
                insertLoop = false;
            }
        }

        insertLoop = true;
        while(true == insertLoop){ //check in and out date
            Console.WriteLine("please enter the check in date: ");
            bool success = DateTime.TryParse(Console.ReadLine(), out checkInDate);
            Console.WriteLine("please enter the check out date: ");
            bool success2 = DateTime.TryParse(Console.ReadLine(), out checkOutDate);
            if (true == success || true == success2)
            {
                insertLoop = false;
            }
            
        }

        Console.WriteLine("information: " + hotelID + ", " + roomID + ", " + customerID + ", " + numberOfKids + ", " + numberOfAdults + ", " + checkInDate + ", " + checkOutDate);

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
            
            */
        
        Console.WriteLine(@"what extras do you want
1, Extra bed
2, Half board child
3, Half board adult
4, All-inclusive child
5, All-inclusive adult");

        string chosenExtrasString = Console.ReadLine();
        
        Console.WriteLine(chosenExtrasString);

        string[] chosenExtras = chosenExtrasString.Split(", ");

        foreach (string c in chosenExtras)
        {
            if ("1" == c)
            {
                
            }
        }
    }

}