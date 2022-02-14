using Orleans;

namespace Orleans_BettingSite_Common.GrainInterfaces
{
    public interface ITest : IGrainObserver
    {
        public void ReceiveMessage(string message);
    }
}
