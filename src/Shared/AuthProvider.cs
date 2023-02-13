using GhsflUtils.Models;

namespace GhsflUtils.Shared;

public class AuthProvider
{
    public string AccessToken { get; set; }
    public Club UserClub { get; set; }
}