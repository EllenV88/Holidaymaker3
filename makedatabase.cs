using Npgsql;

namespace makedatabase;
class script 
{
    static async Task createdatabase()
    { 
        //OBS, MÅSTE GÖRA EN .CSPROJ FÖR ATT KÖRA PROGRAMMET, OCH DU MÅSTA LÄGGA TILL NPGSQL OCKSÅ
        //gör det såhär då detta inte kommer vara huvudprogrammet, men jag vill skjuta upp det ändå
            
        string dbUri = "Host=localhost;Port=5455;Username=postgres;Password=postgres;";
        
        await using var db = NpgsqlDataSource.Create(dbUri);

        await using (var cmd = db.CreateCommand("CREATE DATABASE holidaymaker"))
        { 
            await cmd.ExecuteNonQueryAsync();
        }
                
            
            /*
            bool addtables = true;
            while (addtables = true)
            {
                string dbUri = "Host=localhost;Port=5455;Username=postgres;Password=postgres;Database=holidaymaker;";

                await using var db = NpgsqlDataSource.Create(dbUri);


                //ska telefonnumret vara TEXT eller INTEGER?
                await using (var cmd = db.CreateCommand(
                                 "CREATE TABLE IF NOT EXISTS customer (id SERIAL PRIMARY KEY, first_name TEXT, last_name TEXT, email TEXT, number INTEGER, date_of_birth DATE)"))
                {
                    await cmd.ExecuteNonQueryAsync();
                }
            }*/
        }
    }
