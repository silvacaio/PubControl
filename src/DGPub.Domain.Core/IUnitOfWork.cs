using System;

namespace DGPub.Domain.Core
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
