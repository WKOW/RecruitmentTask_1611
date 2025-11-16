namespace RecruitmentTask_1611;

using FluentAssertions.Execution;
using Newtonsoft.Json.Linq;
using Xunit;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Helpers;
using static Helpers.BaseHelpers;
using Models;
using static ApiEndpoints;
using Bogus;
using FluentAssertions;

public class PutBooksTests
{
    private readonly ApiClient _client = new();
    Faker _faker = new Faker();

    [Fact]
    public async Task ShouldPutCreateBookAndReturnCorrectStatus()
    {
        //Given 
        var id = GetRandomTenDigitInt();
        var requestBody = new Book
        {
            Id = id,
            Title = _faker.Lorem.Sentence(),
            Description = _faker.Lorem.Paragraph(),
            PageCount = new Random().Next(50, 1000),
            Excerpt = _faker.Lorem.Sentence(),
            PublishDate = DateTime.Now.ToString("o")
        };
        //When 
        var response = await _client.PutAsync($"{BooksEndpoint}/{id}", requestBody);
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
    public async Task ShouldPutCreateBookAndRequestAndResponseBeEqual()
    {
        //Given 
        var id = GetRandomTenDigitInt();
        var requestBody = new Book
        {
            Id = id,
            Title = _faker.Lorem.Sentence(),
            Description = _faker.Lorem.Paragraph(),
            PageCount = new Random().Next(50, 1000),
            Excerpt = _faker.Lorem.Sentence(),
            PublishDate = DateTime.Now.ToString("o")
        };

        //When 
        var response = await _client.PutAsync($"{BooksEndpoint}/{id}", requestBody);

        //Then
        var responseJson = JObject.Parse(response.Content);
        try
        {
            using (new AssertionScope("Book_validation"))
            {
                requestBody.Id.Should().Be((int)responseJson["id"]);
                requestBody.Title.Should().Be(responseJson["title"].ToString());
                requestBody.Description.Should().Be(responseJson["description"].ToString());
                requestBody.PageCount.Should().Be((int)responseJson["pageCount"]);
                requestBody.Excerpt.Should().Be(responseJson["excerpt"].ToString());
                requestBody.PublishDate.Should().Be(responseJson["publishDate"].ToString());
                // there is a bug in application 
                //See more details
                //↓ (expected)
                //    "2025-11-16T14:28:20.0557689+01:00"
                //    "16.11.2025 14:28:20"
                //    ↑ (actual)
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