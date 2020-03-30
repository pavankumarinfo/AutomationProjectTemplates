using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CommandChainFramework.HttpEngine;


// ReSharper disable ConsiderUsingConfigureAwait

namespace CommandChainFramework.HttpEngine
{
    public class PostAsyncTests
    {
        public static IEnumerable<object[]> ValidPostHttpContent
        {
            get
            {
                // Source content, Expected Content.
                // NOTE: we have to duplicate the source/expected below so we 
                //       test that they are **separate memory references** and not the same
                //       memory reference.
                yield return new object[]
                {
                    // Sample json.
                    new StringContent("{\"id\":1}", Encoding.UTF8),
                    new StringContent("{\"id\":1}", Encoding.UTF8)
                };

                yield return new object[]
                {
                    // Form key/values.
                    new FormUrlEncodedContent(new[]
                                              {
                                                  new KeyValuePair<string, string>("a", "b"),
                                                  new KeyValuePair<string, string>("c", "1")
                                              }),
                    new FormUrlEncodedContent(new[]
                                              {
                                                  new KeyValuePair<string, string>("a", "b"),
                                                  new KeyValuePair<string, string>("c", "1")
                                              })
                };
            }
        }

        public static IEnumerable<object[]> InvalidPostHttpContent
        {
            get
            {
                yield return new object[]
                {
                    // Sample json.
                    new StringContent("{\"id\":1}", Encoding.UTF8),
                    new StringContent("{\"id\":2}", Encoding.UTF8)
                };

                yield return new object[]
                {
                    // Sample json.
                    new StringContent("{\"id\":1}", Encoding.UTF8),
                    new StringContent("{\"ID\":1}", Encoding.UTF8) // Case has changed.
                };

                yield return new object[]
                {
                    // Form key/values.
                    new FormUrlEncodedContent(new[]
                                              {
                                                  new KeyValuePair<string, string>("a", "b"),
                                                  new KeyValuePair<string, string>("c", "1")
                                              }),
                    new FormUrlEncodedContent(new[]
                                              {
                                                  new KeyValuePair<string, string>("2", "1")
                                              })
                };
            }
        }
      
    }
}