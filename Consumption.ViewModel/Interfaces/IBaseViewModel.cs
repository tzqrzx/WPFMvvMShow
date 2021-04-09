

namespace Consumption.ViewModel.Interfaces
{
    using Consumption.Shared.DataInterfaces;
    using Consumption.Shared.Dto;
    using Microsoft.Toolkit.Mvvm.Input;
    using System.Threading.Tasks;

    #region 模块接口

    /// <summary>
    /// 实现基础的增删改查、分页、权限接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseViewModel<TEntity> : IOrdinary<TEntity>, IDataPager, IAuthority where TEntity : class
    { }

    public interface IUserViewModel : IBaseViewModel<UserDto>
    { }

    //public interface IUserControlEscolhaViewModel : IBaseViewModel<UserDto>
    //{ }
    public interface IGroupViewModel : IBaseViewModel<GroupDto>
    { }

    public interface IMenuViewModel : IBaseViewModel<MenuDto>
    { }

    public interface IBasicViewModel : IBaseViewModel<BasicDto>
    { }

    #endregion

    #region 组件接口

    public interface IComponentViewModel { }
    public interface ISkinViewModel : IComponentViewModel { }
    public interface IDashboardViewModel : IComponentViewModel { }
    public interface IHomeViewModel : IComponentViewModel { }
    public interface IUserControlEscolhaViewModel : IComponentViewModel { Task InitBind(); }
    public interface IUserControlInicioViewModel : IComponentViewModel { Task InitBind(); }

    public interface ILoginViewModel : IBaseDialog { }
    public interface IMainViewModel : IBaseDialog
    {
        Task InitDefaultView();
    }

    #endregion

    #region ICenter

    public interface ILoginCenter
    {
        Task<bool> ShowDialog();
    }
    public interface IMainCenter
    {
        Task<bool> ShowDialog();
    }
    public interface IUserCenter : IBaseCenter { }
    public interface IMenuCenter : IBaseCenter { }
    public interface IGroupCenter : IBaseCenter { }
    public interface IBasicCenter : IBaseCenter { }
    public interface IHomeCenter : IBaseCenter { }
    public interface IUserControlEscolhaCenter : IBaseCenter { Task InitBindShow(); }
    public interface IUserControlInicioCenter : IBaseCenter { Task InitBindShow(); }
    public interface IDashboardCenter : IBaseCenter { }
    public interface ISkinCenter : IBaseCenter { }
    public interface IMsgCenter
    {
        Task<bool> Show(object obj);
    }

    #endregion

    /// <summary>
    /// 弹窗窗口基础接口
    /// </summary>
    public interface IBaseDialog
    {
        bool DialogIsOpen { get; set; }

        void SnackBar(string msg);

        RelayCommand ExitCommand { get; }
    }
}
