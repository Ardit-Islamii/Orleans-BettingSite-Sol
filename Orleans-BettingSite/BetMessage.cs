namespace Orleans_BettingSite
{
    [Serializable]
    public class BetMessage
    {
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public decimal Amount { get; set; }
        public string Message { get; set; }
        public BetMessage(decimal amount, string message)
        {
            Amount = amount;
            Message = message;
        }
    }
}
