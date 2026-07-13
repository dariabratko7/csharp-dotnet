using System.ComponentModel.DataAnnotations;

namespace ShopParallelism
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int AvailableQuantity { get; set; }


        [Timestamp]
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    }

    public class ProductHistory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int AvailableQuantity { get; set; }
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
        public string Action { get; set; } = string.Empty;
    }
}