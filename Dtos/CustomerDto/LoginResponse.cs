using System.ComponentModel.DataAnnotations;
using Booking.API.Dtos;

namespace Booking.API.Dtos.CustomerDto
{
    public class LoginResponse :ResponseStatus
    {
       public string Token {get;set;}
    }
}