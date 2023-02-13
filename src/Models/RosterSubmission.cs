using GhsflUtils.Enums;

namespace GhsflUtils.Models;

public class RosterSubmission
{
    public long Round { get; set; }
    public Genders Gender { get; set; }
    public required List<Name> FencerNames { get; set; }
}