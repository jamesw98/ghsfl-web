using System.Net;
using GhsflUtils.Models;
using GhsflUtils.Shared;

namespace GhsflUtils.Services;

public class RoundService : GhsflService
{
    public RoundService(IConfiguration config, HttpClient client, AuthProvider auth) : base(config, client, auth)
    {
    }

    public async Task<List<Round>> GetRounds()
    {
        var request = CreateRequest<string>(HttpMethod.Get, "round", false, null);
        return await GetResponse<List<Round>>(request);
    }

    public async Task<HttpStatusCode> CreateRound(int roundNum, string description, string roundDate,
        string rosterSubmissionDate,
        string rosterSubmissionTime, string removeDate, string removeTime)
    {
        var round = new Round
        {
            RoundNumber = roundNum,
            Description = description,
            RoundDate = DateTime.Parse(roundDate),
            SubmitByDate = DateTime.Parse($"{rosterSubmissionDate} {rosterSubmissionTime}"),
            RemoveByDate = DateTime.Parse($"{removeDate} {removeTime}")
        };

        var request = CreateRequest<Round>(HttpMethod.Post, "round", true, round);
        return await GetResponseNoContent(request);
    }
}