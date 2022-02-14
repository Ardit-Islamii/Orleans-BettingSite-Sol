using Orleans_BettingSite.Interfaces;

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
