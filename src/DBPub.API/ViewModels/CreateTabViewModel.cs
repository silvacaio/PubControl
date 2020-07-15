using System.ComponentModel.DataAnnotations;

namespace DBPub.API.ViewModels
{
    public class CreateTabViewModel
    {
        [Display(Name = "CustomerName")]
        public string CustomerName { get; set; }
    }
}
