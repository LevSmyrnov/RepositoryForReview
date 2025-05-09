using RealtimeChatApplication.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR().AddAzureSignalR();
builder.Services.AddScoped<IDatabaseService, DatabaseService>()
                .AddScoped<IAIService, AIService>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
var app = builder.Build();

app.UseDefaultFiles();
app.UseRouting();
app.UseStaticFiles();
app.MapControllers();
app.MapHub<ChatHub>("/chat");
app.Run();