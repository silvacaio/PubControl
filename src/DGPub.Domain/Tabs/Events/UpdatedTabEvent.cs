using DGPub.Domain.Core;

namespace DGPub.Domain.Tabs.Events
{

    public class UpdatedTabEvent : Event<UpdatedTabEvent>
    {
        public UpdatedTabEvent(Tab tab)
        {
            Tab = tab;
        }

        public Tab Tab { get; private set; }
    }
}
