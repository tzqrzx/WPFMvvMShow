

namespace Consumption.ViewModel
{
    using Consumption.Shared.Dto;
    using Consumption.ViewModel.Interfaces;

    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserViewModel : BaseRepository<UserDto>, IUserViewModel
    {
        public UserViewModel(IUserRepository repository) : base(repository)
        {

        }
    }
}
