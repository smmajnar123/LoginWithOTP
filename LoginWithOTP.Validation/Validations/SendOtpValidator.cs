using FluentValidation;
using LoginWithOTP.DTO.ReqModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWithOTP.Validation.Validations
{
    public class SendOtpValidator : AbstractValidator<SendOtpRequest>
    {
        public SendOtpValidator()
        {
            RuleFor(x => x.MobileNumber)
                .NotEmpty().WithMessage("Mobile number is required")
                .Length(10).WithMessage("Mobile number must be 10 digits")
                .Matches("^[0-9]*$").WithMessage("Mobile must be numeric");
        }
    }
}
