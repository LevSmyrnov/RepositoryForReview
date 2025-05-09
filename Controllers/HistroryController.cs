using Microsoft.AspNetCore.Mvc;

namespace RealtimeChatApplication.Controllers
{
    [ApiController]
    [Route("history")]
    public class HistoryController : Controller
    {
        private readonly IDatabaseService _databaseService;

        public HistoryController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet("{rowsCount}")]
        public ActionResult ChatHistory(int rowsCount)
        {
            var rows = new List<string>();
            _databaseService.GetChatHistoryRows(rowsCount, rows);
            return View(rows);
        }
    }
}