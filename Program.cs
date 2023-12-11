using Npgsql;


/*
string dbUri2 = "Host=localhost;Port=5455;Username=postgres;Password=postgres;";
        
await using var db2 = NpgsqlDataSource.Create(dbUri2);

await using (var cmd = db2.CreateCommand("CREATE DATABASE holidaymaker"))
{ 
    await cmd.ExecuteNonQueryAsync();
}
*/

string dbUri = "Host=localhost;Port=5455;Username=postgres;Password=postgres;Database=holidaymaker;";

await using var db = NpgsqlDataSource.Create(dbUri);

await using (var cmd = db.CreateCommand(
                 "CREATE TABLE IF NOT EXISTS customers (id SERIAL PRIMARY KEY, first_name TEXT, last_name TEXT, email TEXT, number INTEGER, date_of_birth DATE)"))
{
    await cmd.ExecuteNonQueryAsync();
}

await using (var cmd = db.CreateCommand(@"CREATE TABLE IF NOT EXISTS hotels (
id SERIAL PRIMARY KEY,
name TEXT, 
address TEXT, 
city TEXT, 
country TEXT, 
beach_distance NUMERIC, 
center_distance NUMERIC, 
rating NUMERIC, 
restaurant BOOLEAN, 
pool BOOLEAN, 
evening_entertainment BOOLEAN, 
kids_club BOOLEAN,
price_extrabeds MONEY, 
price_halfboard_child MONEY, 
price_halfboard_adult MONEY, 
price_allinclusive_child MONEY, 
price_allinclusive_adult MONEY
)"))

{
    await cmd.ExecuteNonQueryAsync();
}

await using (var cmd = db.CreateCommand(@"CREATE TABLE IF NOT EXISTS bookings(
id SERIAL PRIMARY KEY, 
hotels_id INTEGER, 
rooms_id INTEGER,
customers_id INTEGER,
check_in_date DATE,
check_out_date DATE,
number_of_extrabeds INTEGER DEFAULT NULL CHECK (number_of_extrabeds <= 1), 
number_of_halfboard_child INTEGER, 
number_of_halfboard_adult INTEGER, 
number_of_allinclusive_child INTEGER, 
number_of_allinclusive_adult INTEGER, 
adults INTEGER, 
children INTEGER
)"))

{
    await cmd.ExecuteNonQueryAsync();
}

await using (var cmd = db.CreateCommand(@"CREATE TABLE IF NOT EXISTS rooms(
id SERIAL PRIMARY KEY, 
type TEXT, 
hotel_id INTEGER,
price MONEY
)"))

{
    await cmd.ExecuteNonQueryAsync();
}