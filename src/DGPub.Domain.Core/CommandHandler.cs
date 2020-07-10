
namespace DGPub.Domain.Core
{
    public abstract class CommandHandler
    {
        private readonly IUnitOfWork _uow;

        protected CommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected bool Commit()
        {
            if (_uow.Commit()) return true;

            return false;
        }
    }
}
