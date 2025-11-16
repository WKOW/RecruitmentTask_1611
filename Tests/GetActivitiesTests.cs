namespace RecruitmentTask_1611;

using Xunit;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Helpers;
using Models;
using static ApiEndpoints;

public class GetActivitiesTests
{
    private readonly ApiClient _client = new();

    [Fact]
    public async Task ShouldReturnCorrectStatus()
    {
        //When 
        var response = await _client.GetAsync(ActivitiesEndpoint);
        // Then
        Assert.True(response.IsSuccessful,
            $"Request failed with status {response.StatusCode}\n"
            + RequestFailureHelper.GetFailuredRequestDetails(
                baseUrl: ApiClient.BaseUrl,
                endpoint: ActivitiesEndpoint,
                statusCode: response.StatusCode,
                actualResponseContent: response.Content));
    }

    [InlineData(30)]
    [Theory]
    public async Task ShouldReturnCorrectActivitiesNumber(int numberOfActivities)
    {
        //When
        var response = await _client.GetAsync(ActivitiesEndpoint);
        // Then
        Assert.False(string.IsNullOrEmpty(response.Content), $"Actual response is null or empty\n" +
                                                             RequestFailureHelper.GetFailuredRequestDetails(
                                                                 baseUrl: ApiClient.BaseUrl,
                                                                 endpoint: ActivitiesEndpoint,
                                                                 statusCode: response.StatusCode,
                                                                 actualResponseContent: response.Content));
        var activities = JsonConvert.DeserializeObject<List<Activity>>(response.Content);
        // And
        Assert.True(activities.Count == numberOfActivities,
            $"Expected {numberOfActivities} activities, but got {activities.Count}.\n" +
            RequestFailureHelper.GetFailuredRequestDetails(
                baseUrl: ApiClient.BaseUrl,
                endpoint: ActivitiesEndpoint,
                statusCode: response.StatusCode,
                actualResponseContent: response.Content));
    }

    [Fact]
    public async Task ShouldNotReturnActivitiesWithDueDateYesterdayOrEarlier()
    {
        //When 
        var response = await _client.GetAsync(ActivitiesEndpoint);
        // Then
        Assert.False(string.IsNullOrEmpty(response.Content), $"Actual response is null or empty\n" +
                                                             RequestFailureHelper.GetFailuredRequestDetails(
                                                                 baseUrl: ApiClient.BaseUrl,
                                                                 endpoint: ActivitiesEndpoint,
                                                                 statusCode: response.StatusCode,
                                                                 actualResponseContent: response.Content));
        var activities = JsonConvert.DeserializeObject<List<Activity>>(response.Content);
        // And
        Assert.False(CompareDays.ActivitiesDueYesterdayOrEarlier(activities),
            $"There are some activities due yesterday or due earlier date." +
            RequestFailureHelper.GetFailuredRequestDetails(
                baseUrl: ApiClient.BaseUrl,
                endpoint: ActivitiesEndpoint,
                statusCode: response.StatusCode,
                actualResponseContent: response.Content));
    }
}