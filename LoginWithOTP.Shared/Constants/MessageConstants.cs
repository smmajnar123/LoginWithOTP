using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWithOTP.Shared.Constants
{
    public static class MessageConstants
    {
        // 🔐 OTP Messages
        public const string OTP_SENT_SUCCESS = "OTP sent successfully";
        public const string OTP_VERIFIED_SUCCESS = "OTP verified successfully";
        public const string OTP_INVALID = "Invalid OTP";
        public const string OTP_EXPIRED = "OTP has expired";
        public const string OTP_ALREADY_USED = "OTP already used";
        public const string OTP_NOT_FOUND = "OTP not found";
        public const string OTP_MAX_ATTEMPTS = "Maximum OTP attempts exceeded";
        public const string OTP_SENT_FAILURE = "Failed to send OTP. Please try again";

        // 📱 Mobile
        public const string INVALID_MOBILE = "Invalid mobile number";

        // 🔑 Auth
        public const string UNAUTHORIZED = "Unauthorized access";
        public const string TOKEN_EXPIRED = "Session expired. Please login again";

        // ⚙️ General
        public const string SUCCESS = "Request processed successfully";
        public const string BAD_REQUEST = "Invalid request";
        public const string INTERNAL_SERVER_ERROR = "Something went wrong. Please try again later";
    }
}
