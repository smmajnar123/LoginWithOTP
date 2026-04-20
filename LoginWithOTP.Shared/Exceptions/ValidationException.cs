using LoginWithOTP.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWithOTP.Shared.Exceptions
{
    public class ValidationException(List<ApiError> errors) : BaseException("Validation Failed", "VALIDATION_ERROR", 400)
    {
        public List<ApiError> Errors { get; } = errors;
    }
}
