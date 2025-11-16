namespace RecruitmentTask_1611.Helpers;

using RestSharp;
using System.Threading.Tasks;

public class ApiClient
{
    public readonly RestClient _client;
    public static readonly string BaseUrl = "http://fakerestapi.azurewebsites.net";

    public ApiClient()
    {
        _client = new RestClient(BaseUrl);
    }

    public async Task<RestResponse> GetAsync(string resource)
    {
        var request = new RestRequest(resource, Method.Get);
        return await _client.ExecuteAsync(request);
    }

    public async Task<RestResponse> PostAsync(string resource, object body)
    {
        var request = new RestRequest(resource, Method.Post);
        request.AddJsonBody(body);
        return await _client.ExecuteAsync(request);
    }

    public async Task<RestResponse> PutAsync(string resource, object body)
    {
        var request = new RestRequest(resource, Method.Put);
        request.AddJsonBody(body);
        return await _client.ExecuteAsync(request);
    }

    public async Task<RestResponse> DeleteAsync(string resource)
    {
        var request = new RestRequest(resource, Method.Delete);
        return await _client.ExecuteAsync(request);
    }
}