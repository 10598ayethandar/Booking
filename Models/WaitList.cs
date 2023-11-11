using System;

namespace Booking.API.Models
{
    public class WaitList
    {
        public int WaitListId {get;set;}
        public int CustomerId {get;set;}
        public int ClassId { get; set; }
        public int PackageId {get;set;}

        public virtual Class Class {get;set;}
        public virtual Customer Customer {get;set;}
        public virtual Packages Packages {get;set;}
        
    }
}