using System.ComponentModel.DataAnnotations;

namespace CoreBusiness
{
    public class Category
    {
        public int CategoryId { get; set; }

        // public string Name { get; set; }; warning
        //      Reference type default value is null
        //      A variable of a reference type T must be initialized with non-null, and can never be assigned a value that might be null
        //      A variable of a reference type T? can be initialized with null or assigned null, but is required to be checked against null before dereferencing
        //      
        //      The above statement Name will have default value null whereas the type is string which can't hold null value
        //
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