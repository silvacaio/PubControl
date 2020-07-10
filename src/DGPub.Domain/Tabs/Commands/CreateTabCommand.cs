using DGPub.Domain.Core;

namespace DGPub.Domain.Tabs.Commands
{
    public class CreateTabCommand : Command
    {
        public CreateTabCommand(string customerName)
        {
            CustomerName = customerName;
        }

        public string CustomerName { get; set; }
        public override bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(CustomerName);
        }
    }
}
