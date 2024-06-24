namespace UserProduct.Domain.Entities
{
    public class Product : BaseEntity, IAuditable
    {
        public string ProductName { get; set; }

        public string? UserId { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }


        //Navigation
        public User? User { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }


        public static Product Create(string name, string userId, decimal price, string description)
        {
            return new Product
            {
                ProductName = name,
                UserId = userId,
                Price = price,
                Description = description

            };
        }
    }
}
