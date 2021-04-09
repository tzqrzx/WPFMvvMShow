

namespace Consumption.Service
{
    using Consumption.Core.Request;
    using Consumption.Shared.DataModel;
    using Consumption.Shared.Dto;
    using Consumption.Shared.HttpContact;
    using Consumption.ViewModel.Interfaces;
    using RestSharp;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserService : BaseService<UserDto>, IUserRepository
    {
        public async Task<BaseResponse<List<AuthItem>>> GetAuthListAsync()
        {
            return await new BaseServiceRequest().GetRequest<BaseResponse<List<AuthItem>>>(new AuthItemRequest(), Method.GET);
        }

        public async Task<BaseResponse> GetUserPermByAccountAsync(string account)
        {
            return await new BaseServiceRequest().GetRequest<BaseResponse>(new UserPermRequest()
            {
                account = account
            }, Method.GET);
        }


        public async Task<BaseResponse> LoginAsync(RequestUserDto ruser, string obj, HeaderDto header)
        {
            return await new BaseServiceRequest().GetPostHeader<BaseResponse>(new UserLoginRequest()
            {
                Parameter = ruser

            }, Method.POST, obj, header);
        }



        public async Task<BaseResponse> SendSMSAsync(phoneDto phone, string obj, HeaderDto header)
        {
            return await new BaseServiceRequest().GetPostHeader<BaseResponse>(new UserSMSRegionRequest()
            {
                Parameter = phone
            }, Method.POST, obj, header);
        }

        public BaseResponse SendSMSHeaderAsync(phoneDto phone, phoneDto obj, HeaderDto header)
        {
            return new BaseServiceRequest().GetPostHeaderTwo<BaseResponse>(new UserSMSRegionRequest()
            {
                Parameter = phone
            }, Method.POST, obj, header);
        }
    }
}
