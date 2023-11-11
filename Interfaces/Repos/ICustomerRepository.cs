using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Booking.API.Dtos.CustomerDto;
using Booking.API.Models;



namespace Booking.API.Interfaces.Repos
{
    public interface ICustomerRepository
    {
        
        Task<RegistraionResponse> Registration(RegistrationRequest request);
        Task<LoginResponse> Login(LoginRequest request);
        Task<GetCustomerProfileResponse> GetCustomerProfileById(GetCustomerProfileRequest request);
        Task<ResponseStatus> ChangePassword(ChangePasswordRequest request);
        Task<ResponseStatus> ResetPassword(ResetPasswordRequest request);
        
        
    }
}
