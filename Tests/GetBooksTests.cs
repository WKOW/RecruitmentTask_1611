namespace RecruitmentTask_1611;

using FluentAssertions.Execution;
using Newtonsoft.Json.Linq;
using Xunit;
using System.Threading.Tasks;
using Helpers;
using static ApiEndpoints.Paths;
using static ApiEndpoints;
using Bogus;
using FluentAssertions;

public class GetBooksTests
{
    private readonly ApiClient _client = new();
    Faker _faker = new Faker();

    [InlineData(1, 100)]
    [InlineData(2, 200)]
    [InlineData(3, 300)]
    [InlineData(4, 400)]
    [InlineData(5, 500)]
    [InlineData(6, 600)]
    [InlineData(7, 700)]
    [InlineData(8, 800)]
    [InlineData(9, 900)]
    [InlineData(10, 1000)]
    [Theory]
    public async Task ShouldGetBookIdAndPageCountCheck(int id, int pageCount)
    {
        //When 
        var response = await _client.GetAsync(GetResourceById(BooksEndpoint, id));

        //Then
        var responseJson = JObject.Parse(response.Content);
        try
        {
            using (new AssertionScope("Book_Id_And_Page_Number_Validation"))
            {
                responseJson["id"].Value<int>().Should().Be(id);
                responseJson["pageCount"].Value<int>().Should().Be(pageCount);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Field values check has failed:\n" + ex.Message + "\n" +
                                RequestFailureHelper.GetFailuredRequestDetails(
                                    baseUrl: ApiClient.BaseUrl,
                                    endpoint: AuthorsEndpoint,
                                    statusCode: response.StatusCode,
                                    actualResponseContent: response.Content));
        }
    }
}