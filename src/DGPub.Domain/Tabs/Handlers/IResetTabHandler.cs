using DGPub.Domain.Core;
using DGPub.Domain.Tabs.Commands;
using DGPub.Domain.Tabs.Events;

namespace DGPub.Domain.Tabs.Handlers
{
    public interface IResetTabHandler : IHandler<ResetTabCommand, Event<UpdatedTabEvent>>
    {
    }
}
