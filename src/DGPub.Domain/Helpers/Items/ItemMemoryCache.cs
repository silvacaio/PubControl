using DGPub.Domain.Items.Repositories;
using System;
using System.Collections.Generic;

namespace DGPub.Domain.Helpers.Items
{
    public class ItemMemoryCache : IItemCache
    {
        private Dictionary<Guid, string> _cache;
        private readonly IItemRepository _itemRepository;

        public ItemMemoryCache(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
            _cache = new Dictionary<Guid, string>();
        }

        public string GetName(Guid id)
        {
            if (_cache.TryGetValue(id, out string name))
                return name;

            name = _itemRepository.FindById(id)?.Name;
            _cache.Add(id, name);
            return name;
        }
    }
}
