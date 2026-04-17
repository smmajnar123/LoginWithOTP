using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWithOTP.Shared.Enums
{
    public enum OtpStatus
    {
        Pending = 1,
        Verified = 2,
        Expired = 3,
        Failed = 4,
        Blocked = 5
    }
}
