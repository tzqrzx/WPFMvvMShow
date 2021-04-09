
namespace Consumption.ViewModel
{
    using Consumption.Core.Request;
    using Consumption.Shared.Common;
    using Consumption.Shared.Common.Enums;
    using Consumption.Shared.Common.Security;
    using Consumption.Shared.Dto;
    using Consumption.ViewModel.Common;
    using Consumption.ViewModel.Common.Aop;
    using Consumption.ViewModel.Interfaces;
    using Microsoft.Toolkit.Mvvm.Input;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;


    public class UserControlInicioViewModel : BaseViewModel, IUserControlInicioViewModel
    {
        //private string serverpath = AppDomain.CurrentDomain.BaseDirectory;
        public UserControlInicioViewModel(IGameInfoRepository repository)
        {
            this.repository = repository;
            QueryCommand = new AsyncRelayCommand(GetGameSearch);
            OpenSpeedUpCommand = new AsyncRelayCommand<int>(UserSpeedUpShow);
            OpenDialogCommand = new AsyncRelayCommand(OpenDialogShow);
            IsHot = true;
            // vMSelectedTabIndex = 0;

        }

        #region Property
        private readonly IGameInfoRepository repository;
        private string userName;
        private string phone;
        private string code;
        private string btnContent;
        private string search;
        private int vMSelectedTabIndex;
        private bool isOpen;
        private bool isHot;
        private bool isGov;
        private ObservableCollection<ModuleGameInfo> modules;

        public ObservableCollection<ModuleGameInfo> Modules
        {
            get { return modules; }
            set { modules = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ModuleGameInfo> modulesGov;

        public ObservableCollection<ModuleGameInfo> ModulesGov
        {
            get { return modulesGov; }
            set { modulesGov = value; OnPropertyChanged(); }
        }

        public bool IsHot
        {
            get { return isHot; }
            set { isHot = value; OnPropertyChanged(); }
        }

        public bool IsGov
        {
            get { return isGov; }
            set { isGov = value; OnPropertyChanged(); }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; OnPropertyChanged(); }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(); }
        }

        public string Code
        {
            get { return code; }
            set { code = value; OnPropertyChanged(); }
        }

        public string BtnContent
        {
            get { return btnContent; }
            set { btnContent = value; OnPropertyChanged(); }
        }

        public string Search
        {
            get { return search; }
            set { search = value; OnPropertyChanged(); }
        }

        public int VMSelectedTabIndex
        {
            get { return vMSelectedTabIndex; }
            set { vMSelectedTabIndex = value; OnPropertyChanged(); }
        }

        public bool IsOpen
        {
            get { return isOpen; }
            set { isOpen = value; OnPropertyChanged(); }
        }
        #endregion

        #region Command

        public AsyncRelayCommand QueryCommand { get; private set; }
        public AsyncRelayCommand<int> OpenSpeedUpCommand { get; private set; }
        public AsyncRelayCommand OpenDialogCommand { get; private set; }
        public RelayCommand SMSCommand { get; private set; }
        #endregion

        public async Task GetGameSearch()
        {
            var categorys = "";
            if (!isGov)
                categorys = Md5.CharToUTF8("游戏平台");
            else
                categorys = Md5.CharToUTF8("外服游戏");

            string xdid = IPMack.GetNetworkAdpaterID();
            if (Search == null)
            {
                Search = "";
            }
            var gameSearchDto = new GameSearchDto()
            {
                category = categorys,
                keyword = Search,
                offset = 0,
                pagesize = 50,
                platform = "windows"
            };
            GameAllSearchRequest urls = new GameAllSearchRequest();
            var url = "/" + urls.route;
            HeaderDto header = HeaderInfo.SetHeaderInfo(gameSearchDto, xdid, url);
            var Result = await repository.GameSearchAsync(gameSearchDto, JsonConvert.SerializeObject(gameSearchDto), header);
            if (Result == null)
            {
                SnackBar("服务接口连接错误！");
                return;
            }
            if (Result.code != (int)ActionReturnCode.ok)
            {
                SnackBar(Result.message);
                return;
            }
            var games = JsonConvert.DeserializeObject<List<ModuleGameInfo>>(JsonConvert.SerializeObject(Result.result));
            Modules = new ObservableCollection<ModuleGameInfo>(games);
        }

        public async Task UserSpeedUpShow(int ids)
        {
            string xdid = IPMack.GetNetworkAdpaterID();
            GameAddUserRequest urls = new GameAddUserRequest();
            var url = "/" + urls.route + "/" + ids;
            HeaderDto header = HeaderInfo.SetHeaderInfo("", xdid, url);
            var Result = await repository.GameAddUserAsync(ids, header);
            if (Result == null)
            {
                SnackBar("服务接口连接错误！");
                return;
            }
            if (Result.code != (int)ActionReturnCode.ok)
            {
                SnackBar(Result.message);
                return;
            }
            WeakReferenceMessenger.Default.Send(string.Empty, "OpenUserGame");


        }
        [GlobalProgress]
        private async Task OpenDialogShow()
        {
            await Task.Delay(5000);

        }
        public async Task InitBind()
        {
            await this.GetGameSearch();
        }

    }

}
