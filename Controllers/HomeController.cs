using DogBarberShopBL.Interfaces;
using DogBarberShopEntites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DogBarberShopAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HomeController : Controller
    {
    
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IHomeBL _homeBL;
        public HomeController(ILogger<HomeController> logger, IMemoryCache memoryCache, IHomeBL homeBL)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _homeBL = homeBL;
        }
        [HttpPost]
        public IActionResult GetHeader()
        {
            try
            {
               string header = _homeBL.GetHeader();
                return Ok($"דף הבית - {header}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on GetHeader, Message: {ex.Message}," +
                    $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public IActionResult GetUserName()
        {
            try
            {
                string userName = _homeBL.GetUserName();
                return Ok(userName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on GetUserName, Message: {ex.Message}," +
                                       $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public IActionResult GetUserId()
        {
            try
            {
                int userId = _homeBL.GetUserId();
                return Ok(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on GetUserId, Message: {ex.Message}," +
                                       $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}


   