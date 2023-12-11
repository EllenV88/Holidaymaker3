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

await using (var cmd = db.CreateCommand(@"CREATE TABLE IF NOT EXISTS additions(
id SERIAL PRIMARY KEY, 
number_of_extrabeds INT DEFAULT NULL CHECK (number_of_extrabeds <= 1), 
number_of_halfboard_child INT DEFAULT NULL, 
number_of_halfboard_adult INT DEFAULT NULL, 
number_of_allinclusive_child INT DEFAULT NULL, 
number_of_allinclusive_adult INT DEFAULT NULL
)"))

{
    await cmd.ExecuteNonQueryAsync();
}