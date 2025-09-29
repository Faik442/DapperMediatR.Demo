namespace DapperMediatR.Demo.API.Models.ResponseModels
{
    public class AddProductResponseModel
    {
        public long ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
