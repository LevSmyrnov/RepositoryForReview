using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace RealtimeChatApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HistoryController : Controller
    {
        private readonly string _sqlConnectionString;

        public HistoryController()
        {
            _sqlConnectionString = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING") ?? "<sql_connection_string>";
        }

        [HttpGet]
        public async Task<ActionResult<IList<string>>> ChatHistory()
        {
            var rows = new List<string>();

            using var conn = new SqlConnection(_sqlConnectionString);
            conn.Open();

            var command = new SqlCommand("SELECT * FROM Messages", conn);
            using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    rows.Add($"{reader.GetInt32(0)}, {reader.GetString(1)}, {reader.GetDateTime(2)}, {reader.GetString(3)}, {reader.GetString(4)}");
                }
            }

            return View(rows);
        }
    }
}