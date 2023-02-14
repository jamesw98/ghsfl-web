using System.Net;
using GhsflUtils.Models;
using GhsflUtils.Shared;

namespace GhsflUtils.Services;

public class PointsService : GhsflService
{
    public PointsService(IConfiguration config, HttpClient client, AuthProvider auth) : base(config, client, auth)
    {
    }

    public async Task<HttpStatusCode> PostPoints(List<Result> results)
    {
        var request = CreateRequest<List<Result>>(HttpMethod.Post, "points", true, results);
        return await GetResponseNoContent(request);
    }
}