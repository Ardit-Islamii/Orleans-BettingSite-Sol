using Orleans;

namespace Orleans_BettingSite_Common.GrainInterfaces
{
    public interface IBetGrain : IGrainWithGuidKey
    {
        Task<decimal> GetBetAmountAsync();
        Task<decimal> SetBetAmountAsync(decimal amount);
    }
}
