using Consumption.Shared.Dto;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Consumption.Service
{


    /// <summary>
    /// RestSharp Client
    /// </summary>
    public class RestSharpCertificateMethodHeader
    {


        public static async Task<ResponseResultDto> PostAPI<ResponseResultDto>(string url, Method method, string obj, HeaderDto heardto,
            bool isToken = true, bool isJson = true) where ResponseResultDto : class
        {

            string path = @"Assets\server.crt";
            string password = "53f29eb6468111eb";
            path = AppDomain.CurrentDomain.BaseDirectory + path;
            var handler = new HttpClientHandler();
            handler.AllowAutoRedirect = false;

            handler.AutomaticDecompression = DecompressionMethods.None;
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.SslProtocols = SslProtocols.Tls12;
            //获取证书路径
            //商户私钥证书，用于对请求报文进行签名
            handler.ClientCertificates.Add(new X509Certificate2(path, password, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet));
            handler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;


            using (var client = new HttpClient(handler))
            {

                if (isToken)
                {
                    byte[] byty11 = Encoding.ASCII.GetBytes(heardto.XClientInfo);
                    var a11 = System.Text.Encoding.ASCII.GetString(byty11);
                    byte[] byty22 = Encoding.ASCII.GetBytes(heardto.XSign);
                    var a22 = System.Text.Encoding.ASCII.GetString(byty22);
                    //  client.DefaultRequestHeaders.Add("Authorization", heardto.XSign);
                    client.DefaultRequestHeaders.Add("x-client-info", a11);
                    if (!String.IsNullOrWhiteSpace(heardto.XDid))
                    {
                        byte[] byty3 = Encoding.ASCII.GetBytes(heardto.XDid);
                        var a3 = System.Text.Encoding.ASCII.GetString(byty3);
                        client.DefaultRequestHeaders.Add("x-did", heardto.XDid);
                    }

                    client.DefaultRequestHeaders.Add("x-sign", a22);
                    client.DefaultRequestHeaders.Add("x-trace-id", heardto.XTraceId);
                    client.DefaultRequestHeaders.Add("x-ts", heardto.XTs);
                    client.DefaultRequestHeaders.Add("x-ua", heardto.XUa);
                    client.DefaultRequestHeaders.Add("x-uid", heardto.XUid);
                    client.DefaultRequestHeaders.Add("x-sid", heardto.XSid);

                }

                client.BaseAddress = new Uri(url);

                if (!string.IsNullOrEmpty(obj))
                {
                    var requestJson = new JsonSerializer().Deserialize(new JsonTextReader(new StringReader(JsonConvert.SerializeObject(JsonConvert.DeserializeObject(obj)))));
                    HttpContent content = new StringContent(requestJson.ToString());
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage message = null;
                    if (Method.POST == method)
                        message = await client.PostAsync(url, content);
                    if (Method.GET == method)
                        message = await client.GetAsync(url);
                    if (Method.DELETE == method)
                        message = await client.DeleteAsync(url);
                    if (message.StatusCode.ToString() == "OK")
                    {
                        var tempstr = await message.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseResultDto>(tempstr);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    HttpContent content = new StringContent("");
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage message = null;
                    if (Method.POST == method)
                        message = await client.PostAsync(url, content);
                    if (Method.GET == method)
                        message = await client.GetAsync(url);
                    if (Method.DELETE == method)
                        message = await client.DeleteAsync(url);
                    if (message.StatusCode.ToString() == "OK")
                    {
                        var tempstr = await message.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseResultDto>(tempstr);
                    }
                    else
                    {
                        return null;
                    }

                }


            }

        }





        public static string PostAPITwo<HttpResponseMessage>(string url, Method method, phoneDto obj, HeaderDto heardto,
            bool isToken = true, bool isJson = true)
        {
            string json = null;
            System.Net.ServicePointManager.Expect100Continue = false;
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "post";
            byte[] byty1 = Encoding.ASCII.GetBytes(heardto.XClientInfo);
            var a1 = System.Text.Encoding.ASCII.GetString(byty1);
            request.Headers.Add("x-client-info", a1);
            byte[] byty2 = Encoding.ASCII.GetBytes(heardto.XSign);
            var a2 = System.Text.Encoding.ASCII.GetString(byty2);
            request.Headers.Add("x-sign", a2);
            request.Headers.Add("x-trace-id", heardto.XTraceId);
            request.Headers.Add("x-ts", heardto.XTs);
            request.Headers.Add("x-ua", heardto.XUa);
            request.ContentType = "application/json";
            //request.Accept = "text/html, application/xhtml+xml, */*";
            var requestJson = new JsonSerializer().Deserialize(new JsonTextReader(new StringReader(JsonConvert.SerializeObject(obj))));
            byte[] buffer = encoding.GetBytes(requestJson.ToString());
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                json = reader.ReadToEnd();
            }
            return json;

        }


    }
}
