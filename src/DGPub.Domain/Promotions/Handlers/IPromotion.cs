using DGPub.Domain.Tabs;

namespace DGPub.Domain.Promotions.Handlers
{
    public interface IPromotion
    {
        void Calcule(Tab tab);
    }
}
