

namespace Consumption.ViewModel.Interfaces
{
    using Consumption.Shared.Common.Collections;
    using Consumption.Shared.Common.Query;
    using Consumption.Shared.DataModel;
    using Consumption.Shared.Dto;
    using Consumption.Shared.HttpContact;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IConsumptionRepository<T>
    {
        Task<BaseResponse<PagedList<T>>> GetAllListAsync(QueryParameters parameters);

        Task<BaseResponse<T>> GetAsync(int id);

        Task<BaseResponse> SaveAsync(T model);

        Task<BaseResponse> AddAsync(T model);

        Task<BaseResponse> DeleteAsync(int id);

        Task<BaseResponse> UpdateAsync(T model);
    }

    public interface IUserRepository : IConsumptionRepository<UserDto>
    {
        Task<BaseResponse> SendSMSAsync(phoneDto phone, string obj, HeaderDto header);

        Task<BaseResponse> LoginAsync(RequestUserDto phone, string obj, HeaderDto header);

        BaseResponse SendSMSHeaderAsync(phoneDto phone, phoneDto obj, HeaderDto header);
        /// <summary>
        /// 获取用户的所属权限信息
        /// </summary>
        Task<BaseResponse> GetUserPermByAccountAsync(string account);

        Task<BaseResponse<List<AuthItem>>> GetAuthListAsync();
    }

    public interface IGameInfoRepository : IConsumptionRepository<GameInfoDto>
    {
        //Task<BaseResponse<List<GameInfoDto>>> GameSearchAsync(GameSearchDto game, string obj, HeaderDto header);
        Task<BaseResponse> GameSearchAsync(GameSearchDto game, string obj, HeaderDto header);
        Task<BaseResponse> GameAddUserAsync(int gameId, HeaderDto header);
        Task<BaseResponse> GameAllowAsync(int gameId, HeaderDto header);
        Task<BaseResponse> GameSearchUserAsync(string platForm, HeaderDto header);
        Task<BaseResponse> GameDeleteUserAsync(int gameId, HeaderDto header);
    }

    public interface IUserControlEscolhaRepository : IConsumptionRepository<UserDto>
    {
        //Task<BaseResponse> SendSMSAsync(phoneDto phone, string obj, HeaderDto header);

        //Task<BaseResponse<UserInfoDto>> LoginAsync(string account, string passWord);

        //BaseResponse SendSMSHeaderAsync(phoneDto phone, phoneDto obj, HeaderDto header);
        ///// <summary>
        ///// 获取用户的所属权限信息
        ///// </summary>
        //Task<BaseResponse> GetUserPermByAccountAsync(string account);

        //Task<BaseResponse<List<AuthItem>>> GetAuthListAsync();
    }

    public interface IGroupRepository : IConsumptionRepository<GroupDto>
    {
        /// <summary>
        /// 获取菜单模块列表(包含每个菜单拥有的一些功能)
        /// </summary>
        /// <returns></returns>
        Task<BaseResponse<List<MenuModuleGroupDto>>> GetMenuModuleListAsync();

        /// <summary>
        /// 根据ID获取用户组信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BaseResponse<GroupDataDto>> GetGroupAsync(int id);

        /// <summary>
        /// 保存组数据
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        Task<BaseResponse> SaveGroupAsync(GroupDataDto group);
    }

    public interface IMenuRepository : IConsumptionRepository<MenuDto>
    {

    }

    public interface IBasicRepository : IConsumptionRepository<BasicDto>
    {

    }

}
