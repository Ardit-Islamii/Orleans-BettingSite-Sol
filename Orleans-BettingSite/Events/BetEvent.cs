namespace Orleans_BettingSite.Events
{
    [Serializable]
    public class BetEvent
    {
        public decimal Amount { get; set; }
        public string Reason { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public BetEvent(decimal amount, string reason)
        {
            Amount = amount;
            Reason = reason;
        }
    }
}
