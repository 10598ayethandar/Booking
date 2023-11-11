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
using Booking.API.Interfaces.Repos;
using Booking.API.Models;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Web;
using Booking.API.Dtos.CustomerDto;
using System.Security.Claims;
using System.Net.Http;


namespace Booking.API.Repos
{
    public class PackageRepository : IPackageRepository
    {
        private readonly BookingContext _context;
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public PackageRepository(BookingContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
           
        }
        public async Task<List<CountryType>> GetCountryType()
        {
            return await _context.CountryType
                .Where(x => x.IsActive  == true)
                .Select(x => new CountryType
                {
                    CountryTypeId = x.CountryTypeId,
                    Name = x.Name,
                    IsActive = x.IsActive,
                    
                }).ToListAsync(); 

        }
        public async Task<List<Packages>> GetAllPackageInfo(int CountryTypeId)
        {
            return await _context.Packages
                .Where(x => x.CountryTypeId  == CountryTypeId)
                .Select(x => new Packages
                {
                    PackageId = x.PackageId,
                    PackageName = x.PackageName,
                    PackageDescription = x.PackageDescription,
                    Price = x.Price,
                }).ToListAsync(); 
        }
        public async Task<List<Packages>> GetMyPackageInfo(int CustomerId)
        {
            var packageIdList = await _context.Booking.Where(x => x.CustomerId == CustomerId).Select(x =>x.PackageId).ToListAsync();
            return await _context.Packages
                .Where(x => packageIdList.Contains (x.PackageId))
                .Select(x => new Packages
                {
                    PackageId = x.PackageId,
                    PackageName = x.PackageName,
                    PackageDescription = x.PackageDescription,
                    Price = x.Price,
                }).ToListAsync(); 
        }
    }
}