using Orleans.Streams;

namespace Orleans_BettingSite.Streams
{
    public class StreamObserver : IAsyncObserver<BetMessage>
    {
        public StreamObserver()
        {

        }
        public Task OnCompletedAsync() => Task.CompletedTask;

        public Task OnErrorAsync(Exception ex)
        {
            return Task.CompletedTask;
        }
        public Task OnNextAsync(BetMessage message, StreamSequenceToken token = null)
        {
            Console.WriteLine($"Getting bet with amount: {message.Amount}, last updated at {message.Created}");
            return Task.CompletedTask;
        }
    }
}
