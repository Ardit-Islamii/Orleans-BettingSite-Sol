using Orleans;
using Orleans.Streams;
using Orleans_BettingSite.Events;
using Orleans_BettingSite.Requests;
using Orleans_BettingSite_Common.GrainInterfaces;
using Orleans_BettingSite_Common.Responses;

namespace Orleans_BettingSite.Grains
{
    public class IntermediateGrain : Grain, IIntermediateGrain
    {
        private IBetGrain currentBet;
        private IAsyncStream<BetEvent> stream;

        public override Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider("bet");
            stream = streamProvider.GetStream<BetEvent>(this.GetPrimaryKey(), "default");
            currentBet = GrainFactory.GetGrain<IBetGrain>(this.GetPrimaryKey());
            return base.OnActivateAsync();
        }

        public async Task<BetReadResponse> GetBetAsync(Guid betId)
        {
            var result = await currentBet.GetBetAmountAsync();
            var betReadResponse = new BetReadResponse()
            {
                Amount = result,
                Id = this.GetPrimaryKey(),
                LastUpdated = DateTime.UtcNow
            };
            return await Task.FromResult(betReadResponse);
        }

        public async Task<BetCreateResponse> SetBetAmountAsync(decimal amount)
        {
            await stream.OnNextAsync(new BetEvent(amount, "setBetAmountAsync"));
            var returnedResult = new BetCreateResponse()
            {
                Amount = amount,
                Id = this.GetPrimaryKey(),
                LastUpdated = DateTime.Now
            };
            return await Task.FromResult(returnedResult);
        }
    }
}
