using System.ComponentModel.DataAnnotations;

namespace WebAppMVC.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        // In WebAppMVC.csproj <Nullable>enable</Nullable> means property is non-nullable
        //      Solution:
        //          1. Use string?
        //          2. Assign a default value
        //
        // Decorate Name property with Required attribute for data annotation
        //      If the controller received data to set `Name` to null, ModelState.IsValid will be false
        [Required]
        public string Name { get; set; } = string.Empty;

        // If we use string as type, it means Description is non-nullable
        //      This will trigger validation to show
        //      The Description field is required
        //      In Views/Categories/Add.cshtml
        //      If we don't input the description field
        // public string Description { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}