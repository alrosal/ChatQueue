using ChatQueue.Controllers;
using ChatQueue.Mocker;
using ChatQueue.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using Xunit;

namespace ChatQueue.Unit_Tests;

public class ChatQueueAPITests 
{
    readonly HttpClient _client;

    public ChatQueueAPITests()
    {
        
    }

    [Fact]
    public void Should_Return_Ok()
    {
        //Arrange
        var controller = new ChatSessionController(new ChatQueueService(new MockData()));

        //Act
        var result = controller.RequestAgent("12345");

        //Asset
        Assert.True((int?)HttpStatusCode.OK == ((IStatusCodeActionResult)result).StatusCode);
    }

}
