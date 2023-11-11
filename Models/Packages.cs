using System;

namespace Booking.API.Models
{
    public class Packages
    {  
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public string PackageDescription {get;set;}
        public int Price { get; set; }
        public DateTime ExpiredTime {get;set;}
        public int CountryTypeId {get;set;}
        public virtual CountryType CountryType {get;set;}
        
    }
}