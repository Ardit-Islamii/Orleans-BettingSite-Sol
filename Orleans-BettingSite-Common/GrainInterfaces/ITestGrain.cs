using Orleans;

namespace Orleans_BettingSite_Common.GrainInterfaces
{
    public interface ITestGrain : IGrainWithGuidKey
    {
        Task Subscribe(ITest observer);
        Task UnSubscribe(ITest observer);
        Task SendMessage(string message);
    }
}
