using System.ComponentModel.DataAnnotations;
using CoreBusiness.Validations;

namespace CoreBusiness;

public class Genre
{
    public int Id { get; set; }    
    
    [Required(ErrorMessage = "You must fill the {0} field")]
    [StringLength(maximumLength: 50)]
    [FirstLetterUppercase]
    public required string Name { get; set; }
}