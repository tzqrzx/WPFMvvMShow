using System;

namespace Consumption.Shared.Dto
{
    public class ResponseResultDto
    {
        //返回码
        public int code { get; set; }

        //返回错误信息
        public string message { get; set; }

        public object result { get; set; }

        public Boolean success { get; set; }

    }


}
