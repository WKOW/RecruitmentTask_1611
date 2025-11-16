namespace RecruitmentTask_1611;

using Xunit;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Helpers;
using Models;
using static ApiEndpoints;
using Bogus;
using FluentAssertions;

public class DeleteAuthorsTests
{
    private readonly ApiClient _client = new();
    Faker _faker = new ();

    [Fact]
    public async Task ShouldCreateAndDeleteAuthorSuccesfully()
    {
        //Given 
        var requestBody = new Author
        {
            ID = BaseHelpers.GetRandomTenDigitInt(),
            idBook = BaseHelpers.GetRandomTenDigitInt(),
            firstName = _faker.Name.FirstName(),
            lastName = _faker.Name.LastName()
        };
        var postResponse = await _client.PostAsync(AuthorsEndpoint, requestBody);
        postResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        //postResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        //invalid response it should be 201 preferably
        
        //When 
        var deleteResponse = await _client.DeleteAsync($"{AuthorsEndpoint}/{requestBody.ID}");
        deleteResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        //deleteResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        //invalid response it should be 201 preferably, and 404 not found if entity do not exist 
        
        //Then
        var getResponse = await _client.GetAsync($"{AuthorsEndpoint}/{requestBody.ID}");
        try
        {
            getResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
        catch
        {
            throw new Exception($"Request failed with status {getResponse.StatusCode}\n"
                                + RequestFailureHelper.GetFailuredRequestDetails(
                                    baseUrl: ApiClient.BaseUrl,
                                    endpoint: AuthorsEndpoint,
                                    requestContent: JsonConvert.SerializeObject(requestBody, Formatting.Indented),
                                    statusCode: getResponse.StatusCode,
                                    actualResponseContent: getResponse.Content,
                                    expectedResponseContent: JsonConvert.SerializeObject(requestBody,
                                        Formatting.Indented)));
        }
    }
}