namespace WebAppMVC.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        // In WebAppMVC.csproj <Nullable>enable</Nullable> means property is non-nullable
        //      Solution:
        //          1. Use string?
        //          2. Assign a default value
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}