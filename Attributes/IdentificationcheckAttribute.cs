using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using DogBarberShopBL.Interfaces;

namespace DogBarberShopAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class IdentificationcheckAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext _context, ActionExecutionDelegate _next)
        {
            var serviceProvider = _context.HttpContext.RequestServices;
            var queueBL = serviceProvider.GetService<IQueueBL>();
            var homeBL = serviceProvider.GetService<IHomeBL>();

            if (_context.ActionArguments.TryGetValue("id", out object idObj) && idObj is int id)
            {
                var queueUserId = queueBL.GetUserById(id);
                var userId = homeBL.GetUserId();
                if (queueUserId == userId)
                {
                    await _next(); // Continue with the action
                }
                else
                {
                    _context.Result = new ForbidResult(); 
                }
            }
            else
            {
                _context.Result = new BadRequestObjectResult("Invalid request"); 
            }
        }
    }
}
