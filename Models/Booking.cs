using System;

namespace Booking.API.Models
{
    public class Booking
    {  
        public int BookingId {get;set;}
        public int ClassId { get; set; }
        public int CustomerId { get; set; }
        public int PackageId {get;set;}
        public DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; }

        public virtual Class Class {get;set;}
        public virtual Customer Customer {get;set;}
        public virtual Packages Packages {get;set;}

    }
}