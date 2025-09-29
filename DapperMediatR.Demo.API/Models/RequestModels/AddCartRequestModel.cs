namespace DapperMediatR.Demo.API.Models.RequestModels
{
    public class AddCartRequestModel
    {
        public long UserId { get; set; }
        public List<int>? ProductIds { get; set; }
    }
}
