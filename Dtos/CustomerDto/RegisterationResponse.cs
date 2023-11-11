using System;
using System.Collections.Generic;
using Booking.API.Models;

namespace Booking.API.Dtos.UserDto
{
    public class RegistrationResponse : ResponseStatus
    {
       
        public string Token {get;set;}
        public Customer CustomerInfo {get;set;}
        
    }
    
}