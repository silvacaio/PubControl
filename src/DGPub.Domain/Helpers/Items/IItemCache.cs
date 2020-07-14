using System;

namespace DGPub.Domain.Helpers.Items
{
    public interface IItemCache
    {
        string GetName(Guid id);
    }
}
