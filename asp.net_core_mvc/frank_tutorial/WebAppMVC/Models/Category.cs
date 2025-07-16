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
        // public string Description { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}