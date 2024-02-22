using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class APIException : APIResponse
    {
        public APIException(int statuscode, string message = null, string details = null) : base(statuscode,message)
        {
            Details = details;
        }
        public string Details { get; set; }
    }
}