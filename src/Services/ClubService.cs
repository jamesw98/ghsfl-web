using GhsflUtils.Models;
using GhsflUtils.Shared;

namespace GhsflUtils.Services;

public class ClubService : GhsflService
{
    public ClubService(IConfiguration config, HttpClient client, AuthProvider auth) : base(config, client, auth)
    {
    }

    public async Task<Club> GetMyClub()
    {
        var request = CreateRequest<string>(HttpMethod.Get, "club/me", true, null);
        return await GetResponse<Club>(request);
    }

    public async Task<List<Club>> GetAllClubs()
    {
        var request = CreateRequest<string>(HttpMethod.Get, "club", false, null);
        return await GetResponse<List<Club>>(request);
    }
}