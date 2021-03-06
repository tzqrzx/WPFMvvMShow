/*
*
* 文件名    ：GroupUser                             
* 程序说明  : 用户组所对应用户实体
* 更新时间  : 2020-05-16 15:03
* 联系作者  : QQ:779149549 
* 开发者群  : QQ群:874752819
* 邮件联系  : zhouhaogg789@outlook.com
* 视频教程  : https://space.bilibili.com/32497462
* 博客地址  : https://www.cnblogs.com/zh7791/
* 项目地址  : https://github.com/HenJigg/WPF-Xamarin-Blazor-Examples
* 项目说明  : 以上所有代码均属开源免费使用,禁止个人行为出售本项目源代码
*/

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Consumption.Shared.DataModel
{
    /// <summary>
    /// 组用户
    /// </summary>
    public partial class GroupUser : BaseEntity
    {
        /// <summary>
        /// 组代码
        /// </summary>
        public string GroupCode { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

    }

}
