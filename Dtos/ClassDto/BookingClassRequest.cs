using System.ComponentModel.DataAnnotations;
namespace Booking.API.Dtos.ClassDto
{
    public class BookingClassRequest
    {
       
       [Required]
       public int CustomerId {get;set;}
       [Required]
       public int ClassId {get;set;}
       [Required]
       public int PackageId {get;set;}
       public string PaymentCard {get;set;}
    }
}