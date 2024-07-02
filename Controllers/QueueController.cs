using DogBarberShopAPI.Attributes;
using DogBarberShopBL.Interfaces;
using DogBarberShopBL.Services;
using DogBarberShopDB.EF.Models;
using DogBarberShopEntites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogBarberShopAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly IQueueBL _queueBL;
            public QueueController(IQueueBL queueBL, ILogger<UserController> logger)
            {
            _queueBL = queueBL;
            _logger = logger;   
            }
        [HttpPost]
        public IActionResult addQueue([FromBody] QueueAddQueueDTO queueDTO)
        {
            try
            {
                _queueBL.addQueue(queueDTO);
                return Ok(new { message = "Queue added successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding queue");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllQueues()
        {
            try
            {
                List<Queue> queues = _queueBL.GetAllQueues();
                return Ok(queues);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
              }
        [Identificationcheck]
        
        [HttpPut("{id}")]
        public IActionResult UpdateQueue(int id, [FromBody] QueueUpdateDTO queueUpdateDTO)
        {
            try
            {
                _queueBL.UpdateQueue(id, queueUpdateDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Identificationcheck]
        [HttpDelete("{id}")]
        public IActionResult DeleteQueue(int id)
        {
            try
            {
                _queueBL.DeleteQueue(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



    }
}
