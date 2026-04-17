using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWithOTP.Services.IServices
{
    public interface IOtpService
    {
        Task<string> SendOtpAsync(string mobile);
        Task<bool> VerifyOtpAsync(string mobile, string otp);
    }
}
