using System.Data;
using System.Data.SqlClient;

FileStream FileStream = new FileStream("H:\\Bases_1C\\Swap\\raspisanie.csv", FileMode.OpenOrCreate);
FileStream fs = new FileStream("F:\\Program Files\\DelfaBot\\parser\\TestText.txt", FileMode.Create);
StreamReader Reader = new StreamReader(FileStream);
StreamWriter Writer = new StreamWriter(fs);
Writer.WriteLine(Reader.ReadToEnd());

Reader.Close();
Writer.Close();

FileStream notificationsStream = new FileStream("F:\\Program Files\\DelfaBot\\parser\\TestText.txt", FileMode.OpenOrCreate);

StreamReader streamReader = new StreamReader(notificationsStream);

string text = streamReader.ReadToEnd();

string[] splittedText = text.Split("\n");

await Select($"delete from marksOfUsers");
for (int i = 1; i < splittedText.Count(); i++)
{
    string[] line = splittedText[i].Split(";");

    for(int index = 0; index < line.Count(); index++)
    {
        Console.WriteLine(line[index]); 
    }
    Console.WriteLine();

    await Select($"INSERT into {tableName} (code, groupOfUser, date, mark, comment, course) values (N'{line[0]}',N'{line[1]}',N'{line[2]}',N'{line[3]}',N'{line[4]}',N'{line[6]}')");
}
//for (int i = 0; i < date.Rows.Count; i++)
//{
//    Console.WriteLine(date.Rows[i][2]);
//}
static async Task<System.Data.DataTable> Select(string selectSQL)
{
    System.Data.DataTable data = new System.Data.DataTable("dataBase");

    SqlConnection sqlConnection = new SqlConnection($"server=server-name;Trusted_connection=no;DataBase=database-name;User=User;PWD=PWD");
    sqlConnection.Open();

    SqlCommand sqlCommand = sqlConnection.CreateCommand();
    sqlCommand.CommandText = selectSQL;

    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
    sqlDataAdapter.Fill(data);

    sqlCommand.Dispose();
    sqlDataAdapter.Dispose();
    sqlConnection.Close();

    return data;
}//Запрос