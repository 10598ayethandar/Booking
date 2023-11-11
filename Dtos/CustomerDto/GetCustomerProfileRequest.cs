using System.ComponentModel.DataAnnotations;
namespace Booking.API.Dtos.CustomerDto
{
    public class GetCustomerProfileRequest
    {
       
       [Required]
       public int CustomerId {get;set;}
    }
}