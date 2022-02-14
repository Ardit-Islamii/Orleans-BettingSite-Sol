using Orleans;
using Orleans_BettingSite_Client.ClientConfiguration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<ClusterClientHostedService>();

builder.Services.AddSingleton<IHostedService>(sp => sp.GetService<ClusterClientHostedService>());

builder.Services.AddSingleton<IClusterClient>(sp => sp.GetService<ClusterClientHostedService>().Client);

builder.Services.AddSingleton<IGrainFactory>(sp => sp.GetService<ClusterClientHostedService>().Client);

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
