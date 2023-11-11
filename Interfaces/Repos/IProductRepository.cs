using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Booking.API.Dtos.ProductDto;
using Booking.API.Models;



namespace Booking.API.Interfaces.Repos
{
    public interface IProductRepository
    {
        
        Task<GetProductDetailResponse> GetProductDetail(GetProductDetailRequest request,int userId,string token);
        
        
    }
}
