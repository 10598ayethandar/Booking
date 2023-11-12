using System.ComponentModel.DataAnnotations;
namespace Booking.API.Dtos.ClassDto
{
    public class CancelBookingClassRequest
    {
       
       [Required]
       public int CustomerId {get;set;}
       [Required]
       public int ClassId {get;set;}
       [Required]
       public int PackageId {get;set;}
    }
}