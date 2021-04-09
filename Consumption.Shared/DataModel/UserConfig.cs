﻿/*
*
* 文件名    ：UserConfig                             
* 程序说明  : 用户个性化配置
* 更新时间  : 2020-05-16 15:05
* 作用      : 用于配置单个用户得每月预计支出 和 收入 
* 联系作者  : QQ:779149549 
* 开发者群  : QQ群:874752819
* 邮件联系  : zhouhaogg789@outlook.com
* 视频教程  : https://space.bilibili.com/32497462
* 博客地址  : https://www.cnblogs.com/zh7791/
* 项目地址  : https://github.com/HenJigg/WPF-Xamarin-Blazor-Examples
* 项目说明  : 以上所有代码均属开源免费使用,禁止个人行为出售本项目源代码
*/

namespace Consumption.Shared.DataModel
{
    /// <summary>
    /// 用户个性化配置
    /// </summary>
    public class UserConfig : BaseEntity
    {
        /// <summary>
        /// 账户名
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 预计支出
        /// </summary>
        public decimal ExpectedOut { get; set; }

        /// <summary>
        /// 预计收入
        /// </summary>
        public decimal ExpectedIn { get; set; }
    }
}
