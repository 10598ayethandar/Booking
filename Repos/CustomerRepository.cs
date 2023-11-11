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
        private  bool SendVerifyEmail(string email)
        {
            if(email != null)
            {
                return true;
            }
            else
            {
                return false;
            }
                
        }

        private string GenerateToken(string password)
        {
            //generate token with secret keys
            return password;
        }

        
        public async Task<RegistrationResponse> Registraion(RegistrationRequest request)
        {
            try
            {

               RegistrationResponse response = new RegistrationResponse();
               bool isCorrectEmail = false;
               isCorrectEmail = this.SendVerifyEmail(request.Email);
               if(isCorrectEmail)
               {
                    var user = await _context.Customer.Where(x => x.Email == request.Email).FirstOrDefaultAsync();
                    if (user == null)
                    {
                        var newCustomer = new Customer
                        {
                            FirstName = Request.FirstName,
                            LastName = Request.LastName,
                            Email = Request.Email,
                            Password = Request.Password,

                        };
                        var token = this.GenerateToken(reqeust.Password);
                        newCustomer.Token = token;
                        _context.Customer.Add(newCustomer);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        response.StatusCode = StatusCodes.Status401Unauthorized;
                        response.Message = "This Email is already registered!!!!";
                        return response;
                    }
                    
                    var newCustomerResponse = new Customer();
                    newCustomerResponse.FirstName = newCustomer.FirstName;
                    newCustomerResponse.LastName  = newCustomer.LastName;
                    newCustomerResponse.Email = newCustomer.Email; 
                    response.Token = token; 
                    response.CustomerInfo = newCustomerResponse;
                     return response; 
               }
               else
               {
                     response.StatusCode = StatusCodes.Status401Unauthorized;
                     response.Message = "Please Enter correct Email";
                     return response;
               }
            }
            catch (Exception e)
            {
                
                throw e;
            }
            
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            try
            {
                LoginResponse response = new LoginResponse();
                bool isCorrectEmail = false;
               isCorrectEmail = this.SendVerifyEmail(request.Email);
               if(isCorrectEmail)
               {
                    var user = await _context.Customer.Where(x => x.Email == request.Email && x.Password = request.Password).FirstOrDefaultAsync();
                    if(use != null)
                    {
                        response.Token = user.Token;
                        return response;
                    }
                    else
                    {
                        response.StatusCode = StatusCodes.Status401Unauthorized;
                        response.Message = "Please registration First";
                        return response;
                    }
 
               }
               else
               {
                    response.StatusCode = StatusCodes.Status401Unauthorized;
                     response.Message = "Please Enter correct Email";
                     return response;
               }

                
            }
            catch (Exception e)
            {
                
                throw e;
            }

        }

        public async Task<GetCustomerProfileResponse> GetCustomerProfileById(GetCustomerProfileRequest request)
        {
            try
            {
              return await _context.Customer
                .Where(x => x.Id  == request.CustomerId)
                .Select(x => new GetCustomerProfileResponse
                {
                    CustomerId = x.CustomerId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    Password = x.Password,
                    Token =  x.Token
                }).FirstOrDefaultAsync(); 
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}




