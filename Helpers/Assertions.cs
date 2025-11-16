using RecruitmentTask_1611.Models;

namespace RecruitmentTask_1611.Helpers;
using FluentAssertions;
using FluentAssertions.Execution;
using Newtonsoft.Json.Linq;

public class Assertions
{
    
public void AssertAuthorFields(Author expected, string responseJson)
{
    var json = JObject.Parse(responseJson);

    using (new AssertionScope("Author fields"))
    {
        json["ID"].ToString().Should().Be(expected.ID.ToString(), $"ID should match\n");
        json["idBook"].ToString().Should().Be(expected.idBook.ToString(), $"idBook should match\n");
        json["firstName"].ToString().Should().Be(expected.firstName, $"firstName should match\n");
        json["lastName"].ToString().Should().Be(expected.lastName, $"lastName should match\n");
    }
}
}