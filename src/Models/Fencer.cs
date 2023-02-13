using GhsflUtils.Enums;

namespace GhsflUtils.Models;

public class Fencer
{
    public long Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public long? Points { get; set; }
    public Genders Gender { get; set; }
    public long ClubId { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var other = (Fencer)obj;

        return string.Equals(FirstName, other.FirstName, StringComparison.CurrentCultureIgnoreCase) &&
               string.Equals(LastName, other.LastName, StringComparison.CurrentCultureIgnoreCase) &&
               ClubId == other.ClubId;
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}