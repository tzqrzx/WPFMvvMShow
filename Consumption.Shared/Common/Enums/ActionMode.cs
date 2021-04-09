/*
*
* 文件名    ：ActionMode.cs                              
* 程序说明  : 窗口功能区编辑类型
* 更新时间  : 2020-05-11
* 联系作者  : QQ:779149549 
* 开发者群  : QQ群:874752819
* 邮件联系  : zhouhaogg789@outlook.com
* 视频教程  : https://space.bilibili.com/32497462
* 博客地址  : https://www.cnblogs.com/zh7791/
* 项目地址  : https://github.com/HenJigg/WPF-Xamarin-Blazor-Examples
* 项目说明  : 以上所有代码均属开源免费使用,禁止个人行为出售本项目源代码
*/

using System.ComponentModel;

namespace Consumption.Shared.Common.Enums
{
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum ActionMode
    {
        None,
        Add,
        Edit
    }

    public enum ActionReturnCode
    {
        /// <summary>
        /// 请求成功
        /// </summary>
        ok = 10200,

        /// <summary>
        /// 
        /// </summary>
        err = 10400,

        /// <summary>
        /// 签名错误
        /// </summary>
        errNum = 10405,

    }

    /// <summary>
    /// 验证类型
    /// </summary>
    public enum ValidationType
    {
        None,

        [Description(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")]
        Email,

        [Description(@"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$")]
        Phone,

        Str,
        //...
    }

    //页面验证
    public enum ModulePageEum
    {
        None,
        /// <summary>
        /// 加速页面
        /// </summary>
        [Description(@"UserControlEscolhaCenter")]
        Escolha,

        /// <summary>
        /// 全部游戏
        /// </summary>
        [Description(@"UserControlInicioCenter")]
        Inicio,

    }

    public enum ModuleVisibilityEum
    {
        Visible = 2,

        Collapsed = 3,


        Hidden = 1,

    }
}
