using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans_BettingSite.Grains;

var builder = WebApplication.CreateBuilder(args);

//Configuring the Silo
builder.Host.UseOrleans(siloBuilder =>
{
    siloBuilder.Configure((Action<ClusterOptions>)(options =>
    {
        options.ClusterId = "dev";
        options.ServiceId = "dev";
    }));
    siloBuilder.UseAdoNetClustering(options =>
    {
        options.Invariant = "Npgsql";
        options.ConnectionString = builder.Configuration.GetConnectionString("DatabaseConnectionString");
    });
    siloBuilder.AddSimpleMessageStreamProvider("bet", options =>
    {
        options.FireAndForgetDelivery = true;
    }).AddAdoNetGrainStorage("PubSubStore", options =>
    {
        options.Invariant = "Npgsql";
        options.ConnectionString = builder.Configuration.GetConnectionString("DatabaseConnectionString");
        options.UseJsonFormat = true;
    });
    siloBuilder.AddAdoNetGrainStorage("amountStore", options =>
    {
        options.Invariant = "Npgsql";
        options.ConnectionString = builder.Configuration.GetConnectionString("DatabaseConnectionString");
        options.UseJsonFormat = true;
    });
    siloBuilder.AddLogStorageBasedLogConsistencyProvider("testLogStorage");
    siloBuilder.ConfigureApplicationParts
    (
        parts => parts.AddApplicationPart(typeof(BetGrain).Assembly).WithReferences()
    );
    siloBuilder.ConfigureEndpoints
    (
        siloPort: 11111,
        gatewayPort: 30000,
        listenOnAnyHostAddress: true
    );
});

// Add services to the container.



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
