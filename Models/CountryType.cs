using System;

namespace Booking.API.Models
{
    public class CountryType
    {  
        public int CountryTypeId { get; set; }
        public string Name { get; set; }
        public bool IsActive {get;set;}

    }
}