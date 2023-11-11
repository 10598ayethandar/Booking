using System;

namespace Booking.API.Models
{
    public class Class
    {  
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int RequiredCredits { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration {get;set;}
        public int AvailableSlots { get; set; }
        public int CountryTypeId {get;set;}
        public virtual CountryType CountryType {get;set;}
    }
}