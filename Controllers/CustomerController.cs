using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Booking.API.Interfaces.Repos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Booking.API.Dtos;
using System.Security.Claims;
using log4net;
using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using DeviceDetectorNET.Parser;
using Newtonsoft.Json;
using System.Diagnostics;
using Booking.API.Models;
using Booking.API.Dtos.CustomerDto;


namespace .API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]    
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repo;
       
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(IProductRepository repo, IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _httpContextAccessor = httpContextAccessor;
        }

        
           
        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(RegistrationRequest request)
        {
            try
            {
                var response = await _repo.Registration(request);
                if (response == null)
                {
                    return BadRequest(new {status = StatusCodes.Status400BadRequest,message = "No Result Found!"});
                }
                else if (response.StatusCode == StatusCodes.Status500InternalServerError)
                {
                    return StatusCode(response.StatusCode, response);
                }
                return Ok(response);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,e.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var response = await _repo.Login(request);
                if (response == null)
                {
                    return BadRequest(new {status = StatusCodes.Status400BadRequest,message = "No Result Found!"});
                }
                else if (response.StatusCode == StatusCodes.Status500InternalServerError)
                {
                    return StatusCode(response.StatusCode, response);
                }
                return Ok(response);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,e.Message);
            }
        }

        [HttpGet("GetCustomerProfileById")]
        public async Task<IActionResult> GetCustomerProfileById(GetCustomerProfileRequest request)
        {
            try
            {
                var response = await _repo.GetCustomerProfileById(request);
                if (response == null)
                {
                    return BadRequest(new {status = StatusCodes.Status400BadRequest,message = "No Result Found!"});
                }
                else if (response.StatusCode == StatusCodes.Status500InternalServerError)
                {
                    return StatusCode(response.StatusCode, response);
                }
                return Ok(response);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,e.Message);
            }
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                var response = await _repo.ResetPassword(request);
                if (response == null)
                {
                    return BadRequest(new {status = StatusCodes.Status400BadRequest,message = "No Result Found!"});
                }
                else if (response.StatusCode == StatusCodes.Status500InternalServerError)
                {
                    return StatusCode(response.StatusCode, response);
                }
                return Ok(response);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,e.Message);
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            try
            {
                var response = await _repo.ChangePassword(request);
                if (response == null)
                {
                    return BadRequest(new {status = StatusCodes.Status400BadRequest,message = "No Result Found!"});
                }
                else if (response.StatusCode == StatusCodes.Status500InternalServerError)
                {
                    return StatusCode(response.StatusCode, response);
                }
                return Ok(response);
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,e.Message);
            }
        }


    }
}