namespace DapperMediatR.Demo.API.Models.ResponseModels
{
    public class AddCartResponseModel
    {
        public User User { get; set; }
        public List<Product> Products { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
