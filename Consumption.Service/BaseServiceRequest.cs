
namespace Consumption.Service
{
    using Consumption.Shared.Common;
    using Consumption.Shared.Dto;
    using Consumption.Shared.HttpContact;
    using Newtonsoft.Json;
    using RestSharp;
    using System.Threading.Tasks;

    /// <summary>
    /// 请求服务基类
    /// </summary>
    public class BaseServiceRequest
    {
        private readonly string _requestUrl = Contract.serverUrl;

        public string requestUrl
        {
            get { return _requestUrl; }
        }



        /// <summary>
        /// restSharp实例
        /// </summary>
        public RestSharpCertificateMethod restSharp = new RestSharpCertificateMethod();

        /// <summary>
        /// T请求
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <param name="method">方法类型</param>
        /// <returns></returns>
        public async Task<Response> GetRequestHeader<Response>(BaseRequest request, Method method, phoneDto obj, HeaderDto header) where Response : class
        {
            string pms = request.GetPropertiesObject();
            string url = requestUrl + request.route;
            if (!string.IsNullOrWhiteSpace(request.getParameter))
                url += request.getParameter;
            Response result = await restSharp.RequestBehaviorHeader<Response>(url, method, obj, header);
            return result;
        }

        public async Task<HttpRequestMessage> GetPostHeader<HttpRequestMessage>(BaseRequest request, Method method, string obj, HeaderDto header) where HttpRequestMessage : class
        {

            string pms = request.GetPropertiesObject();
            string url = requestUrl + request.route;

            if (Method.POST == method && !string.IsNullOrWhiteSpace(request.getParameter))
            {
                url = url + "/" + request.getParameter.ToString();
            }
            if (Method.GET == method && !string.IsNullOrWhiteSpace(request.getParameter))
            {
                // url += request.getParameter;
                url = url + "/" + request.getParameter.ToString();
            }
            if (Method.DELETE == method && !string.IsNullOrWhiteSpace(request.getParameter))
            {
                // url += request.getParameter;
                url = url + "/" + request.getParameter.ToString();
            }

            HttpRequestMessage result = await RestSharpCertificateMethodHeader.PostAPI<HttpRequestMessage>(url, method, obj, header);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="HttpRequestMessage"></typeparam>
        /// <param name="request"></param>
        /// <param name="method"></param>
        /// <param name="obj"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public async Task<HttpRequestMessage> GetPostHeaderD<HttpRequestMessage>(BaseRequest request, Method method, string obj, HeaderDto header) where HttpRequestMessage : class
        {
            string pms = request.GetPropertiesObject();
            string url = requestUrl + request.route;
            if (!string.IsNullOrWhiteSpace(request.getParameter))
                url += request.getParameter;
            HttpRequestMessage result = await RestSharpCertificateMethodHeader.PostAPI<HttpRequestMessage>(url, method, obj, header);
            return result;
        }



        public HttpRequestMessage GetPostHeaderTwo<HttpRequestMessage>(BaseRequest request, Method method, phoneDto obj, HeaderDto header) where HttpRequestMessage : class
        {
            string pms = request.GetPropertiesObject();
            string url = requestUrl + request.route;
            if (!string.IsNullOrWhiteSpace(request.getParameter))
                url += request.getParameter;
            var result = RestSharpCertificateMethodHeader.PostAPITwo<HttpRequestMessage>(url, method, obj, header);
            return (HttpRequestMessage)JsonConvert.DeserializeObject(result);
        }

        public async Task<Response> GetRequest<Response>(BaseRequest request, Method method) where Response : class
        {
            string pms = request.GetPropertiesObject();
            string url = requestUrl + request.route;
            if (!string.IsNullOrWhiteSpace(request.getParameter))
                url += request.getParameter;
            Response result = await restSharp.RequestBehavior<Response>(url, method, pms);
            return result;
        }

        /// <summary>
        /// 请求
        /// </summary>
        /// <typeparam name="Response"></typeparam>
        /// <param name="url">地址</param>
        /// <param name="pms">参数</param>
        /// <param name="method">方法类型</param>
        /// <returns></returns>
        public async Task<Response> GetRequest<Response>(string route, object obj, Method method) where Response : class
        {
            string pms = string.Empty;
            if (!string.IsNullOrWhiteSpace(obj?.ToString())) pms = JsonConvert.SerializeObject(obj);
            Response result = await restSharp.RequestBehavior<Response>(requestUrl + route, method, pms);
            return result;
        }


    }
}
