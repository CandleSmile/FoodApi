using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public class Error
    {
        public Error(int errorCode, string message = "") {
            ErrorCode = errorCode;
            Message= message;
        }

        public int ErrorCode { get; set; }
        public string? Message { get; set; }
    }
}
