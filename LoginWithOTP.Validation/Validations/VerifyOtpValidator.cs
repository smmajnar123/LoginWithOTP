using FluentValidation;
using LoginWithOTP.DTO.ReqModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWithOTP.Validation.Validations
{
    public class VerifyOtpValidator : AbstractValidator<VerifyOtpRequest>
    {
        public VerifyOtpValidator()
        {
            RuleFor(x => x.MobileNumber)
                .NotEmpty().WithMessage("Mobile number is required")
                .Matches(@"^[6-9]\d{9}$").WithMessage("Invalid mobile number");

            RuleFor(x => x.OtpCode)
                .NotEmpty().WithMessage("OTP is required")
                .Length(6).WithMessage("OTP must be 6 digits")
                .Matches(@"^\d{6}$").WithMessage("OTP must be numeric");
        }
    }
}
