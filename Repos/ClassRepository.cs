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
using Booking.API.Dtos.ClassDto;
using System.Security.Claims;
using System.Net.Http;


namespace Booking.API.Repos
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BookingContext _context;
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public CustomerRepository(BookingContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
           
        }

        public bool AddPaymentCard(string paymentcard)
        {
                return (paymentcard !=null)?true:false;
        }
        public bool PaymentCharge(int price)
        {
            // To do payment integratin with third party
                return (pirce != 0)?true:false;
        }


        public async Task<List<GetClassListByCountryTypeIdResponse>> GetClassListByCountryTypeId(int CountryTypeId)
        {
            try
            {
                return await _context.Class
                .Where(x => x.CountryTypeId  == CountryTypeId)
                .Select(x => new GetClassListByCountryTypeIdResponse
                {
                    ClassId = x.ClassId,
                    ClassName = x.ClassName,
                    RequiredCredits = x.RequiredCredits,
                    StartTime = x.StartTime,
                    Duration = x.Duration,
                    AvailableSlots = x.AvailableSlots,
                    CountryTypeName = await _context.CountryType.Where(x =>x.CountryTypeId == x.CountryTypeId).Select( x=> x.Name).FirstOrDefaultAsync()
                }).ToListAsync(); 
                
            }
            catch (Exception e )
            {
                
                throw e;
            }
        }
        public asyncTask<ResponseStatus> BookingClass(BookingClassRequest request)
        {
            try
            {
                
                ResponseStatus  response = new ResponseStatus();
                

                #region Payment
                if(request != null)
                {
                    
                
                    if (request.PaymentCard != null)
                    {
                        var addtopaymentCard = AddPaymentCard(request.PaymentCard);
                        if (addtopaymentCard)
                        {
                            var PaymentCharge = _context.Class.Where(x => x.ClassId == request.ClassId).Select(x => x.Price).FirstOrDefaultAsync();
                            var IsPaymentCharge = PaymentCharge(PaymnetCharge);
                            if(IsPaymentCharge)
                            {
                                #regon Booking if payment is success
                                    var newclass = new Booking
                                    {
                                        ClassId = request.ClassId,
                                        CustomerId = request.CustomerId,
                                        PackageId = request.PackageId,
                                        BookingDate = DateTime.Now,
                                        BookingStatus = 'Success',

                                    };
                                    _context.Booking.Add(newclass);
                                    await _context.SaveChangesAsync();
                                #endregion
                            }
                        }
                    }
                }
                #endregion
                
            }
            catch (Exception e )
            {
                
                throw e;
            }
        }
        
        public asyncTask<ResponseStatus> CancelBookingClass(CancelBookingClassRequest request)
        {
            try
            {
                ResponseStatus response = new ResponseStatus();
                if (request != null)
                {
                    #region remove booking
                    var classToRemove = _Context.Booking.Where(e => e.ClassId == request.Class ClassId
                                                                && e.CustomerId == request.CustomerId
                                                                && e.PackageId == request.PackageId).ToList();

                    if (classToRemove.Any())
                    {
                        _dbContext.Class.RemoveRange(classToRemove);
                        _dbContext.SaveChanges();
                    }
                    #endregion

                
                    #region AddtoWishList
                    var IsAddWaitlist = await _context.WaitList.Where(e.ClassId == request.Class ClassId
                                                                && e.PackageId == request.PackageId).FirstOrDefaultAsync();
                    if(IsAddWaitlist != null)
                    {
                        var AddtoWaitListRequest = new BookingClassRequest
                        {
                            ClassId = IsAddWaitlist.ClassId,
                            CustomerId = IsAddWaitlist.CustomerId,
                            PackageId = IsAddWaitlist.PackageId,
                        };
    
                        var AddingToWaitList = await AddtoWaitList(AddtoWaitListRequest);
                    }
                    #endregion

                }
            }
            catch (Exception e )
            {
                
                throw e;
            }
        }

        public asyncTask<ResponseStatus> AddtoWaitList(BookingClassRequest request)
        {
            ResponseStatus response = new ResponseStatus();
            if(request != null)
            {
                var newWaitList = new BookingClassRequest
                {
                    ClassId = request.ClassId,
                    CustomerId = request.CustomerId,
                    PackageId = request.PackageId,
                };
                _context.WaitList.Add(newWaitList);
                await _context.SaveChangesAsync();
            }

            response.Message = "Successfully add to WaitList";
            return response;
        }
    }
}