using DGPub.Domain.Core;
using DGPub.Domain.Items;
using DGPub.Domain.Items.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DBPub.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class ItemController : BaseController
    {
        private readonly IItemRepository _itemRepository;

        public ItemController(IItemRepository itemRepository, IUser user) : base(user)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        [Route("items")]
        public IEnumerable<Item> Get()
        {
            return _itemRepository.GetAll();
        }

        [HttpGet]
        [Route("items/{id:guid}")]
        public Item Get(Guid id)
        {
            return _itemRepository.FindById(id);
        }
    }
}
