namespace WebApi_task1.Entities
{
    public class Order
    {
        public Order()
        {
            OrderDate = DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public int ProductId {  get; set; }
        public int CustomerId {  get; set; }

        public virtual Product? Product { get; set; }
        public virtual Customer? Customer { get; set; }

        
    }
}
