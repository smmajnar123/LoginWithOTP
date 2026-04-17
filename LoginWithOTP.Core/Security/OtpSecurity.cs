using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LoginWithOTP.Core.Security
{
    public static class OtpSecurity
    {
        private static readonly string SecretKey = "SUPER_SECRET_KEY";

        public static string HashOtp(string mobile, string otp)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(SecretKey));
            var data = $"{mobile}:{otp}";
            return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(data)));
        }
    }
}
