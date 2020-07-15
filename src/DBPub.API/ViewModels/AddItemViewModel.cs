using System;
using System.ComponentModel.DataAnnotations;

namespace DBPub.API.ViewModels
{
    public class AddItemViewModel
    {        
        [Display(Name = "Tab")]
        public Guid TabId { get; set; }
        [Display(Name = "Item")]
        public Guid ItemId { get; set; }
    }
}
