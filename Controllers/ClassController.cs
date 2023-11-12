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
using Booking.API.Dtos.ClassDto;
using Microsoft.Extensions.Caching.Memory;
using Booking.API.Context;


namespace Class.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]    
    public class ClassController : ControllerBase
    {
        private readonly IClassRepository _repo;
       
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _cache;
        private readonly BookingContext _context;

        public ClassController(IClassRepository repo, IHttpContextAccessor httpContextAccessor, IMemoryCache cache, BookingContext context)
        {
            _repo = repo;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
            _context = context;
        }

        [HttpGet("GetClassListByCountryTypeId")]
        public async Task<IActionResult> GetClassListByCountryTypeId(int CountryTypeId)
        {
            try
            {
                var response = await _repo.GetClassListByCountryTypeId(CountryTypeId);
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

         [HttpPost("BookingClass")]
         [Authorize]
        public async Task<IActionResult> BookingClass(BookingClassRequest request)
        {
             try
            {
                #region Cache
                    // Check if the class is available in the cache
                    if (!_cache.TryGetValue(request.ClassId, out bool isClassAvailable))
                    {
                        // If not available, check the database and update the cache
                        isClassAvailable = IsClassAvailableInDatabase(request.ClassId);
                        _cache.Set(request.ClassId, isClassAvailable, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) // Set an expiration time
                        });
                    }

                    if (!isClassAvailable)
                    {
                       var response = await _repo.AddToWaitList(request);
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
                #endregion

                // Continue with the booking process
                var response = await _repo.BookingClass(request);
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

         private bool IsClassAvailableInDatabase(int classId)
        {
            // Check the database for class availability logic
            var classAvailableSlots = await _context.Class.Where(x => x.ClassId == classId).Select(x => x.AvailableSlots).FirstOrDefaultAsync();
            var bookedclassQtyList = await _context.Booking.Where(x => x.ClassId == classId).ToList();
            var bookedclassQty = bookedclassQtyList.Count();


            // For simplicity, assume the class is available
            if(classAvailableSlots != bookedclassQty)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        [HttpPost("CancelBookingClass")]
        [Authorize]
        public async Task<IActionResult> CancelBookingClass(CancelBookingClassRequest request)
        {
            try
            {
                var response = await _repo.CancelBookingClass(request);
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
