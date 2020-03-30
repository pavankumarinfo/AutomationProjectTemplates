using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CommandChainFramework.HttpEngine;

// ReSharper disable ConsiderUsingConfigureAwait

namespace CommandChainFramework.HttpEngine
{
    public class PutAsyncTests
    {
        public static IEnumerable<object[]> ValidPutHttpContent
        {
            get
            {
                yield return new object[]
                {
                    // Sample json.
                    new StringContent("{\"id\":1}", Encoding.UTF8)
                };

                yield return new object[]
                {
                    new StringContent(JsonConvert.SerializeObject(DateTime.UtcNow.ToString("o")) , Encoding.UTF8)
                };

                yield return new object[]
                {
                    // Form key/values.
                    new FormUrlEncodedContent(new[]
                                              {
                                                  new KeyValuePair<string, string>("a", "b"),
                                                  new KeyValuePair<string, string>("c", "1")
                                              })
                };
            }
        }

        public static IEnumerable<object[]> VariousOptions
        {
            get
            {
                yield return new object[]
                {
                    new []
                    {
                        new HttpMessageOptions
                        {
                            HttpMethod = HttpMethod.Put,
                            RequestUri = new Uri("http://www.something.com/some/website"),
                            HttpContent = new StringContent(JsonConvert.SerializeObject(DateTime.UtcNow)),
                            HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.NoContent)
                        }
                    }
                };

                // Two options setup with two different Request Uri's.
                yield return new object[]
                {
                    new []
                    {
                        new HttpMessageOptions
                        {
                            HttpMethod = HttpMethod.Put,
                            RequestUri = new Uri("http://www.something.com/some/website"),
                            HttpContent = new StringContent(JsonConvert.SerializeObject(DateTime.UtcNow)),
                            HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.NoContent)
                        },
                        new HttpMessageOptions
                        {
                            HttpMethod = HttpMethod.Put,
                            RequestUri = new Uri("http://www.1.2.3.4/a/b"),
                            HttpContent = new StringContent(JsonConvert.SerializeObject(DateTime.UtcNow)),
                            HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.NoContent)
                        }
                    }
                };

                // Two options setup with two different Request Uri's.
                yield return new object[]
                {
                    new []
                    {
                        new HttpMessageOptions
                        {
                            HttpMethod = HttpMethod.Put,
                            RequestUri = new Uri("http://www.something.com/some/website"),
                            HttpContent = new StringContent(JsonConvert.SerializeObject(DateTime.UtcNow)),
                            Headers = new Dictionary<string, IEnumerable<string>>
                            {
                                {
                                    "Bearer", new[]
                                    {
                                        "pewpew",
                                        "1234"
                                    }
                                }
                            },
                            HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.NoContent)
                        },

                        new HttpMessageOptions
                        {
                            HttpMethod = HttpMethod.Put,
                            RequestUri = new Uri("http://www.1.2.3.4/a/b"),
                            HttpContent = new StringContent(JsonConvert.SerializeObject(DateTime.UtcNow)),
                            Headers = new Dictionary<string, IEnumerable<string>>
                            {
                                {
                                    "Bearer", new[]
                                    {
                                        "pewpew",
                                        "1234"
                                    }
                                }
                            },
                            HttpResponseMessage = new HttpResponseMessage(HttpStatusCode.NoContent)
                        }
                    }
                };
            }
        }
    }
}
