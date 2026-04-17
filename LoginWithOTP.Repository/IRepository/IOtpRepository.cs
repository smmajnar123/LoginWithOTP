using LoginWithOTP.DbLayer.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWithOTP.Repository.IRepository
{
    public interface IOtpRepository
    {
        Task SaveOtpAsync(OtpRecordDocument otp);
        Task<OtpRecordDocument?> GetLatestOtpAsync(string mobileNumber);
        Task UpdateOtpAsync(OtpRecordDocument otp);
        Task<int> GetRecentOtpCountAsync(string mobileNumber, DateTime fromTime);
    }
}
