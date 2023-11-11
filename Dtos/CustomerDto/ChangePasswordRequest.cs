using System.ComponentModel.DataAnnotations;
namespace Booking.API.Dtos.CustomerDto
{
    public class ChangePasswordRequest
    {
       
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; } 
    }
}

   