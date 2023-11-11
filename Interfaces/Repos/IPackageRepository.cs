using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Booking.API.Models;



namespace Booking.API.Interfaces.Repos
{
    public interface IPackageRepository
    {
        
        Task<List<CountryType>> GetCountryType();
        Task<List<Packages>> GetAllPackageInfo(int CountryTypeId);
        Task<List<Packages>> GetMyPackageInfo(int CustomerId);
        
        
        
    }
}
