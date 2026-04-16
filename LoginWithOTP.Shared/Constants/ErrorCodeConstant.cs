using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWithOTP.Shared.Constants
{
    public static class ErrorCodeConstants
    {
        // 🔐 OTP Errors
        public const string OTP_NOT_FOUND = "OTP_001";
        public const string OTP_EXPIRED = "OTP_002";
        public const string OTP_INVALID = "OTP_003";
        public const string OTP_ALREADY_USED = "OTP_004";
        public const string OTP_MAX_ATTEMPTS = "OTP_005";

        // 📱 Mobile Errors
        public const string INVALID_MOBILE = "MOB_001";

        // 🔑 Authentication
        public const string UNAUTHORIZED = "AUTH_001";
        public const string TOKEN_EXPIRED = "AUTH_002";

        // ⚙️ General Errors
        public const string INTERNAL_SERVER_ERROR = "GEN_001";
        public const string BAD_REQUEST = "GEN_002";
    }
}
