using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWithOTP.Shared.Models
{
    public class ApiError
    {
        public string? Field { get; set; }
        public string? Message { get; set; }
    }
}
