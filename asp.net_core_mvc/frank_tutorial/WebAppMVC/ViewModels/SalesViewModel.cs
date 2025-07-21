using System.ComponentModel.DataAnnotations;
using CoreBusiness;
using WebAppMVC.ViewModels.Validations;

namespace WebAppMVC.ViewModels
{
    public class SalesViewModel
    {
        // Represent which category the user has selected
        public int SelectedCategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; } = new List<Category>();

        public int SelectedProductId { get; set; }

        // The type is int
        //      We don't need Require attribute
        // [Required]

        // Validate QuantityToSell property
        //      Create a subfolder in current folder
        [Range(1, int.MaxValue)]
        [Display(Name = "Quantity")]
        [SalesViewModelEnsureProperQuantity]
        public int QuantityToSell { get; set; }
    }
}