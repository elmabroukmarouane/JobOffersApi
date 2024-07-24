using JobsOffer.Api.Server.Extensions.Add;
using JobsOffer.Api.Server.Extensions.Use;
using JobsOffer.Api.Server.RealTime.Class;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSWAGGER();
builder.Services.AddConnection(builder.Configuration, builder.Environment);
builder.Services.AddSERVICES(builder.Configuration, builder.Environment);
builder.Services.AddJWT(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddCORS(builder.Configuration);
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCORS(app.Configuration);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseJWT();

app.MapControllers();
app.MapHub<RealTimeHub>("/realTimeHub");

await app.RunAsync();
