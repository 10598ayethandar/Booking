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
using Booking.API.Dtos.PackageDto;


namespace Package.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]    
    public class PackageController : ControllerBase
    {
        private readonly IPackageRepository _repo;
       
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(IPackageRepository repo, IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("GetCountryType")]
        public async Task<IActionResult> GetCountryType()
        {
            try
            {
                var response = await _repo.GetCountryType();
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

        [HttpGet("GetAllPackageInfo")]
        public async Task<IActionResult> GetAllPackageInfo(int CountryTypeId)
        {
            try
            {
                var response = await _repo.GetAllPackageInfo(CountryTypeId);
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

        [HttpGet("GetMyPackageInfo")]
        public async Task<IActionResult> GetMyPackageInfo(int CustomerId)
        {
            try
            {
                var response = await _repo.RegistrGetMyPackageInfoation(CustomerId);
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
