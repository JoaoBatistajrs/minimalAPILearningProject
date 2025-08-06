namespace MinimalAPI.Domain.Models;

public class CarModel
{
    public string Model { get; set; } = default!;
    public string Make { get; set; } = default!;
    public int Year { get; set; }
}
