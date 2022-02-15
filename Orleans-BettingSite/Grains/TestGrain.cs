using Orleans;
using Orleans_BettingSite.ObserverManager;
using Orleans_BettingSite_Common.GrainInterfaces;

namespace Orleans_BettingSite.Grains
{
    public class TestGrain : Grain, ITestGrain
    {
        private readonly ObserverManager<ITest> _subsManager;
        private ITest _observer;
        public TestGrain(ILogger<TestGrain> logger)
        {
            _subsManager = new ObserverManager<ITest>(TimeSpan.FromMinutes(5), logger, "subs");
        }

        public Task Subscribe(ITest observer)
        {
            _observer = observer;
            _subsManager.Subscribe(observer, observer);
            return Task.CompletedTask;
        }

        public Task UnSubscribe()
        {
            if (_observer is not null)
            {
                _subsManager.Unsubscribe(_observer);
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }

        public Task SendMessage(string message)
        {
            _subsManager.Notify(x => x.ReceiveMessage(message));
            return Task.CompletedTask;
        }
    }
}
