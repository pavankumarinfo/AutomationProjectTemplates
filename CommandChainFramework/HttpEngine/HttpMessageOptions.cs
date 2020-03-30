using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CommandChainFramework.HttpEngine
{
    public class HttpMessageOptions
    {
        private const string AnyValue = "*";
        private HttpContent _httpContent;
        private string _httpContentSerialized;

        /// <summary>
        /// Optional: If not provided, then assumed to be *any* endpoint. Otherise, the endpoint we are trying to call/hit/test.
        /// </summary>
        public Uri RequestUri { get; set; }

        /// <summary>
        /// Optional: If not provided, then assumed to be *any* method.
        /// </summary>
        public HttpMethod HttpMethod { get; set; }

        /// <summary>
        /// Required: Need to know what type of response we will return.
        /// </summary>
        public HttpResponseMessage HttpResponseMessage { get; set; }

        /// <summary>
        /// Optional: If not provided, then assumed to be *no* content.
        /// </summary>
        public HttpContent HttpContent
        {
            get => _httpContent;
            set
            {
                _httpContent = value;
                _httpContentSerialized = _httpContent == null
                    ? null
                    : Task.Run(_httpContent.ReadAsStringAsync).Result;
            }
        }

        /// <summary>
        /// Optional: If not provided, then assumed to have *no* headers.
        /// </summary>
        public IDictionary<string, IEnumerable<string>> Headers { get; set; }

        // Note: I'm using reflection to set the value in here because I want this value to be _read-only_.
        //       Secondly, this occurs during a UNIT TEST, so I consider the expensive reflection costs to be
        //       acceptable in this situation.
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        public int NumberOfTimesCalled { get; private set; }

        public static HttpMessageOptions GetContent(string requestUri, string responseContent)
        {
            var options = new HttpMessageOptions
            {
                HttpMethod = HttpMethod.Get,
                RequestUri = new Uri(requestUri),
                HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(responseContent)
                }
            };
            return options;
        }

        public override string ToString()
        {
            var httpMethodText = HttpMethod?.ToString() ?? AnyValue;

            var headers = Headers != null &&
                          Headers.Any()
                              ? " || Headers: " + String.Join(":", Headers.Select(x => $"{x.Key}|{String.Join(",", x.Value)}"))
                              : "";
            return $"{httpMethodText} {RequestUri}{(HttpContent != null ? $" || body/content: {_httpContentSerialized}" : "")}{headers}";
        }
    }
}