using LoginWithOTP.DTO.ReqModels;
using LoginWithOTP.Services.IServices;
using LoginWithOTP.Shared.Constants;
using Microsoft.AspNetCore.Mvc;

namespace LoginWithOTP.Controllers
{
    [Route("api/otp")]
    [ApiController]
    public class OtpController(IOtpService otpService, ILogger<OtpController> logger) : ControllerBase
    {
        private readonly IOtpService _otpService = otpService;
        private readonly ILogger<OtpController> _logger = logger;

        [HttpPost("send")]
        public async Task<IActionResult> SendOtp([FromBody] SendOtpRequest request)
        {
            try
            {
                _logger.LogInformation("API Hit: Send OTP for {Mobile}", request.MobileNumber);
                var result = await _otpService.SendOtpAsync(request.MobileNumber);
                return Ok(new { success = true, message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending OTP for {Mobile}", request.MobileNumber);
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequest request)
        {
            string mobile = request.MobileNumber;
            string otp = request.OtpCode;
            _logger.LogInformation("API Hit: Verify OTP for {Mobile}", mobile);
            var isValid = await _otpService.VerifyOtpAsync(mobile, otp);

            if (!isValid)
            {
                _logger.LogWarning("OTP verification failed for {Mobile}", mobile);
                return BadRequest(new { success = false, message = MessageConstants.OTP_INVALID });
            }
            return Ok(new { success = true, message = MessageConstants.OTP_VERIFIED_SUCCESS });
        }
    }
}
