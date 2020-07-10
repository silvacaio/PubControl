using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DGPub.Application.Tabs.Handlers;
using DGPub.Domain.Tabs.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace DBPub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TabController : BaseController
    {

        private readonly TabHandler _tabHandler;

        public TabController(TabHandler tabHandler)
        {
            _tabHandler = tabHandler;
        }

        [HttpPost]
        //[Route("eventos")]
        //[Authorize(Policy = "PodeGravar")]
        public async Task<IActionResult> Post([Required]string customerName)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _tabHandler.Handler(new CreateTabCommand(customerName));
            if (result.Valid)
                return SuccessResponse(result);

            return ErrorResponse(new string[1] { result.Error });
        }

    }
}
