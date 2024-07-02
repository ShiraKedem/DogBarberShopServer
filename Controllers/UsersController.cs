using DogBarberShopBL.Interfaces;
using DogBarberShopDB.EF.Models;
using DogBarberShopEntites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DogBarberShopAPI.Controllers
{[Route("api/[controller]/[action]")]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersBL _usersBL;

        public UsersController(ILogger<UsersController> logger, IUsersBL usersBL)
        {
            _logger = logger;
            _usersBL = usersBL;
        }


        //[MyCustom]
        //[AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLoginDTO userLoginDTO)
        {
            try
            {
                bool isExist = _usersBL.Login(userLoginDTO);
                if (!isExist)
                    return NotFound(new { message = "User does not exist" }); 
                return Ok(new { message = "התחברת בהצלחה" });
            }

            catch (Exception ex)
            {
                _logger.LogError($"Error on Login, Message: {ex.Message}," +
                    $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred", details = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            try
            {
                Response.Cookies.Delete("AccessToken");
                return Ok(new { message = "התנתקת בהצלחה" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on Logout, Message: {ex.Message}," +
                                       $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "שגיאה בלתי צפויה" });
            }
        }


    }
}
