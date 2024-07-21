namespace DapperTrial.API.Models.RequestModels
{
    public class AddRedisRequestModel<T>
    {
        public string Key { get; set; }
        public T Action { get; set; }
        public TimeSpan ExpireTime { get; set; }
    }
}
