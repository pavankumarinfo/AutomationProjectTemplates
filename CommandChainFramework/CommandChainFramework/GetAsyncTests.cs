using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CommandChainFramework.HttpEngine;
using Shouldly;
using Xunit;

// ReSharper disable ConsiderUsingConfigureAwait

namespace CommandChainFramework.UnitTest
{
    public class GetAsyncTests
    {
        private static Uri RequestUri = new Uri("http://www.something.com/some/website");
        private const string ExpectedContent = "pew pew";

        [Theory]
        [MemberData(nameof(HttpEngine.GetAsyncTests.ValidHttpMessageOptions))]
        public async Task GivenAnHttpMessageOptions_GetAsync_ReturnsAFakeResponse(HttpMessageOptions options)
        {
            // Arrange.
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(options);

            // Act.
            await DoGetAsync(RequestUri,
                             ExpectedContent,
                             fakeHttpMessageHandler,
                             options.Headers);

            // Assert.
            options.NumberOfTimesCalled.ShouldBe(1);
        }

        [Theory]
        [MemberData(nameof(HttpEngine.GetAsyncTests.ValidSomeHttpMessageOptions))]
        public async Task GivenSomeHttpMessageOptions_GetAsync_ReturnsAFakeResponse(IList<HttpMessageOptions> lotsOfOptions)
        {
            // Arrange.
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(lotsOfOptions);

            // Act & Assert.
            await DoGetAsync(RequestUri,
                             ExpectedContent,
                             fakeHttpMessageHandler);
            lotsOfOptions.Sum(x => x.NumberOfTimesCalled).ShouldBe(1);
        }

        [Fact]
        public async Task GivenAnHttpResponseMessage_GetAsync_ReturnsAFakeResponse()
        {
            // Arrange.
            var httpResponseMessage = FakeHttpMessageHandler.GetStringHttpResponseMessage(ExpectedContent);
            var options = new HttpMessageOptions
            {
                HttpResponseMessage = httpResponseMessage
            };
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(options);

            // Act & Assert.
            await DoGetAsync(RequestUri,
                             ExpectedContent,
                             fakeHttpMessageHandler);
            options.NumberOfTimesCalled.ShouldBe(1);
        }

        [Fact]
        public async Task GivenSomeHttpResponseMessages_GetAsync_ReturnsAFakeResponse()
        {
            // Arrange.
            var messageResponse1 = FakeHttpMessageHandler.GetStringHttpResponseMessage(ExpectedContent);

            const string responseData2 = "Html, I am not.";
            var messageResponse2 = FakeHttpMessageHandler.GetStringHttpResponseMessage(responseData2);

            const string responseData3 = "<html><head><body>pew pew</body></head>";
            var messageResponse3 = FakeHttpMessageHandler.GetStringHttpResponseMessage(responseData3);

            var options = new List<HttpMessageOptions>
            {
                new HttpMessageOptions
                {
                    RequestUri = RequestUri,
                    HttpResponseMessage = messageResponse1
                },
                new HttpMessageOptions
                {
                    RequestUri = new Uri("http://www.something.com/another/site"),
                    HttpResponseMessage = messageResponse2
                },
                new HttpMessageOptions
                {
                    RequestUri = new Uri("http://www.whatever.com/"),
                    HttpResponseMessage = messageResponse3
                },
            };

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(options);

            // Act & Assert.
            await DoGetAsync(RequestUri,
                             ExpectedContent,
                             fakeHttpMessageHandler);
            options[0].NumberOfTimesCalled.ShouldBe(1);
            options[1].NumberOfTimesCalled.ShouldBe(0);
            options[2].NumberOfTimesCalled.ShouldBe(0);
        }

