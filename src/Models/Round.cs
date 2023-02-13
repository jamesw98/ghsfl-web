namespace GhsflUtils.Models;

public class Round
{
    public long Id { get; set; }
    public long RoundNumber { get; set; }
    public string Description { get; set; }
    public DateTime RoundDate { get; set; }
    public DateTime SubmitByDate { get; set; }
    public DateTime RemoveByDate { get; set; } 
}