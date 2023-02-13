using GhsflUtils.Models;
using GhsflUtils.Shared;

namespace GhsflUtils.Services;

public class ClubService : GhsflService
{
    public ClubService(HttpClient client, AuthProvider auth) : base(client, auth)
    {
    }

    public async Task<Club> GetMyClub()
    {
        var request = CreateRequest<string>(HttpMethod.Get, "https://localhost:7284/api/club/me", true, null);
        return await GetResponse<Club>(request);
    }

    public async Task<List<Club>> GetAllClubs()
    {
        var request = CreateRequest<string>(HttpMethod.Get, "https://localhost:7284/api/club", false, null);
        return await GetResponse<List<Club>>(request);
    }
}