using RestSharp;
using System;
using System.Net.Http;
using System.Threading;

namespace CommandChainFramework.RestHttpEngine
{
    public class RestCalls : BaseHttp
    {
        private RestClient _restClient;

        public RestCalls GetHttpInstance(Uri uri)
        {
            this._restClient = new RestClient(uri);
            return this;
        }

        public RestCalls GetHttpInstance(string url)
        {
            this._restClient = new RestClient(new Uri(url, UriKind.RelativeOrAbsolute));
            return this;
        }

        public RestCalls HttpGetAndAssertCalls(string getUrl, out string statusCode)
        {
            var request = new RestRequest(getUrl, Method.GET);
            var response = this._restClient.Execute<HttpResponseMessage>(request);
            statusCode = response.StatusCode.ToString();
            return this;
        }

        //public RestCalls HttpGetAndAssertCalls(string getUrl, out IRestResponse restResponse)
        //{
        //    var request = new RestRequest(getUrl, Method.GET);
        //    var response = this._restClient.Execute<HttpResponseMessage>(request);
        //    statusCode = response.StatusCode.ToString();
        //    return this;
        //}

        public RestCalls AssertHttpGet(IRestResponse response)
        {
            if (!response.IsSuccessful)
            {
                new Exception(response.StatusCode + " Status Description" + response.StatusDescription);
            }
            return this;
        }

        public void httpPostAndAssertCalls(string contentType, string bodyData)
        {
            const Method httpMethod = Method.POST;

            var request = new RestRequest(RequestBodyCapturer.RESOURCE, httpMethod);

            request.AddParameter(contentType, bodyData, ParameterType.RequestBody);

            var resetEvent = new ManualResetEvent(false);

            this._restClient.ExecuteAsync(request, response => resetEvent.Set());
            resetEvent.WaitOne();
        }
    }

}
