using DogBarberShopBL.Interfaces;
using DogBarberShopBL.Services;
using DogBarberShopDB.EF.Models;
using DogBarberShopDB.Interfaces;
using DogBarberShopEntites;
using Microsoft.AspNetCore.Mvc;

namespace DogBarberShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;


        private readonly IUserBL _userBL;
        public UserController(IUserBL userBL,ILogger<UserController>logger)
        {
            _userBL = userBL;
            _logger = logger;
        }


        [HttpPost]
        public IActionResult addUserName([FromBody] UserAddUserNameDTO userDTO)
        {
            try
            {
                _userBL.addUserName(userDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
               
                List<User> users = _userBL.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on GetAllUsers, Message: {ex.Message}," +
                  $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet]
        public IActionResult GetUserNameById(int id)
        {
            try
            {
                string userName = _userBL.GetUserNameById(id);
                if (string.IsNullOrEmpty(userName))
                {
                    return NotFound("User not found");
                }
                return Ok(userName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error on GetUserNameById, Message: {ex.Message}," +
                  $" InnerException: {ex.InnerException}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }




    }
}