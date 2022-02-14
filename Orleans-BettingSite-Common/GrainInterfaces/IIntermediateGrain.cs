using Orleans;
using Orleans_BettingSite.Requests;
using Orleans_BettingSite_Common.Responses;

namespace Orleans_BettingSite_Common.GrainInterfaces
{
    public interface IIntermediateGrain : IGrainWithGuidKey
    {
        Task<BetReadResponse> GetBetAsync(Guid betId);
        Task<BetCreateResponse> SetBetAmountAsync(decimal amount);
    }
}
