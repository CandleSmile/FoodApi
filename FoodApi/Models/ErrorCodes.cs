using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApi.Models
{
    public enum ErrorCodes
    {
        ObjectNotFound = 1,
        ObjectAlreadyExists = 2,
        NoValidData = 3,
        NotValidRefreshToken = 4,
        UnexpectedError = 5,
        TokenWasRefreshed = 6,
        ErrorOnRefreshToken = 7,
    };
}
