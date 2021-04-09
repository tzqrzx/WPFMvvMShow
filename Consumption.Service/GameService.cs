

namespace Consumption.Service
{
    using Consumption.Core.Request;
    using Consumption.Shared.Dto;
    using Consumption.Shared.HttpContact;
    using Consumption.ViewModel.Interfaces;
    using RestSharp;
    using System.Threading.Tasks;

    public class GameService : BaseService<GameInfoDto>, IGameInfoRepository
    {
        public async Task<BaseResponse> GameSearchAsync(GameSearchDto ruser, string obj, HeaderDto header)
        {
            return await new BaseServiceRequest().GetPostHeader<BaseResponse>(new GameAllSearchRequest()
            {
                Parameter = ruser

            }, Method.POST, obj, header);
        }
        public async Task<BaseResponse> GameAddUserAsync(int gameId, HeaderDto header)
        {

            return await new BaseServiceRequest().GetPostHeader<BaseResponse>(new GameAddUserRequest()
            {
                Parameter = gameId,
                method = Method.POST.ToString()

            }, Method.POST, "", header);
        }

        public async Task<BaseResponse> GameSearchUserAsync(string platForm, HeaderDto header)
        {

            return await new BaseServiceRequest().GetPostHeader<BaseResponse>(new GameSearchUserRequest()
            {
                Parameter = platForm

            }, Method.GET, "", header);
        }

        public async Task<BaseResponse> GameAllowAsync(int gameId, HeaderDto header)
        {

            return await new BaseServiceRequest().GetPostHeader<BaseResponse>(new GameAllowRequest()
            {
                Parameter = gameId

            }, Method.GET, "", header);
        }

        public async Task<BaseResponse> GameDeleteUserAsync(int gameId, HeaderDto header)
        {

            return await new BaseServiceRequest().GetPostHeader<BaseResponse>(new GameDeleteUserRequest()
            {
                Parameter = gameId

            }, Method.DELETE, "", header);
        }

    }
}
