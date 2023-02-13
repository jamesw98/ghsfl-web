using GhsflUtils.Models;
using GhsflUtils.Shared;

namespace GhsflUtils.Services;

public class FencerService : GhsflService
{
    public FencerService(HttpClient client, AuthProvider auth) : base(client, auth)
    {
    }

    public async Task<List<Fencer>> GetFencersForUser()
    {
        var request = CreateRequest<string>(HttpMethod.Get, "https://localhost:7284/api/fencer", true, null);
        return await GetResponse<List<Fencer>>(request);
    }
}