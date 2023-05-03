namespace GhsflUtils.Models;

public class Club
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long? MenPoints { get; set; }

    public long? WomenPoints { get; set; }
    
    public bool IsAdmin { get; set; }
}