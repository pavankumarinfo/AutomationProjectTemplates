using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CommandChainFramework.HttpEngine;
using CommandChainFramework.HttpEngine;


// ReSharper disable ConsiderUsingConfigureAwait

namespace CommandChainFramework.HttpEngine
{
    public static class GetAsyncTests
    {
        private static Uri RequestUri = new Uri("http://www.something.com/some/website");
        private const string ExpectedContent = "pew pew";

        private static List<HttpMessageOptions> GetSomeFakeHttpMessageOptions(HttpMessageOptions option)
        {
            return new List<HttpMessageOptions>
            {
                new HttpMessageOptions
                {
                    HttpMethod = HttpMethod.Get,
                    RequestUri = new Uri("http://some/url"),
                    HttpResponseMessage = SomeFakeResponse
                },
                new HttpMessageOptions
                {
                    HttpMethod = HttpMethod.Get,
                    RequestUri = new Uri("http://another/url"),
                    HttpResponseMessage = SomeFakeResponse
                },
                option
            };
        }

        private static HttpResponseMessage SomeFakeResponse => new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(ExpectedContent)
        };

        public static IEnumerable<object[]> ValidHttpMessageOptions
        {
            get
            {
                yield return new object[]
                {
                    // All wildcards.
                    new HttpMessageOptions
                    {
                        HttpResponseMessage = SomeFakeResponse
                    }
                };

                // Any Uri but has to be a GET.
                yield return new object[]
                {
                    new HttpMessageOptions
                    {
                        HttpMethod = HttpMethod.Get,
                        HttpResponseMessage = SomeFakeResponse
                    }
                };

                // Has to match GET + URI.
                // NOTE: Http GET shouldn't have a content/body.
                yield return new object[]
                {
                    new HttpMessageOptions
                    {
                        HttpMethod = HttpMethod.Get,
                        RequestUri = RequestUri,
                        HttpResponseMessage = SomeFakeResponse
                    }
                };

                // Has to match GET + URI + Header
                yield return new object[]
                {
                    new HttpMessageOptions
                    {
                        HttpMethod = HttpMethod.Get,
                        RequestUri = RequestUri,
                        Headers = new Dictionary<string, IEnumerable<string>>
                        {
                            {"Bearer", new[]
                                {
                                    "pewpew"
                                }
                            }
                        },
                        HttpResponseMessage = SomeFakeResponse
                    }
                };

                // Has to match GET + URI + Header (but with a different case)
                yield return new object[]
                {
                    new HttpMessageOptions
                    {
                        HttpMethod = HttpMethod.Get,
                        RequestUri = RequestUri,
                        Headers = new Dictionary<string, IEnumerable<string>>
                        {
                            {"Bearer", new[]
                                {
                                    "PEWPEW"
                                }
                            }
                        },
                        HttpResponseMessage = SomeFakeResponse
                    }
                };
            }
        }

        public static IEnumerable<object[]> ValidSomeHttpMessageOptions
        {
            get
            {
                yield return new object[]
                {
                    // All wildcards.
                    GetSomeFakeHttpMessageOptions(
                        new HttpMessageOptions
                        {
                            HttpResponseMessage = SomeFakeResponse
                        })
                };

                yield return new object[]
                {
                    // Any Uri but has to be a GET.
                    GetSomeFakeHttpMessageOptions(
                        new HttpMessageOptions
                        {
                            HttpMethod = HttpMethod.Get,
                            HttpResponseMessage = SomeFakeResponse
                        })
                };

                yield return new object[]
                {
                    // Has to match GET + URI
                    GetSomeFakeHttpMessageOptions(
                        new HttpMessageOptions
                        {
                            HttpMethod = HttpMethod.Get,
                            RequestUri = RequestUri,
                            HttpResponseMessage = SomeFakeResponse
                        })
                };

                yield return new object[]
                {
                    // Has to match GET + URI (case sensitive)
                    GetSomeFakeHttpMessageOptions(
                        new HttpMessageOptions
                        {
                            HttpMethod = HttpMethod.Get,
                            RequestUri = new Uri(RequestUri.AbsoluteUri.ToUpper()),
                            HttpResponseMessage = SomeFakeResponse
                        })
                };
            }
        }

        public static IEnumerable<object[]> DifferentHttpMessageOptions
        {
            get
            {
                yield return new object[]
                {
                    // Different uri.
                    new HttpMessageOptions
                    {
                        RequestUri = new Uri("http://this.is.a.different.website")
                    }
                };

                yield return new object[]
                {
                    // Different Method.
                    new HttpMessageOptions
                    {
                        HttpMethod = HttpMethod.Head
                     }
                };

                yield return new object[]
                {
                    // Different header (different key).
                    new HttpMessageOptions
                    {
                        Headers = new Dictionary<string, IEnumerable<string>>
                        {
                            {
                                "xxxx", new[]
                                {
                                    "pewpew"
                                }
                            }
                        }
                    }
                };

                yield return new object[]
                {
                    // Different header (found key, different content).
                    new HttpMessageOptions
                    {
                        Headers = new Dictionary<string, IEnumerable<string>>
                        {
                            {
                                "Bearer", new[]
                                {
                                    "pewpew"
                                }
                            }
                        }
                    }
                };
            }
        }
    }
}