using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace CommandChainFramework.RestHttpEngine
{
    class RequestBodyCapturer
    {
        public const string RESOURCE = "Capture";

        public static string CapturedContentType { get; set; }

        public static bool CapturedHasEntityBody { get; set; }

        public static string CapturedEntityBody { get; set; }

        public static void Capture(HttpListenerContext context)
        {
            var request = context.Request;

            CapturedContentType = request.ContentType;
            CapturedHasEntityBody = request.HasEntityBody;
            CapturedEntityBody = StreamToString(request.InputStream);
        }

        static string StreamToString(Stream stream)
        {
            var streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        }
    }
    public class RestCalls: BaseHttp
    {
        private RestClient _restClient;
        
        public RestCalls getHttpInstance(Uri uri)
        {
            this._restClient = new RestClient(uri);
            return this;
        }

        public RestCalls getHttpInstance(string reggieDomain)
        {
            this._restClient=new RestClient(new Uri(reggieDomain,UriKind.RelativeOrAbsolute));
            return this;
        }

        public RestCalls HttpGetAndAssertCalls(string getUrl,out string statuscode)
        {
            var request = new RestRequest(getUrl, Method.GET);
            var response= this._restClient.Execute<HttpResponseMessage>(request);
            statuscode = response.StatusCode.ToString();
            return this;
        }

        public RestCalls AssertHttpGet(IRestResponse response)
        {
            if (!response.IsSuccessful)
            {
                new Exception(response.StatusCode + " Status Description" + response.StatusDescription);
            }
            return this;
        }

        public void httpPostAndAssertCalls(string contentType,string bodyData)
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
