using Orleans_BettingSite_Common.GrainInterfaces;

namespace Orleans_BettingSite.ObserverClasses
{
    public class Test : ITest
    {
        public void ReceiveMessage(string message)
        {
            Console.WriteLine($"Successfully received message: {message}");
        }
    }
}
