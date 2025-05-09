public interface IDatabaseService
{
    void InsertNewMessageToChatHistory(string name, string message, string sentiment);
    void GetChatHistoryRows(int rowsCount, IList<string> rows);
}