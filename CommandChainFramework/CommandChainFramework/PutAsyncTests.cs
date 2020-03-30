using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shouldly;
using CommandChainFramework.HttpEngine;
using Xunit;

// ReSharper disable ConsiderUsingConfigureAwait

namespace CommandChainFramework.UnitTest
{
    public class PutAsyncTests
    {
       
        [Theory]
        [MemberData(nameof(HttpEngine.PutAsyncTests.ValidPutHttpContent))]
        public async Task GivenAPutRequest_PutAsync_ReturnsAFakeResponse(HttpContent content)
        {
            // Arrange.
            var requestUri = new Uri("http://www.something.com/some/website");
            var options = new HttpMessageOptions
            {
                HttpMethod = HttpMethod.Put,
                RequestUri = requestUri,
                HttpContent = content,
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.NoContent)
            };

            var messageHandler = new FakeHttpMessageHandler(options);

            HttpResponseMessage message;
            using (var httpClient = new System.Net.Http.HttpClient(messageHandler))
            {
                // Act.
                message = await httpClient.PutAsync(requestUri, content);
            }

            // Assert.
            message.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// The actual httpClient call fails to match anything setup with the 'options'.
        /// </summary>
        [Theory]
        [MemberData(nameof(HttpEngine.PutAsyncTests.VariousOptions))]
        public async Task GivenADifferentPutRequestAndExpectedOutcome_PutAsync_ThrowsAnException(IEnumerable<HttpMessageOptions> options)
        {
            // Arrange.
            var content = new StringContent("hi");
            var messageHandler = new FakeHttpMessageHandler(options);

            InvalidOperationException exception;
            using (var httpClient = new System.Net.Http.HttpClient(messageHandler))
            {
                // Act.
                exception = await Should.ThrowAsync<InvalidOperationException>(async () => await httpClient.PutAsync("http://a.b.c.d./abcde", content));
            }

            // Act.
            exception.ShouldNotBeNull();
            exception.Message.ShouldStartWith("No HttpResponseMessage found for the Request => What was called:");
            exception.Message.ShouldContain($"{options.Count()}) "); // e.g. 1)  or   2)  .. etc.
        }
    }
}
