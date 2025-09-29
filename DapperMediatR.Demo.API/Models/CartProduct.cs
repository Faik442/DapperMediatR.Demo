namespace DapperMediatR.Demo.API.Models
{
    public class CartProduct
    {
        public long CartProductId { get; set; }
        public long CartId { get; set; }
        public long ProductId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
