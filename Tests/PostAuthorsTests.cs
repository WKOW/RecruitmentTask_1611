namespace RecruitmentTask_1611;

using FluentAssertions.Execution;
using Newtonsoft.Json.Linq;
using Xunit;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Helpers;
using Models;
using static ApiEndpoints;
using Bogus;
using FluentAssertions;

public class PostAuthorsTests
{
    private readonly ApiClient _client = new();
    Faker _faker = new Faker();

    [Fact]
    public async Task ShouldCreateAuthorAndReturnCorrectStatus()
    {
        //Given 
        var requestBody = new Author
        {
            ID = BaseHelpers.GetRandomTenDigitInt(),
            idBook = BaseHelpers.GetRandomTenDigitInt(),
            firstName = _faker.Name.FirstName(),
            lastName = _faker.Name.LastName()
        };
        //When 
        var response = await _client.PostAsync(AuthorsEndpoint, requestBody);
        // Then
        Assert.True(response.IsSuccessful,
            $"Request failed with status {response.StatusCode}\n"
            + RequestFailureHelper.GetFailuredRequestDetails(
                baseUrl: ApiClient.BaseUrl,
                endpoint: AuthorsEndpoint,
                requestContent: JsonConvert.SerializeObject(requestBody, Formatting.Indented),
                statusCode: response.StatusCode,
                actualResponseContent: response.Content,
                expectedResponseContent: JsonConvert.SerializeObject(requestBody, Formatting.Indented)));
    }

    [Fact]
    public async Task ShouldPostAuthorRequestAndResponseBeEqual()
    {
        //Given 
        var requestBody = new Author
        {
            ID = BaseHelpers.GetRandomTenDigitInt(),
            idBook = BaseHelpers.GetRandomTenDigitInt(),
            firstName = _faker.Name.FirstName(),
            lastName = _faker.Name.LastName()
        };
        //When 
        var response = await _client.PostAsync(AuthorsEndpoint, requestBody);

        //Then
        var responseJson = JObject.Parse(response.Content);
        try
        {
            using (new AssertionScope("Actor_field_validation"))
            {
                requestBody.ID.Should().Be((int)responseJson["id"]);
                requestBody.idBook.Should().Be((int)responseJson["idBook"]);
                requestBody.firstName.Should().Be(responseJson["firstName"].ToString());
                requestBody.lastName.Should().Be(responseJson["lastName"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Field values check has failed:\n" + ex.Message + "\n" +
                                RequestFailureHelper.GetFailuredRequestDetails(
                                    baseUrl: ApiClient.BaseUrl,
                                    endpoint: AuthorsEndpoint,
                                    requestContent: JsonConvert.SerializeObject(requestBody, Formatting.Indented),
                                    statusCode: response.StatusCode,
                                    actualResponseContent: response.Content,
                                    expectedResponseContent: JsonConvert.SerializeObject(requestBody,
                                        Formatting.Indented)));
        }
    }
}