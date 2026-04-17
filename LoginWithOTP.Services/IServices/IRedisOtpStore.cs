using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWithOTP.Services.IServices
{
    public interface IRedisOtpStore
    {
        Task SetOtpAsync(string mobile, string hash, TimeSpan expiry);
        Task<string?> GetOtpAsync(string mobile);
        Task RemoveOtpAsync(string mobile);
        Task<long> IncrementRateLimitAsync(string mobile, TimeSpan expiry);
    }
}
