using System.Diagnostics;
using System.Net;
using System.Text.Json;
using GhsflUtils.Shared;
using System.Net.Http.Headers;
using GhsflUtils.Exceptions;

namespace GhsflUtils.Services;

public class GhsflService
{
    private string _baseUrl;
    
    protected HttpClient Client;
    protected AuthProvider Auth;
    
    protected GhsflService(IConfiguration config, HttpClient client, AuthProvider auth)
    {
        _baseUrl = config.GetConnectionString("GhsflApi");
        
        Client = client;
        Auth = auth;
    }

    /// <summary>
    /// creates an http request message
    /// </summary>
    /// <param name="method">GET, POST, PUT, etc</param>
    /// <param name="uri">the uri of the endpoint to hit</param>
    /// <param name="authenticated">whether or not this is an authenticated endpoint</param>
    /// <param name="content">optional: the content to send in the body</param>
    /// <typeparam name="T">the type of the content body, if there is any</typeparam>
    /// <returns>an http request message</returns>
    protected HttpRequestMessage CreateRequest<T>(HttpMethod method, string uri, bool authenticated, T? content)
    {
        var request = new HttpRequestMessage(method, $"{_baseUrl}{uri}");
        request.Headers.TryAddWithoutValidation("accept", "*/*");
        
        if (authenticated)
            request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {Auth.AccessToken}");

        if (content != null)
        {
            request.Content = new StringContent(JsonSerializer.Serialize(content, options: new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }));
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
        }

        return request;
    }
    
    /// <summary>
    /// gets a status code response from an endpoint
    /// </summary>
    /// <param name="request">the request to send</param>
    /// <returns>200 on success</returns>
    /// <exception cref="ApiException">if the status code isn't 200</exception>
    protected async Task<HttpStatusCode> GetResponseNoContent(HttpRequestMessage request)
    {
        var rawResponse = await Client.SendAsync(request);
        
        if (!rawResponse.IsSuccessStatusCode)
            throw new ApiException($"An error occured: {rawResponse.StatusCode}");

        return rawResponse.StatusCode;
    }
    
    /// <summary>
    /// gets a response from the api
    /// </summary>
    /// <param name="request">the request to send to the api</param>
    /// <typeparam name="T">the expected type of response</typeparam>
    /// <returns></returns>
    /// <exception cref="ApiException">if 200 wasn't returned from the api</exception>
    /// <exception cref="JsonParseException">if the response couldn't be parsed</exception>
    protected async Task<T> GetResponse<T>(HttpRequestMessage request)
    {
        try
        {
            var rawResponse = await Client.SendAsync(request);

            if (!rawResponse.IsSuccessStatusCode)
                throw new ApiException($"An error occured: {rawResponse.StatusCode}");

            var response = JsonSerializer.Deserialize<T>(await rawResponse.Content.ReadAsStringAsync(),
                options: new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (response == null)
                throw new JsonParseException("Could not parse response");

            return response;
        }
        catch (HttpRequestException)
        {
            throw new ApiException("Could not connect to the API, contact site admin.");
        }
    }
}