using RealtimeChatApplication.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSignalR().AddAzureSignalR();
builder.Services.AddScoped<IDatabaseService, DatabaseService>()
                .AddScoped<IAIService, AIService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseRouting();
app.UseStaticFiles();
app.MapHub<ChatHub>("/chat");
app.Run();