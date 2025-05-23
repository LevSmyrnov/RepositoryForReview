using Azure;
using Azure.AI.TextAnalytics;

public class AIService : IAIService
{
    public string GetSentimentForMessage(string message)
    {
        Uri endpoint = new(Environment.GetEnvironmentVariable("AI_ENDPOINT") ?? "<endpoint>");
        AzureKeyCredential credential = new(Environment.GetEnvironmentVariable("AI_API_KEY") ?? "<apiKey>");
        TextAnalyticsClient client = new(endpoint, credential);
        Response<DocumentSentiment> response = client.AnalyzeSentiment(message);
        DocumentSentiment docSentiment = response.Value;
        return docSentiment.Sentiment.ToString();
    }
}