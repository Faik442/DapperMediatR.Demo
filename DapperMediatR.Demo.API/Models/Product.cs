
namespace DapperMediatR.Demo.API.Models
{
    public class Product
    {
        public long ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
