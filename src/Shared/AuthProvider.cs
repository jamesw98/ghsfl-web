using GhsflUtils.Models;

namespace GhsflUtils.Shared;

public class AuthProvider
{
    public required string AccessToken { get; set; }
    public required Club UserClub { get; set; }
}