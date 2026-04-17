using LoginWithOTP.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace LoginWithOTP.Services.Services
{
    public class RedisOtpStore(IConnectionMultiplexer redis) : IRedisOtpStore
    {
        private readonly IDatabase _db = redis.GetDatabase();

        public async Task SetOtpAsync(string mobile, string hash, TimeSpan expiry)
        {
            await _db.StringSetAsync($"otp:{mobile}", hash, expiry);
        }

        public async Task<string?> GetOtpAsync(string mobile)
        {
            return await _db.StringGetAsync($"otp:{mobile}");
        }

        public async Task RemoveOtpAsync(string mobile)
        {
            await _db.KeyDeleteAsync($"otp:{mobile}");
        }

        public async Task<long> IncrementRateLimitAsync(string mobile, TimeSpan expiry)
        {
            var key = $"otp:rate:{mobile}";
            var count = await _db.StringIncrementAsync(key);

            if (count == 1)
                await _db.KeyExpireAsync(key, expiry);

            return count;
        }
    }
}
