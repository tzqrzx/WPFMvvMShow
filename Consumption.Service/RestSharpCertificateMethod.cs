
namespace Consumption.Service
{
    using Consumption.Shared.Common.Aop;
    using Consumption.Shared.Dto;
    using Consumption.Shared.HttpContact;
    using Newtonsoft.Json;
    using RestSharp;
    using System.Text;
    using System.Threading.Tasks;
    /// <summary>
    /// RestSharp Client
    /// </summary>
    public class RestSharpCertificateMethod
    {
        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="method">请求类型</param>
        /// <param name="pms">参数</param>
        /// <param name="isToken">是否Token</param>
        /// <param name="isJson">是否Json</param>
        /// <returns></returns>
        [GlobalLoger]
        public async Task<Response> RequestBehavior<Response>(string url, Method method, string pms,
            bool isToken = true, bool isJson = true) where Response : class
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(method);
            if (isToken)
            {
                client.AddDefaultHeader("token", "");
            }
            switch (method)
            {
                case Method.GET:
                    request.AddHeader("Content-Type", "application/json");
                    break;
                case Method.POST:
                    if (isJson)
                    {
                        request.AddHeader("Content-Type", "application/json");

                        request.AddJsonBody(pms);
                    }
                    else
                    {
                        request.AddHeader("Content-Type", "application/json");
                        request.AddParameter("application/x-www-form-urlencoded",
                            pms, ParameterType.RequestBody);
                    }
                    break;
                case Method.PUT:
                    request.AddHeader("Content-Type", "application/json");
                    break;
                case Method.DELETE:
                    request.AddHeader("Content-Type", "application/json");
                    break;
                default:
                    request.AddHeader("Content-Type", "application/json");
                    break;
            }
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<Response>(response.Content);
            else
                return new BaseResponse()
                {
                    code = (int)response.StatusCode,
                    message = response.StatusDescription ?? response.ErrorMessage
                } as Response;
        }


        /// <summary>
        /// 请求数据
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="method">请求类型</param>
        /// <param name="pms">参数</param>
        /// <param name="isToken">是否Token</param>
        /// <param name="isJson">是否Json</param>
        /// <returns></returns>
        [GlobalLoger]
        public async Task<Response> RequestBehaviorHeader<Response>(string url, Method method, phoneDto pms, HeaderDto heardto,
            bool isToken = false, bool isJson = true) where Response : class
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(method);


            if (isToken)
            {
                client.AddDefaultHeader("token", "");
            }
            if (heardto != null)
            {
                //  client.AddDefaultHeader("token", "");
                //var headers = new List<KeyValuePair<string, string>>
                //{
                //    new KeyValuePair<string, string>("x-client-info", heardto.XClientInfo),
                //    new KeyValuePair<string, string>("x-sign", heardto.XSign),
                //    new KeyValuePair<string, string>("x-trace-id", heardto.XTraceId),
                //    new KeyValuePair<string, string>("x-ts", heardto.XTs),
                //    new KeyValuePair<string, string>("x-ua", heardto.XUa)
                //};


                //var headers = new Dictionary<string, string>
                //{
                //    {"Content-Type", "application/json"},
                //    {"x-client-info", heardto.XClientInfo},
                //    {"x-sign", heardto.XSign},
                //    {"x-trace-id", heardto.XTraceId},
                //    {"x-ts", heardto.XTs},
                //    {"x-ua", heardto.XUa}
                //};

                //client.AddDefaultHeaders(headers);


            }

            switch (method)
            {
                case Method.GET:
                    request.AddHeader("Content-Type", "application/json");
                    break;
                case Method.POST:
                    if (isJson)
                    {
                        byte[] byty1 = Encoding.ASCII.GetBytes(heardto.XClientInfo);
                        var a1 = System.Text.Encoding.ASCII.GetString(byty1);

                        byte[] byty2 = Encoding.ASCII.GetBytes(heardto.XSign);
                        var a2 = System.Text.Encoding.ASCII.GetString(byty2);

                        // request.Parameters.Clear();
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("x-client-info", a1);
                        request.AddHeader("x-sign", a2);
                        request.AddHeader("x-trace-id", heardto.XTraceId);
                        request.AddHeader("x-ts", heardto.XTs);
                        request.AddHeader("x-ua", heardto.XUa);
                        var a = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(pms));
                        //var bs = new JsonSerializer().Deserialize(new JsonTextReader(new StringReader(JsonConvert.SerializeObject(pms))));
                        //request.Parameters.Clear();
                        request.AddJsonBody(a);
                    }
                    else
                    {
                        request.AddHeader("Content-Type", "application/json");
                        request.AddParameter("application/x-www-form-urlencoded",
                            pms, ParameterType.RequestBody);
                    }
                    break;
                case Method.PUT:
                    request.AddHeader("Content-Type", "application/json");
                    break;
                case Method.DELETE:
                    request.AddHeader("Content-Type", "application/json");
                    break;
                default:
                    request.AddHeader("Content-Type", "application/json");
                    break;
            }

            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<Response>(response.Content);
            else
                return new BaseResponse()
                {
                    code = (int)response.StatusCode,
                    message = response.StatusDescription ?? response.ErrorMessage
                } as Response;
        }
    }
}
