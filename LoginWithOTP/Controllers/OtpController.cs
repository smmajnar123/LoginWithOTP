using LoginWithOTP.Shared.Constants;
using LoginWithOTP.Shared.ReqModels;
using Microsoft.AspNetCore.Mvc;

namespace LoginWithOTP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        [HttpPost("send")]
        public IActionResult SendOtp([FromBody] SendOtpRequest request)
        {
            if (request != null && !string.IsNullOrEmpty(request.MobileNumber))
            {
                return Ok(new { Message = MessageConstants.OTP_SENT_SUCCESS });
            }
            else
            {
                return BadRequest(new { Message = MessageConstants.OTP_SENT_FAILURE });
            }
        }
        [HttpPost("verify")]
        public IActionResult VerifyOtp([FromBody] VerifyOtpRequest request)
        {
            if (request.OtpCode == "123456")
            {
                return Ok(new { Message = MessageConstants.OTP_VERIFIED_SUCCESS });
            }
            else
            {
                return BadRequest(new { Message = MessageConstants.OTP_INVALID });
            }
        }
    }
}
