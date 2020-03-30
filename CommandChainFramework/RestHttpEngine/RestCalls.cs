using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace CommandChainFramework.RestHttp
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
    public class RestCalls
    {
        private readonly RestClient _restClient;
        
        public RestCalls(Uri uri)
        {
            _restClient = new RestClient(uri);
        }

        public RestCalls(string reggieDomain)
        {
            _restClient=new RestClient(new Uri(reggieDomain,UriKind.RelativeOrAbsolute));
        }

        public IRestResponse httpGet(string getUrl)
        {
            var request = new RestRequest(getUrl, Method.GET);
            var response = _restClient.Execute<HttpResponseMessage>(request);
            return response;
        }

        public void httpPost(string contentType,string bodyData)
        {
            const Method httpMethod = Method.POST;

            var request = new RestRequest(RequestBodyCapturer.RESOURCE, httpMethod);

            request.AddParameter(contentType, bodyData, ParameterType.RequestBody);

            var resetEvent = new ManualResetEvent(false);

           _restClient.ExecuteAsync(request, response => resetEvent.Set());
            resetEvent.WaitOne();
        }
    }
   
}
