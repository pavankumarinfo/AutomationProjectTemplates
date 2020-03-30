using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using CommandChainFramework.HttpEngine;
using Xunit;

// ReSharper disable ConsiderUsingConfigureAwait

namespace CommandChainFramework.UnitTest
{
    public class PostAsyncTests
    {
       
        [Theory]
        [MemberData(nameof(HttpEngine.PostAsyncTests.ValidPostHttpContent))]
        public async Task GivenAPostRequest_PostAsync_ReturnsAFakeResponse(HttpContent expectedHttpContent,
                                                                           HttpContent sentHttpContent)
        {
            // Arrange.
            Uri requestUri = new Uri("http://www.something.com/some/website");
            const string responseContent = "hi";
            var options = new HttpMessageOptions
            {
                HttpMethod = HttpMethod.Post,
                RequestUri = requestUri,
                HttpContent = expectedHttpContent, // This makes sure it's two separate memory references.
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(responseContent)
                }
            };

            var messageHandler = new FakeHttpMessageHandler(options);

            HttpResponseMessage message;
            string content;
            using (var httpClient = new System.Net.Http.HttpClient(messageHandler))
            {
                // Act.
                message = await httpClient.PostAsync(requestUri, sentHttpContent);
                content = await message.Content.ReadAsStringAsync();
            }

            // Assert.
            message.StatusCode.ShouldBe(HttpStatusCode.OK);
            content.ShouldBe(responseContent);
        }

        [Theory]
        [MemberData(nameof(HttpEngine.PostAsyncTests.InvalidPostHttpContent))]
        public async Task GivenAPostRequestWithIncorrectlySetupOptions_PostAsync_ThrowsAnException(HttpContent expectedHttpContent,
                                                                                                   HttpContent sentHttpContent)
        {
            // Arrange.
            const string responseContent = "hi";
            var options = new HttpMessageOptions
            {
                HttpMethod = HttpMethod.Post,
                RequestUri = new Uri("http://www.something.com/some/website"),
                HttpContent = expectedHttpContent,
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(responseContent)
                }
            };

            var messageHandler = new FakeHttpMessageHandler(options);
            InvalidOperationException exception;
            using (var httpClient = new System.Net.Http.HttpClient(messageHandler))
            {
                // Act.
                exception =
                    await
                        Should.ThrowAsync<InvalidOperationException>(
                            async () => await httpClient.PostAsync("http://www.something.com/some/website", sentHttpContent));
            }

            // Assert.
            exception.Message.ShouldStartWith("No HttpResponseMessage found");
        }
    }
}