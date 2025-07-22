using System.ComponentModel.DataAnnotations;

namespace CoreBusiness
{
    public class Product
    {
        // Primary key
        public int ProductId { get; set; }

        // Normally
        //      <label asp-for="Name"></label> in Razor view will be rendered as
        //      <label for="Name">Name</label>
        //      Content is the property name
        //
        // If we add DisplayAttribute to the property
        //      The content will be the string specified by Display(Name="")
        //
        // Serve as the foreign key and will define the relationship
        [Required]
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]        
        public int? Quantity { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public double? Price { get; set; }
        
        // Map CategoryId to Category object
        // Naviagtion property for EF core
        public Category? Category { get; set; }
    }
}
