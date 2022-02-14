using Orleans;
using Orleans.EventSourcing;
using Orleans.Providers;
using Orleans.Runtime;
using Orleans.Streams;
using Orleans_BettingSite.Events;
using Orleans_BettingSite.States;
using Orleans_BettingSite_Common.GrainInterfaces;

namespace Orleans_BettingSite.Grains
{
    [LogConsistencyProvider(ProviderName = "testLogStorage")]
    [StorageProvider(ProviderName = "amountStore")]
    [ImplicitStreamSubscription("default")]
    public class BetGrain : JournaledGrain<AmountState, BetEvent>, IBetGrain
    {
        private IAsyncObservable<BetMessage> consumer;
        internal ILogger logger;
        private StreamSubscriptionHandle<BetMessage> consumerHandle;

        private readonly IPersistentState<AmountState> _amount;

        public BetGrain([PersistentState("amount", "amountStore")] IPersistentState<AmountState> amount, ILoggerFactory loggerFactory)
        {
            _amount = amount;
            logger = loggerFactory.CreateLogger($"{this.GetType().Name}-{this.IdentityString}");
        }
        public override async Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider("bet");
            var stream = streamProvider.GetStream<BetMessage>(this.GetPrimaryKey(), "default");
            await stream.SubscribeAsync(OnNextAsync, OnErrorAsync, OnCompletedAsync);
        }
        public async Task OnNextAsync(BetMessage item, StreamSequenceToken token = null)
        {
            logger.Info("OnNextAsync({0}{1})", item, token != null ? token.ToString() : "null");
            await SetBetAmountAsync(item.Amount);
            await Task.CompletedTask;
        }
        public Task OnCompletedAsync()
        {
            logger.Info("OnCompletedAsync()");
            return Task.CompletedTask;
        }
        public Task OnErrorAsync(Exception ex)
        {
            logger.Info("OnErrorAsync({0})", ex);
            return Task.CompletedTask;
        }
        public Task<decimal> GetBetAmount()
        {
            return Task.FromResult(_amount.State.Amount);
        }
        public async Task<decimal> SetBetAmountAsync(decimal amount)
        {
            _amount.State.Amount = amount;

            RaiseEvent(new BetEvent(amount, "Bet amount set"));
            await ConfirmEvents();
            await _amount.WriteStateAsync();

            logger.LogInformation("Changed the bet amount of bet with id:{1} to {2}", this.GetPrimaryKey(), amount);
            return await Task.FromResult(_amount.State.Amount);
        }
    }
}
