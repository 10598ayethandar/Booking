using System;

namespace Booking.API.Models
{
    public class Class
    {  
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int RequiredCredits { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ExpiredTime {get;set;}
        public int AvailableSlots { get; set; }
    }
}