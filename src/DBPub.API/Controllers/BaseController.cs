using DGPub.Domain.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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

        protected IActionResult ModelStateError()
        {
            HashSet<string> errors = new HashSet<string>();
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }

            return ErrorResponse(errors.ToArray());
        }

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
