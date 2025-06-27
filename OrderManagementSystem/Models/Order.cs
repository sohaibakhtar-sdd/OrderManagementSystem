namespace OrderManagementSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; } = false;
    }
}
