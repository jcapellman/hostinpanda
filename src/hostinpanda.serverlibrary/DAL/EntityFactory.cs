using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using hostinpanda.clientlibrary;

using Newtonsoft.Json;
using StackExchange.Redis;

namespace hostinpanda.serverlibrary.DAL
{
    public class EntityFactory : IDisposable
    {
        private readonly ConnectionMultiplexer _redis;
        private IDatabase _db;
        
        public EntityFactory(string connectionString)
        {
            _redis = ConnectionMultiplexer.Connect(connectionString);
            _db = _redis.GetDatabase();
        }

        public async Task<ReturnContainer<List<T>>> GetListAsync<T>(string key)
        {
            var val = await _db.StringGetAsync(key);

            return !val.HasValue ? new ReturnContainer<List<T>>(null) : new ReturnContainer<List<T>>((List<T>)JsonConvert.DeserializeObject(val));
        }

        public async Task<ReturnContainer<bool>> WriteAsync<T>(string key, T value)
        {
            var result = await _db.StringSetAsync(key, JsonConvert.SerializeObject(value));

            return new ReturnContainer<bool>(result);
        }

        public void Dispose()
        {
            _redis?.Dispose();
        }
    }
}