namespace DapperTrial.API.Models.RequestModels
{
    public class AddCartRequestModel
    {
        public long UserId { get; set; }
        public List<int> ProductIds { get; set; }
    }
}
