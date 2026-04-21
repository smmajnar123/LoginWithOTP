using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWithOTP.DTO.ReqModels
{
    public class VerifyOtpRequest
    {
        public required string MobileNumber { get; set; }
        public required string OtpCode { get; set; }
    }
}
