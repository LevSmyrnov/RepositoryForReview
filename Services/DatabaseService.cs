using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

public class DatabaseService : IDatabaseService
{
    private readonly string _sqlConnectionString;

    public DatabaseService()
    {
        _sqlConnectionString = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING") ?? "<sql_connection_string>";
    }


    public void GetChatHistoryRows(int rowsCount, IList<string> rows)
    {
        using var conn = new SqlConnection(_sqlConnectionString);
        conn.Open();

        var command = new SqlCommand($"SELECT TOP {rowsCount} * FROM [dbo].[Messages] ORDER BY MessageID DESC", conn);
        using SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                rows.Add($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetDateTime(2)}, {reader.GetString(3)}, {reader.GetString(4)}");
            }
        }
    }

    public void InsertNewMessageToChatHistory(string name, string message, string sentiment)
    {
        using var conn = new SqlConnection(_sqlConnectionString);
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
}