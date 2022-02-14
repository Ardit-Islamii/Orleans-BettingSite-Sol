namespace Orleans_BettingSite.Responses
{
    public class BetCreateResponse
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
