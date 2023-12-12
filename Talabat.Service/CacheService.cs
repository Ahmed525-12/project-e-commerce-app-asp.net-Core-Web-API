using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StackExchange.Redis;
using Talabat.Core.Services;

namespace Talabat.Service
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;

        public CacheService(IConnectionMultiplexer Redis)
        {
            _database = Redis.GetDatabase();
        }

        public async Task CacheDataAsync(string Key, object Value, TimeSpan ExpireTime)
        {
            if (Value == null) return;

            var Option = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var SerializedValue = JsonSerializer.Serialize(Value, Option);
            await _database.StringSetAsync(Key, SerializedValue, ExpireTime);
        }

        public async Task<string?> GetCachedAsync(string Key)
        {
            var CachedData = await _database.StringGetAsync(Key);
            if (CachedData.IsNullOrEmpty) return null;
            return CachedData;
        }
    }
}