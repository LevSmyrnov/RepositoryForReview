using Microsoft.AspNetCore.SignalR;

namespace RealtimeChatApplication.Hubs
{
    public class ChatHub : Hub
    {
        public Task BroadcastMessage(string name, string message, IDatabaseService databaseService, IAIService aIService)
        {
            string sentiment = aIService.GetSentimentForMessage(message);
            databaseService.InsertNewMessageToChatHistory(name, message, sentiment);

            return Clients.All.SendAsync("broadcastMessage", name, message);
        }
    }
}