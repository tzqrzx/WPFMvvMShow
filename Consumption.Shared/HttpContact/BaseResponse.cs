
using System;

namespace Consumption.Shared.HttpContact
{
    public class BaseResponse
    {

        //返回码
        public int code { get; set; }

        //返回错误信息
        public string message { get; set; }

        public object result { get; set; }

        public Boolean success { get; set; }



        /// <summary>
        /// //返回状态
        /// </summary>
        public int StatusCode { get; set; }

        public object Result { get; set; }
    }

    public class BaseResponse<T>
    {
        public int code { get; set; }
        /// <summary>
        /// 后台消息
        /// </summary>
        public string message { get; set; }


        public T Result { get; set; }
    }
}
