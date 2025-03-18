namespace Domain.Entities
{
    public class OrderItems
    {
        public Guid Id { get; set; }  
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public int Quantity { get; set; }
    }
}