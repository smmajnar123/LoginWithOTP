using LoginWithOTP.Services.IServices;
using Microsoft.Extensions.Logging;

namespace LoginWithOTP.Services.Services
{
    public class OtpService(Repository.IRepository.IOtpRepository otpRepository, ILogger<OtpService> logger) : IOtpService
    {
        private readonly Repository.IRepository.IOtpRepository _otpRepository = otpRepository;
        private readonly ILogger<OtpService> _logger = logger;
        public async Task<string> SendOtpAsync(string mobile)
        {
            _logger.LogInformation("SendOtp request for {Mobile}", mobile);
            var recentCount = await _otpRepository.GetRecentOtpCountAsync(
                mobile, DateTime.UtcNow.AddMinutes(-5));

            if (recentCount >= 3)
            {
                _logger.LogWarning("Rate limit exceeded for {Mobile}", mobile);
                throw new Exception("Too many requests. Try later.");
            }

            var otp = new Random().Next(100000, 999999).ToString();
            var hashedOtp = Core.Security.OtpSecurity.HashOtp(mobile, otp);
            var otpRecord = new DbLayer.Collections.OtpRecordDocument
            {
                Id = Guid.NewGuid(),
                MobileNumber = mobile,
                OtpCode = hashedOtp,
                ExpiryTime = DateTime.UtcNow.AddMinutes(5),
                CreatedAt = DateTime.UtcNow,
                AttemptCount = 0,
                IsUsed = false
            };

            await _otpRepository.SaveOtpAsync(otpRecord);
            _logger.LogInformation("OTP generated for {Mobile}", mobile);
            return "OTP sent successfully";
        }

        public async Task<bool> VerifyOtpAsync(string mobile, string otp)
        {
            _logger.LogInformation("VerifyOtp request for {Mobile}", mobile);
            var otpRecord = await _otpRepository.GetLatestOtpAsync(mobile);
            if (otpRecord == null)
            {
                _logger.LogWarning("OTP not found for {Mobile}", mobile);
                return false;
            }
            if (otpRecord.ExpiryTime < DateTime.UtcNow)
            {
                _logger.LogWarning("OTP expired for {Mobile}", mobile);
                return false;
            }

            if (otpRecord.AttemptCount >= 3)
            {
                _logger.LogWarning("Max attempts reached for {Mobile}", mobile);
                return false;
            }

            var inputHash = Core.Security.OtpSecurity.HashOtp(mobile, otp);
            if (otpRecord.OtpCode != inputHash)
            {
                otpRecord.AttemptCount++;
                await _otpRepository.UpdateOtpAsync(otpRecord);
                _logger.LogWarning("Invalid OTP for {Mobile}", mobile);
                return false;
            }

            otpRecord.IsUsed = true;
            await _otpRepository.UpdateOtpAsync(otpRecord);
            _logger.LogInformation("OTP verified successfully for {Mobile}", mobile);
            return true;
        }
    }
}