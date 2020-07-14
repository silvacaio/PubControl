using DGPub.Domain.Core;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DBPub.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        public BaseController(IUser user)
        {
            if (user.IsAuthenticated())
            {
                UserId = user.GetUserId();
            }
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

        protected IActionResult ErrorResponse(string error)
        {
            return ErrorResponse(new string[1] { error });
        }

        protected bool ModelStateIsValid()
        {
            if (ModelState.IsValid) return true;

            return false;
        }
    }
}
