namespace WebApi_task1.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public virtual List<Order>? Orders { get; set; }
    }
}
