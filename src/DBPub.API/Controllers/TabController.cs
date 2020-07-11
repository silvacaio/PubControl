using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DBPub.API.ViewModels;
using DGPub.Application.Tabs.Handlers;
using DGPub.Domain.Tabs;
using DGPub.Domain.Tabs.Commands;
using DGPub.Domain.Tabs.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DBPub.API.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    public class TabController : BaseController
    {
        private readonly ITabHandler _tabHandler;
        private readonly ITabRepository _tabRepository;

        public TabController(ITabHandler tabHandler, ITabRepository tabRepository)
        {
            _tabHandler = tabHandler;
            _tabRepository = tabRepository;
        }

        [HttpGet]
        [Route("tab")]
        public IEnumerable<Tab> Get()
        {
            return _tabRepository.GetAll();
        }

        [HttpPost]
        [Route("tab")]
        //[Authorize(Policy = "PodeGravar")]
        public async Task<IActionResult> Post([Required]string customerName)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _tabHandler.Handler(new CreateTabCommand(customerName));
            if (result.Valid)
                return SuccessResponse(result);

            return ErrorResponse(new string[1] { result.Error });
        }

        [HttpPut]
        [Route("tab/addItem")]
        public async Task<IActionResult> AddItem(AddItemViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _tabHandler.Handler(new AddItemTabCommand(model.TabId, model.ItemId, model.Quantity));
            if (result.Valid)
                return SuccessResponse(result);

            return ErrorResponse(new string[1] { result.Error });
        }

        [HttpPost]
        [Route("tab/close")]
        public async Task<IActionResult> Close([Required]Guid tabId)
        {
            if (!ModelState.IsValid) return BadRequest();

            //var result = await _tabHandler.Handler(new AddItemTabCommand(model.TabId, model.ItemId, model.Quantity));
            //if (result.Valid)
            //    return SuccessResponse(result);

            // return ErrorResponse(new string[1] { result.Error });
            return BadRequest();
        }

        [HttpPost]
        [Route("tab/clean")]
        public async Task<IActionResult> Clean([Required]Guid tabId)
        {
            if (!ModelState.IsValid) return BadRequest();

            //var result = await _tabHandler.Handler(new AddItemTabCommand(model.TabId, model.ItemId, model.Quantity));
            //if (result.Valid)
            //    return SuccessResponse(result);

            // return ErrorResponse(new string[1] { result.Error });
            return BadRequest();
        }

    }
}
