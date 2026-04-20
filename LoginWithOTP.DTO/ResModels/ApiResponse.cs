using LoginWithOTP.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWithOTP.DTO.ResModels
{
    public class ApiResponse<T> : BaseResponse
    {
        public T? Data { get; set; }
        public List<ApiError>? Errors { get; set; } = [];
    }
}
