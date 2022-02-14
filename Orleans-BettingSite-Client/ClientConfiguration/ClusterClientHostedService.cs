using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans_BettingSite.Interfaces;

namespace Orleans_BettingSite_Client.ClientConfiguration
{
    public class ClusterClientHostedService : IHostedService
    {
        public IClusterClient Client { get; }

        public ClusterClientHostedService()
        {
            Client = new ClientBuilder()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "dev";
                })
                .UseAdoNetClustering(options =>
                {
                    options.Invariant = "Npgsql";
                    options.ConnectionString = "User ID =postgres;Password=root;Server=localhost;Port=5432;Database=testdb;Integrated Security=true;Pooling=true;";
                })
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(ITestGrain).Assembly))
                .Build();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // A retry filter could be provided here.
            await Client.Connect();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Client.Close();

            Client.Dispose();
        }
    }
}
