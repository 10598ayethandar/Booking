using System.Data.Common;
using System.Transactions;
using System.Buffers;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.ExceptionServices;
using System.Reflection;
using System.Buffers.Text;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Net.Cache;
using System.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Booking.API.Context;
using Booking.API.Controllers;
using Booking.API.Dtos;
using Booking.API.Dtos.ProductDto;
using Booking.API.Interfaces.Repos;
using Booking.API.Models;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Newtonsoft.Json;
using System.Diagnostics;
// using System.Linq.Dynamic.Core;
using System.Web;
using Booking.API.Dtos.ProductDto;
using System.Security.Claims;
using System.Net.Http;


namespace Booking.API.Repos
{
    public class ProductRepository : IProductRepository
    {
        private readonly BookingContext _context;
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        //private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ProductRepository(BookingContext context,
            IHttpContextAccessor httpContextAccessor)// IMapper mapper, IUserServices userServices, IBookingServices BookingServices, IMemberPointServices memberPointServices, IMiscellaneousRepository MiscellaneousRepo
        {
            _context = context;
            
            _httpContextAccessor = httpContextAccessor;
           
        }
        
         public async Task<GetProductDetailResponse> GetProductDetail(GetProductDetailRequest request, int userId, string token)
        {
            GetProductDetailResponse response = new GetProductDetailResponse();
            return response;
        }

    }
}




