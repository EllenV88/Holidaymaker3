using Npgsql;

string dbUri = "Host=localhost;Port=5455;Username=postgres;Password=postgres;Database=holidaymaker;";

await using var db = NpgsqlDataSource.Create(dbUri);

await using (var cmd = db.CreateCommand(@"CREATE TABLE IF NOT EXISTS hotels (
id SERIAL PRIMARY KEY,
name TEXT, 
address TEXT, 
city TEXT, 
country TEXT, 
beach_distance NUMERIC, 
center_distaance NUMERIC, 
rating NUMERIC, 
restaurant BOOLEAN, 
pool BOOLEAN, 
evening_entertainment BOOLEAN, 
kids_club BOOlEAN
)"))

{
    await cmd.ExecuteNonQueryAsync();
}