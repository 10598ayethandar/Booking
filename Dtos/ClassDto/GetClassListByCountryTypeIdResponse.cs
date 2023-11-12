using System.ComponentModel.DataAnnotations;
namespace Booking.API.Dtos.ClassDto
{
    public class GetClassListByCountryTypeIdResponse
    {
       
       public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int RequiredCredits { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration {get;set;}
        public int AvailableSlots { get; set; }
        public string CountryTypeName {get;set;}
       
    }
}