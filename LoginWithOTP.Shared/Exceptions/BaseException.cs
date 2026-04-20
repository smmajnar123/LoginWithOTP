using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWithOTP.Shared.Exceptions
{
    public class BaseException(string message, string code, int statusCode) : Exception(message)
    {
        public string Code { get; } = code;
        public int StatusCode { get; } = statusCode;
    }
}
