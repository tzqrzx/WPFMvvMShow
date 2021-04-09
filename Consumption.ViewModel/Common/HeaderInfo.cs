using Consumption.Shared.Common;
using Consumption.Shared.Dto;
using Newtonsoft.Json;
using System;

namespace Consumption.ViewModel.Common
{
    public class HeaderInfo
    {

        public static HeaderDto SetHeaderInfo(object obj, string xidid, string url)
        {
            var xsid = "";
            var xuid = "";
            if (!string.IsNullOrEmpty(Contract.sid))
            {
                xsid = Contract.sid;
            }
            var xts = Md5.GetTimeStamp();
            var xdid = xidid;
            var XTraceId = Guid.NewGuid().ToString();
            string body = JsonConvert.SerializeObject(obj).Replace("\\", "\\\\");
            var body1 = JsonConvert.DeserializeObject(body);
            string md5str = url + xsid + body1 + xts + XTraceId + "$Ka&L&*qj)d1ab";
            var xsign = Md5.toMd5(md5str);
            //发送请求头文件
            return new HeaderDto
            {
                XClientInfo = "windows",
                XDid = xdid,
                XSid = xsid,
                XSign = xsign,
                XTraceId = XTraceId,
                XTs = xts,
                XUa = "windows",
                XUid = xuid
            };
        }

    }
}
