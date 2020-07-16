using DGPub.Domain.Core;
using DGPub.Infra.Data.Context;

namespace DGPub.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DGPubContext _context;

        public UnitOfWork(DGPubContext context)
        {
            _context = context;

        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
