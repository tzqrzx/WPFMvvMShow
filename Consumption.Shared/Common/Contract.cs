

namespace Consumption.Shared.Common
{
    using Consumption.Shared.DataModel;
    using System.Collections.Generic;

    public static class Contract
    {
        #region 用户信息

        /// <summary>
        /// 登录名
        /// </summary>
        public static string nickName = string.Empty;


        public static string sid = string.Empty;

        public static string vipLevel;
        public static string headImg = string.Empty;
        public static string expireTime = string.Empty;
        public static string createTime = string.Empty;
        public static bool IsLogin;
        public static bool IsAdmin;
        public static string refer;
        #endregion

        #region 权限验证信息

        /// <summary>
        /// 系统中已定义的功能清单-缓存用于页面验证
        /// </summary>
        public static List<AuthItem> AuthItems;

        /// <summary>
        /// 获取用户的所有模块
        /// </summary>
        public static List<Menu> Menus;

        #endregion

        #region 接口地址

        public static string serverUrl = string.Empty;

        #endregion
    }
}
