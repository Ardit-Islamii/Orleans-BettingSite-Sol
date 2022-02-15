using Orleans;
using Orleans_BettingSite.ObserverManager;
using Orleans_BettingSite_Common.GrainInterfaces;

namespace Orleans_BettingSite.Grains
{
    public class TestGrain : Grain, ITestGrain
    {
        private readonly ObserverManager<ITest> _subsManager;
        private readonly ILogger<TestGrain> _logger;

        private ITest _observer;
        public TestGrain(ILogger<TestGrain> logger)
        {
            _subsManager = new ObserverManager<ITest>(TimeSpan.FromMinutes(5), logger, "subs");
            _logger = logger;
        }

        public Task Subscribe(ITest observer)
        {
            if (_observer is null)
            {
                _observer = observer;
                _subsManager.Subscribe(observer, observer);
                _logger.LogInformation("Successfully subscribed to the observer.");
                return Task.CompletedTask;
            }
            _logger.LogInformation("Already subscribed to the observer.");
            return Task.CompletedTask;
        }

        public Task UnSubscribe()
        {
            if (_observer is not null)
            {
                _subsManager.Unsubscribe(_observer);
                _logger.LogInformation("Successfully unsubscribed from the observer.");
                _observer = null;
                return Task.CompletedTask;
            }
            _logger.LogInformation("No observer to unsubscribe from.");
            return Task.CompletedTask;
        }

        public Task SendMessage(string message)
        {
            _subsManager.Notify(x => x.ReceiveMessage($"TestGrain with id: {this.GetPrimaryKey()} sends their regards: {message}"));
            return Task.CompletedTask;
        }
    }
}
