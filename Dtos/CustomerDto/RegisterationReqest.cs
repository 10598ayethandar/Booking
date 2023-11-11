using System.ComponentModel.DataAnnotations;
namespace Booking.API.Dtos.CustomerDto
{
    public class RegistrationRequest
    {
       
        public string FirstName {get;set;}
        public string LastName {get;set;}
        [Required]
        public string Email {get;set;}
        public string PhoneNo {get;set;}
        [Required]
        public string Password {get;set;}
    }
}