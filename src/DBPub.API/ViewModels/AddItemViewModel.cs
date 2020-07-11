using System;
using System.ComponentModel.DataAnnotations;

namespace DBPub.API.ViewModels
{
    public class AddItemViewModel
    {
        [Key]
        [Display(Name = "Tab")]
        public Guid TabId { get; set; }
        [Display(Name = "Item")]
        public Guid ItemId { get; set; }
        [Display(Name = "Quantidade")]
        public int Quantity { get; set; }

    }
}
