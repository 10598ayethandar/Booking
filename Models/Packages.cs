using System;

namespace Booking.API.Models
{
    public class Packages
    {  
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public string PackageDescription {get;set;}
        public int Price { get; set; }
        
    }
}