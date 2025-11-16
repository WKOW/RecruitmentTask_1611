using System.Net;

namespace RecruitmentTask_1611.Helpers;

public class RequestFailureHelper
{
    public static string GetFailuredRequestDetails(string baseUrl, string endpoint, HttpStatusCode statusCode,
        string? actualResponseContent, string? expectedResponseContent = "NA", string? requestContent = "NA",
        string? correlationId = "NA")
    {
        return
            "=== Failured Request Details ===\n" +
            $"BaseUrl: {baseUrl}\n" +
            $"Endpoint: {endpoint}\n" +
            $"Request: {requestContent}\n" +
            $"CorrelationId: {correlationId}\n" +
            $"Status Code: {statusCode}\n" +
            $"Expected Response: {expectedResponseContent}\n" +
            $"Actual Response: {actualResponseContent}\n" +
            "====================\n";
    }
}