using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DBPub.API.ViewModels;
using DGPub.Domain.Core;
using DGPub.Domain.Tabs;
using DGPub.Domain.Tabs.Commands;
using DGPub.Domain.Tabs.Handlers;
using DGPub.Domain.Tabs.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DBPub.API.Controllers
{
    [Route("api")]
    public class TabController : BaseController
    {
        private readonly ITabHandler _tabHandler;
        private readonly ITabRepository _tabRepository;

        public TabController(ITabHandler tabHandler, ITabRepository tabRepository, IUser user) : base(user)
        {
            _tabHandler = tabHandler;
            _tabRepository = tabRepository;
        }

        [HttpGet]
        [Route("tab/open")]
        public IEnumerable<Tab> Get()
        {
            return _tabRepository.GetAllOpen(true);
        }

        [HttpGet]
        [Route("tab/{id:guid}")]          
        public Tab Get([Required] Guid id)
        {
            return _tabRepository.FindByIdWithItems(id);
        }

        [HttpPost]
        [Route("tab")]        
        public async Task<IActionResult> Post([FromBody]CreateTabViewModel customer)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _tabHandler.Handler(new CreateTabCommand(customer.CustomerName));
            if (result.Valid)
                return SuccessResponse(result.Value);

            return ErrorResponse(result.Error);
        }

        [HttpPut]
        [Route("tab/addItem")]
        public async Task<IActionResult> AddItem([FromBody]AddItemViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                var result = await _tabHandler.Handler(new AddItemTabCommand(model.TabId, model.ItemId));
                if (result.Valid)
                    return SuccessResponse(result.Value);

                return ErrorResponse(result.Error);
            }
            catch (Exception e)
            {
                return ErrorResponse(e.Message);
            }
        }

        [HttpPut]
        [Route("tab/close")]
        public async Task<IActionResult> Close([FromBody]TabViewModel tab)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _tabHandler.Handler(new CloseTabCommand(tab.Id));
            if (result.Valid)
                return SuccessResponse(result.Value);

            return ErrorResponse(result.Error);
        }

        [HttpPut]
        [Route("tab/reset")]
        public async Task<IActionResult> Reset([FromBody]TabViewModel tab)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _tabHandler.Handler(new ResetTabCommand(tab.Id));
            if (result.Valid)
                return SuccessResponse(result.Value);

            return ErrorResponse(result.Error);
        }

    }
}
