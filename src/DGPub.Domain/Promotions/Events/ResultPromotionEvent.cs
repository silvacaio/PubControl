using DGPub.Domain.Core;

namespace DGPub.Domain.Promotions.Events
{
    public class ResultPromotionEvent : EventBase// Event<ResultPromotionEvent>
    {
        public ResultPromotionEvent(bool applied)
        {
            Applied = applied;
            Alert = string.Empty;
        }

        public ResultPromotionEvent(bool applied, string alert)
        {
            Applied = applied;
            Alert = alert;
        }

        public bool Applied { get; private set; }
        public string Alert { get; private set; }
    }
}