        [Fact]
        public async Task GivenAnUnauthorisedStatusCodeResponse_GetAsync_ReturnsAFakeResponseWithAnUnauthorisedStatusCode()
        {
            // Arrange.
            var messageResponse = FakeHttpMessageHandler.GetStringHttpResponseMessage("pew pew", HttpStatusCode.Unauthorized);
            var options = new HttpMessageOptions
            {
                RequestUri = RequestUri,
                HttpResponseMessage = messageResponse
            };
            var messageHandler = new FakeHttpMessageHandler(options);

            HttpResponseMessage message;
            using (var httpClient = new System.Net.Http.HttpClient(messageHandler))
            {
                // Act.
                message = await httpClient.GetAsync(RequestUri);
            }

            // Assert.
            message.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
            options.NumberOfTimesCalled.ShouldBe(1);
        }

        [Fact]
        public async Task GivenAValidHttpRequest_GetSomeDataAsync_ReturnsAFoo()
        {
            // Arrange.
            const string errorMessage = "Oh man - something bad happened.";
            var expectedException = new HttpRequestException(errorMessage);
            var messageHandler = new FakeHttpMessageHandler(expectedException);

            Exception exception;
            using (var httpClient = new System.Net.Http.HttpClient(messageHandler))
            {
                // Act.
                // NOTE: network traffic will not leave your computer because you've faked the response, above.
                exception = await Should.ThrowAsync<HttpRequestException>(async () => await httpClient.GetAsync(RequestUri));
            }

            // Assert.
            exception.Message.ShouldBe(errorMessage);
        }

        [Fact]
        public async Task GivenAFewCallsToAnHttpRequest_GetSomeDataAsync_ReturnsAFakeResponse()
        {
            // Arrange.
            var httpResponseMessage = FakeHttpMessageHandler.GetStringHttpResponseMessage(ExpectedContent);
            var options = new HttpMessageOptions
            {
                HttpResponseMessage = httpResponseMessage
            };
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(options);

            // Act & Assert
            await DoGetAsync(RequestUri,
                             ExpectedContent,
                             fakeHttpMessageHandler);
            await DoGetAsync(RequestUri,
                             ExpectedContent,
                             fakeHttpMessageHandler);
            await DoGetAsync(RequestUri,
                             ExpectedContent,
                             fakeHttpMessageHandler);
            options.NumberOfTimesCalled.ShouldBe(3);
        }

        [Theory]
        [MemberData(nameof(HttpEngine.GetAsyncTests.DifferentHttpMessageOptions))]
        public async Task GivenSomeDifferentHttpMessageOptions_GetAsync_ShouldThrowAnException(HttpMessageOptions options)
        {
            // Arrange.
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(options);
            var headers = new Dictionary<string, IEnumerable<string>>
            {
                {
                    "hi", new[]
                    {
                        "there"
                    }
                }
            };

            // Act.
            var exception = await Should.ThrowAsync<Exception>(() => DoGetAsync(RequestUri,
                             ExpectedContent,
                             fakeHttpMessageHandler,
                             headers));

            // Assert.
            exception.Message.ShouldStartWith("No HttpResponseMessage found for the Request => ");
            options.NumberOfTimesCalled.ShouldBe(0);
        }

        private static async Task DoGetAsync(Uri requestUri,
                                             string expectedResponseContent,
                                             FakeHttpMessageHandler fakeHttpMessageHandler,
                                             IDictionary<string, IEnumerable<string>> optionalHeaders =null)
        {
            requestUri.ShouldNotBeNull();
            expectedResponseContent.ShouldNotBeNullOrWhiteSpace();
            fakeHttpMessageHandler.ShouldNotBeNull();

            HttpResponseMessage message;
            string content;
            using (var httpClient = new System.Net.Http.HttpClient(fakeHttpMessageHandler))
            {
                // Do we have any Headers?
                if (optionalHeaders != null &&
                    optionalHeaders.Any())
                {
                    foreach (var keyValue in optionalHeaders)
                    {
                        httpClient.DefaultRequestHeaders.Add(keyValue.Key, keyValue.Value);
                    }
                }

                // Act.
                message = await httpClient.GetAsync(requestUri);
                content = await message.Content.ReadAsStringAsync();
            }

            // Assert.
            message.StatusCode.ShouldBe(HttpStatusCode.OK);
            content.ShouldBe(expectedResponseContent);
        }
    }
}