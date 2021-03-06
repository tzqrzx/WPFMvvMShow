/*
*
* 文件名    ：AuthItem                             
* 程序说明  : 权限值定义实体
* 更新时间  : 2020-05-16 15:01
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
    /// 权限
    /// </summary>
    public class AuthItem : BaseEntity
    {
        /// <summary>
        /// 权限定义名称
        /// </summary>
        public string AuthName { get; set; }

        /// <summary>
        /// 设定预期图标
        /// </summary>
        public string AuthKind { get; set; }

        /// <summary>
        /// 设定预期颜色
        /// </summary>
        public string AuthColor { get; set; }

        /// <summary>
        /// 所属权限值
        /// </summary>
        public int AuthValue { get; set; }
    }


}
