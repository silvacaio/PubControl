using DGPub.Domain.Core;
using DGPub.Domain.Tabs.Commands;
using DGPub.Domain.Tabs.Event;

namespace DGPub.Domain.Tabs.Handlers
{
    public interface ICloseTabHandler : IHandler<CloseTabCommand, Event<InvoiceTabEvent>>
    {

    }
}
