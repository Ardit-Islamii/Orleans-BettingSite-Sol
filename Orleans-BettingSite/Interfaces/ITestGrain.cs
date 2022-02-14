using Orleans;

namespace Orleans_BettingSite.Interfaces
{
    public interface ITestGrain : IGrainWithGuidKey
    {
        Task Subscribe(ITest observer);
        Task UnSubscribe(ITest observer);
        Task SendMessage(string message);
    }
}
