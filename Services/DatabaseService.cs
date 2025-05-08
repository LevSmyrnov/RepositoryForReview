using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

public class DatabaseService : IDatabaseService
{
    public void InsertNewMessageToChatHistory(string name, string message, string sentiment)
    {
        try
        {
            var sqlConnectionString = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
            using var conn = new SqlConnection(sqlConnectionString);
            conn.Open();
            var command = new SqlCommand(
                "INSERT INTO [dbo].[Messages] (Username, SentDateTime, MessageText, Sentiment) VALUES (@username, @sentDateTime, @messageText, @sentiment);"
                , conn);
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@username", name);
            command.Parameters.AddWithValue("@sentDateTime", new SqlDateTime(DateTime.Now));
            command.Parameters.AddWithValue("@messageText", message);
            command.Parameters.AddWithValue("@sentiment", sentiment);
            using SqlDataReader reader = command.ExecuteReader();
        }
        catch (Exception e)
        {
            System.Diagnostics.Trace.WriteLine(e.Message);
        }
    }
}