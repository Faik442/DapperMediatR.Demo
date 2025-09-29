
namespace DapperMediatR.Demo.API.Models
{
    public class Cart
    {
        public long CartId { get; set; }
        public User User { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
