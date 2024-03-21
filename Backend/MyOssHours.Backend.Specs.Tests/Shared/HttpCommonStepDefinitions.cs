using System.Net;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace MyOssHours.Backend.Specs.Tests.Shared;

[Binding]
public class HttpCommonStepDefinitions(ScenarioContext context)
{
    private readonly ScenarioContext _context = context ?? throw new ArgumentNullException(nameof(context));

    [Then(@"a (.*) is returned")]
    // NOT WORKING [Then(@"a {int} is returned")]
    public void ThenAStatusCodeIsReturned(int returnCode)
    {
        var statusCode = _context.Get<HttpStatusCode>("StatusCode");
        statusCode.Should().Be((HttpStatusCode)returnCode);
    }

}