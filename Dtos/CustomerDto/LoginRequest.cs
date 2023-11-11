using System.ComponentModel.DataAnnotations;
namespace Booking.API.Dtos.CustomerDto
{
    public class LoginRequest
    {
       
       [Required]
       public string Email {get;set;}
       [Required]
       public string Password {get;set;}
    }
}