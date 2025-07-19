using System.ComponentModel.DataAnnotations;
using WebAppMVC.Models;

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
        [Range(1, int.MaxValue)]
        [Display(Name = "Quantity")]
        public int QuantityToSell { get; set; }
    }
}