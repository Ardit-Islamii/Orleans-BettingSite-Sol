using System.Reflection;
using Microsoft.OpenApi.Models;
using Orleans;
using Orleans_BettingSite_Client.ClientConfiguration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<ClusterClientHostedService>();

builder.Services.AddSingleton<IHostedService>(sp => sp.GetService<ClusterClientHostedService>());

builder.Services.AddSingleton<IClusterClient>(sp => sp.GetService<ClusterClientHostedService>().Client);

builder.Services.AddSingleton<IGrainFactory>(sp => sp.GetService<ClusterClientHostedService>().Client);

//Swagger configuration
builder.Services.AddSwaggerGen(cfg =>
{
    cfg.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Swagger API",
        Description = "Bet API crud",
        Version = "v1"
    });
    var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
    cfg.IncludeXmlComments(filePath);
});


builder.Services.AddControllers();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
