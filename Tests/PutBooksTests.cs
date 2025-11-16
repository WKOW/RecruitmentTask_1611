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
                responseJson["title"].ToString().Should().Be(requestBody.Title);
                responseJson["id"].ToObject<int>().Should().Be(requestBody.Id);
                responseJson["description"].ToString().Should().Be(requestBody.Description);
                responseJson["pageCount"].ToObject<int>().Should().Be(requestBody.PageCount);
                responseJson["excerpt"].ToString().Should().Be(requestBody.Excerpt);
                var publishDateResponse = DateTimeOffset.Parse(responseJson["publishDate"].ToString()).ToString("yyyy-MM-ddTHH:mm");
                var publishDateRequest =  DateTimeOffset.Parse(requestBody.PublishDate).ToString("yyyy-MM-ddTHH:mm");
                publishDateResponse.Should().Be(publishDateRequest);
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