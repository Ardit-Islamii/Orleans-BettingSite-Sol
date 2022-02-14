using Orleans;

namespace Orleans_BettingSite.Interfaces
{
    public interface ITest : IGrainObserver
    {
        public void ReceiveMessage(string message);
    }
}
