using Orleans;
using Orleans_BettingSite.Requests;
using Orleans_BettingSite.Responses;

namespace Orleans_BettingSite.Interfaces
{
    public interface IIntermediateGrain : IGrainWithGuidKey
    {
        Task<BetReadResponse> GetBetAsync(Guid betId);
        Task<BetCreateResponse> SetBetAmountAsync(decimal amount);
    }
}
