using Microsoft.AspNetCore.Mvc;
using System;

namespace DBPub.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        public BaseController(                           //  IUser user
                              )
        {
            //_notifications = (DomainNotificationHandler)notifications;
            // _mediator = mediator;

            //if (user.IsAuthenticated())
            //{
            //    UserId = user.GetUserId();
            //}
        }

        protected Guid UserId { get; set; }

        protected IActionResult SuccessResponse(object result = null)
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        protected IActionResult ErrorResponse(string[] errors)
        {
            return BadRequest(new
            {
                success = false,
                errors
            });
        }

        protected bool ModelStateIsValid()
        {
            if (ModelState.IsValid) return true;

            return false;
        }
    }
}
