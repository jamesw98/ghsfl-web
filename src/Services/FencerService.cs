using GhsflUtils.Models;
using GhsflUtils.Shared;

namespace GhsflUtils.Services;

public class FencerService : GhsflService
{
    public FencerService(IConfiguration config, HttpClient client, AuthProvider auth) : base(config, client, auth)
    {
    }

    public async Task<List<Fencer>> GetFencersForUser()
    {
        var request = CreateRequest<string>(HttpMethod.Get, "fencer", true, null);
        return await GetResponse<List<Fencer>>(request);
    }
}