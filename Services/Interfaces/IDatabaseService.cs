public interface IDatabaseService
{
    void InsertNewMessageToChatHistory(string name, string message, string sentiment);
}