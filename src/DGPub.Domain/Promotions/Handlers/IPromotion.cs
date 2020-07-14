using DGPub.Domain.Tabs;

namespace DGPub.Domain.Promotions.Handlers
{
    public abstract class IPromotion
    {
        public void Calcule(Tab tab)
        {
            if (!IsValid(tab))
                return;

            CalculeInternal(tab);
        }

        protected abstract bool IsValid(Tab tab);
        protected abstract void CalculeInternal(Tab tab);
    }
}
