using Moq;
using Microsoft.AspNetCore.Http;
using Formula1.Infrastructure.Middlewares;
using Formula1.Application.Interfaces.Services;

namespace Formula1.Infrastructure.Tests;

[TestFixture]
public class GlobalHttpRequestMiddlewareTests
{
    private Mock<IScopedLogService> _mockLogService;
    private Mock<RequestDelegate> _mockNextDelegate;
    private GlobalHttpRequestMiddleware _middleware;

    [SetUp]
    public void Setup()
    {
        _mockLogService = new Mock<IScopedLogService>();
        _mockNextDelegate = new Mock<RequestDelegate>();
        _middleware = new GlobalHttpRequestMiddleware(_mockNextDelegate.Object);
    }

    [Test]
    public async Task InvokeAsync_ShouldLogRequestUrl()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Request.Method = "GET";
        context.Request.Path = "/test-endpoint";

        _mockNextDelegate.Setup(next => next(It.IsAny<HttpContext>())).Returns(Task.CompletedTask);

        // Act
        await _middleware.InvokeAsync(context, _mockLogService.Object);

        // Assert
        var expectedContent = "GET /test-endpoint";
        _mockLogService.Verify(log =>
            log.Log(
                expectedContent,
                "requestUrl",
                It.IsAny<string>(), // CallerMemberName
                It.IsAny<string>(), // CallerFilePath
                It.IsAny<int>()     // CallerLineNumber
            ), Times.Once);
        _mockNextDelegate.Verify(next => next(It.IsAny<HttpContext>()), Times.Once);
    }
}
