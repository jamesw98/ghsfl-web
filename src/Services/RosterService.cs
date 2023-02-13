using GhsflUtils.Models;
using GhsflUtils.Shared;

namespace GhsflUtils.Services;

public class RosterService : GhsflService
{
    public RosterService(HttpClient client, AuthProvider auth) : base(client, auth)
    {
    }
    
    public async Task RemoveFencersFromRoster(long rosterId, List<long> fencerIds)
    {
        var request = CreateRequest<List<long>>(HttpMethod.Put,
            $"https://localhost:7284/api/roster/{rosterId}/fencers", true, fencerIds);
        await GetResponseNoContent(request);
    }

    public async Task<Roster> GetRoster(long rosterId)
    {
        var request = CreateRequest<string>(HttpMethod.Get, 
            $"https://localhost:7284/api/roster/{rosterId}", true, null);
        return await GetResponse<Roster>(request);
    }
    
    public async Task<List<Roster>> GetRostersForSchool()
    {
        var request = CreateRequest<string>(HttpMethod.Get,
            $"https://localhost:7284/api/roster/club/{Auth.UserClub.Id}", true, null);
        return await GetResponse<List<Roster>>(request);
    }

    public async Task<List<Fencer>> UploadRoster(RosterSubmission submission)
    {
        var request = CreateRequest<RosterSubmission>(HttpMethod.Post,
            "https://localhost:7284/api/roster/", true, submission);
        return await GetResponse<List<Fencer>>(request);
    }
    
    public async Task<List<Fencer>> UploadRosterAdmin(RosterSubmission submission, long clubId)
    {
        var request = CreateRequest<RosterSubmission>(HttpMethod.Post,
            $"https://localhost:7284/api/roster/club/{clubId}", true, submission);
        return await GetResponse<List<Fencer>>(request);
    }
}