//using DGPub.Domain.Core;
//using DGPub.Domain.Tabs;
//using System;

//namespace DGPub.Domain.Promotions.Commands
//{
//    public class PromotionRunCommand : Command
//    {
//        public PromotionRunCommand(Guid tab)
//        {
//            TabId = tab;
//        }

//        public Guid TabId { get; private set; }

//        public override bool IsValid()
//        {
//            return TabId != Guid.Empty;
//        }
//    }
//}
using DGPub.Domain.Core;
using DGPub.Domain.Tabs;
using System;

namespace DGPub.Domain.Promotions.Commands
{
    public class PromotionRunCommand : Command
    {
        public PromotionRunCommand(Tab tab)
        {
            Tab = tab;
        }

        public Tab Tab { get; private set; }

        public override bool IsValid()
        {
            return Tab?.Items?.Count > 0;
        }
    }
}
