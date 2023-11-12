using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Booking.API.Dtos.ClassDto;
using Booking.API.Models;



namespace Booking.API.Interfaces.Repos
{
    public interface IClassRepository
    {
        
        Task<List<GetClassListByCountryTypeIdResponse>> GetClassListByCountryTypeId(int CountryTypeId);
        Task<ResponseStatus> BookingClass(BookingClassRequest request);
        Task<ResponseStatus> AddToWaitList(BookingClassRequest request);
        Task<ResponseStatus> CancelBookingClass(CancelBookingClassRequest request);   
    }
}
