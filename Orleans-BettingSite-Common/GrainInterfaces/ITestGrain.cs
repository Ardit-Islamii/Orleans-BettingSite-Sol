using Orleans;

namespace Orleans_BettingSite_Common.GrainInterfaces
{
    public interface ITestGrain : IGrainWithGuidKey
    {
        Task Subscribe(ITest observer);
        Task UnSubscribe();
        Task SendMessage(string message);
    }
}
