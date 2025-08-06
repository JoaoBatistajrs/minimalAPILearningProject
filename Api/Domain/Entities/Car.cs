using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalAPI.Domain.Entities;

public class Car
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Model { get; set; } = default!;

    [Required]
    [StringLength(100)]
    public string Make { get; set; } = default!;

    [Required]
    public int Year { get; set; }
}
