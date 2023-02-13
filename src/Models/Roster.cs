using GhsflUtils.Enums;

namespace GhsflUtils.Models;

public class Roster
{
    public long Id { get; set; }
    public long ClubId { get; set; }
    public Genders Gender { get; set; }
    public long Round { get; set; }
    public long SubmittedBy { get; set; }
    public DateTime SubmissionTime { get; set; }
    public List<Fencer>? Fencers { get; set; }
}

