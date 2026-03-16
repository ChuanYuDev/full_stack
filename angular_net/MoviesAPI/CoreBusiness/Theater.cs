using System.ComponentModel.DataAnnotations;
using CoreBusiness.Interfaces;
using NetTopologySuite.Geometries;

namespace CoreBusiness;

public class Theater: IId
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(75)]
    public required string Name { get; set; }
    
    public required Point Location { get; set; }
}