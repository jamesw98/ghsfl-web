using System.Net;
using GhsflUtils.Models;
using GhsflUtils.Shared;

namespace GhsflUtils.Services;

public class PointsService : GhsflService
{
    public PointsService(HttpClient client, AuthProvider auth) : base(client, auth)
    {
    }

    public async Task<PointsResult> GetPointsStanding()
    {
        var request = CreateRequest<string>(HttpMethod.Get, "https://localhost:7284/api/points", false, null);
        return await GetResponse<PointsResult>(request);
    }

    public async Task<HttpStatusCode> PostPoints(List<Result> results)
    {
        var request = CreateRequest<List<Result>>(HttpMethod.Post, "https://localhost:7284/api/points", true, results);
        return await GetResponseNoContent(request);
    }
}