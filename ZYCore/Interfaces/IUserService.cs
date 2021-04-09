using System.Threading.Tasks;
using ZYModel.Entity;
using ZYModel.Query;
using ZYModel.ResponseModel;

namespace ZYCore.Interfaces
{
    public interface IUserService
    {
        Task<LoginUserResponse> LoginAsync(string account, string passWord);

        Task<LoginUserAuthResponse> LoginUserAuthAsync(string account);

        Task<bool> AddUserAsync(User model);

        Task<bool> DeleteUserAsync(int id);

        Task<GetUserResponse> GetUsersAsync(UserParameters parameter);

        Task<bool> UpdateUserAsync(User model);
    }
}
