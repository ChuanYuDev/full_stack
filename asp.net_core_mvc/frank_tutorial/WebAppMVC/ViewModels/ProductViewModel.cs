using WebAppMVC.Models;

namespace WebAppMVC.ViewModels
{
    // Use ProductViewModel class to wrap around all of the information
    //      Including Product and Categories list 
    public class ProductViewModel
    {
        // IEnumerable is the base interface for all non-generic collections that can be enumerated 
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();

        public Product Product { get; set; } = new Product();
    }
}