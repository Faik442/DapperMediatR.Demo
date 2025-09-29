using DapperMediatR.Demo.API.Models.RequestModels;
using StackExchange.Redis;
using System.Text.Json;

namespace DapperMediatR.Demo.API.Services.Redis
{
    public static class RedisService
    {
        private static IConnectionMultiplexer _redisCon;
        private static IDatabase _cache;
        public static void Initialize(IConnectionMultiplexer redisCon)
        {
            _redisCon = redisCon;
            _cache = redisCon.GetDatabase();
        }
        public static T? GetOrAdd<T>(AddRedisRequestModel<T> req) where T : class 
        {
            var result = _cache.StringGet(req.Key);
            if (!result.IsNullOrEmpty && result == JsonSerializer.Serialize(req.Action))
            {
                return JsonSerializer.Deserialize<T>(result!)!;
            }
            else if (!result.IsNullOrEmpty && result != JsonSerializer.Serialize(req.Action))
            {
                Clear(req.Key!);
            }
            Add(req);
            return null;
        }

        public static T Add<T>(AddRedisRequestModel<T> req)
        {
            var result = JsonSerializer.SerializeToUtf8Bytes(req.Action);
            _cache.StringSet(req.Key, result, req.ExpireTime);
            return JsonSerializer.Deserialize<T>(result)!;
        }

        public static bool Clear(string key)
        {
            return _cache.KeyDeleteAsync(key).Result;
        }
        public static T? Get<T>(string key) where T : class
        {
            var result = _cache.StringGet(key);
            if (result.IsNull)
            {
                return null;
            }
            return JsonSerializer.Deserialize<T>(result!);
        }
    }
}
