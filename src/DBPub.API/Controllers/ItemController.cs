using DGPub.Domain.Core;
using DGPub.Domain.Items;
using DGPub.Domain.Items.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DBPub.API.Controllers
{   
    [Route("api")]
    [AllowAnonymous]
    public class ItemController : BaseController
    {
        private readonly IItemRepository _itemRepository;

        public ItemController(IItemRepository itemRepository, IUser user) : base(user)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("items")]
        public IActionResult Get()
        {
            return SuccessResponse(_itemRepository.GetAll());
        }

        [HttpGet]
        [Route("items/{id:guid}")]
        [AllowAnonymous]
        public Item Get(Guid id)
        {
            return _itemRepository.FindById(id);
        }
    }
}
