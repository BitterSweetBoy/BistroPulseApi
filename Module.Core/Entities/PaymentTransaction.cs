namespace Module.Core.Entities
{
    public class PaymentTransaction
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int PaymentMethodId { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; } 
        public TransactionStatus Status { get; set; } // Estado de la transacción
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public Guid Reference { get; set; } = Guid.NewGuid(); // Código único de transacción
    }

    public enum TransactionType
    {
        Payment = 1,
        Refund = 2,
        RiderWithdrawal = 3
    }

    public enum TransactionStatus
    {
        Pending = 1,
        Completed = 2,
        Failed = 3
    }
}
