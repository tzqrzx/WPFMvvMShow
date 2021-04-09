﻿/*
*
* 文件名    ：MenuService                             
* 程序说明  : 菜单服务
* 更新时间  : 2020-06-15 22：32
* 联系作者  : QQ:779149549 
* 开发者群  : QQ群:874752819
* 邮件联系  : zhouhaogg789@outlook.com
* 视频教程  : https://space.bilibili.com/32497462
* 博客地址  : https://www.cnblogs.com/zh7791/
* 项目地址  : https://github.com/HenJigg/WPF-Xamarin-Blazor-Examples
* 项目说明  : 以上所有代码均属开源免费使用,禁止个人行为出售本项目源代码
*/


namespace Consumption.Service
{
    using Consumption.Shared.DataModel;
    using Consumption.Shared.Dto;
    using Consumption.ViewModel.Interfaces;

    /// <summary>
    /// 菜单服务
    /// </summary>
    public partial class MenuService : BaseService<MenuDto>, IMenuRepository
    {

    }

}
