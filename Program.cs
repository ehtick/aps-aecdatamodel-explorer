var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var clientId = builder.Configuration["APS_CLIENT_ID"];
var clientSecret = builder.Configuration["APS_CLIENT_SECRET"];
var callbackUrl = builder.Configuration["APS_CALLBACK_URL"];
if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret) || string.IsNullOrEmpty(callbackUrl))
    throw new ApplicationException("Missing required environment variables APS_CLIENT_ID, APS_CLIENT_SECRET, or APS_CALLBACK_URL.");

builder.Services.AddSingleton(new APSService(clientId, clientSecret, callbackUrl));

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();
app.Run();
