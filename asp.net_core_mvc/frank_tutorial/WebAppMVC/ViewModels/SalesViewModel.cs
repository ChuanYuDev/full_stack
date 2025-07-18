using WebAppMVC.Models;

namespace WebAppMVC.ViewModels
{
    public class SalesViewModel
    {
        // Represent which category the user has selected
        public int SelectedCategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }
}