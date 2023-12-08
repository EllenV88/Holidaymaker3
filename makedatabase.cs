using Npgsql;

//OBS, MÅSTE GÖRA EN .CSPROJ FÖR ATT KÖRA PROGRAMMET, OCH DU MÅSTA LÄGGA TILL NPGSQL OCKSÅ
//gör det såhär då detta inte kommer vara huvudprogrammet, men jag vill skjuta upp det ändå

string dbUri = "Host=localhost;Port=5455;Username=postgres;Password=postgres;";

await using var db = NpgsqlDataSource.Create(dbUri);

await using (var cmd = db.CreateCommand("CREATE DATABASE holidaymaker;"))
{
    await cmd.ExecuteNonQueryAsync();
}



string dbUri = "Host=localhost;Port=5455;Username=postgres;Password=postgres;Database=holidaymaker;";

await using var db = NpgsqlDataSource.Create(dbUri);

await using (var cmd = db.CreateCommand("CREATE TABLE customer (id INT, first_name VARCHAR(50), last_name VARCHAR(50), email VARCHAR(80), number INT, date_of_birth DATE)"))
{
    await cmd.ExecuteNonQueryAsync();
}