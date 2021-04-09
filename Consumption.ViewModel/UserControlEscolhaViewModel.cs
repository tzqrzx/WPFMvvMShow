
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
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;


    /// <summary>
    /// 部门管理
    /// </summary>
    public class UserControlEscolhaViewModel : BaseViewModel, IUserControlEscolhaViewModel
    {


        public UserControlEscolhaViewModel(IGameInfoRepository repository)
        {
            if (!Contract.IsLogin)
                SnackBar("用户未登录，请重新登录");

            this.repository = repository;
            DelSpeedUpCommand = new AsyncRelayCommand<int>(DelUserGame);
            SpeedUpCommand = new AsyncRelayCommand<int>(SpeedUp);
            ExitSpeedUpCommand = new AsyncRelayCommand<int>(ExitSpeedUp);
            OpenDialogCommand = new AsyncRelayCommand(OpenDialogShow);
        }

        #region Property
        private readonly IGameInfoRepository repository;
        private string userName;
        private string phone;
        private string code;
        private string btnContent;
        private string visibStop;
        private string visib;
        private bool isOpen;
        private ObservableCollection<ModuleGameInfo> modules;


        public ObservableCollection<ModuleGameInfo> Modules
        {
            get { return modules; }
            set { modules = value; OnPropertyChanged(); }
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

        public string VisibStop
        {
            get { return visibStop; }
            set { visibStop = value; OnPropertyChanged(); }
        }

        public string Visib
        {
            get { return visib; }
            set { visib = value; OnPropertyChanged(); }
        }

        public bool IsOpen
        {
            get { return isOpen; }
            set { isOpen = value; OnPropertyChanged(); }
        }
        #endregion

        #region Command
        public AsyncRelayCommand<int> DelSpeedUpCommand { get; private set; }
        public AsyncRelayCommand<int> SpeedUpCommand { get; private set; }
        public AsyncRelayCommand<int> ExitSpeedUpCommand { get; private set; }
        public AsyncRelayCommand OpenDialogCommand { get; private set; }
        #endregion

        [GlobalProgress]
        private async Task SpeedUp(int id)
        {
            var mod = Modules.Where(p => p.Id == id).First();
            if (mod.VitibilityRun == 2 || mod.VitibilityRun == 0)
            {
                mod.VitibilityRun = 3;
                mod.VitibilityStop = 2;
                string plat = "windows";
                string xdid = IPMack.GetNetworkAdpaterID();
                GameAllowRequest urls = new GameAllowRequest();
                var url = "/" + urls.route + "/" + id.ToString();
                HeaderDto header = HeaderInfo.SetHeaderInfo("", xdid, url);
                var Result = await repository.GameAllowAsync(id, header);
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
                if (Result.code == (int)ActionReturnCode.ok)
                {
                    var ipstr = IPMack.GetLocalIp();
                    var xnip = "10.255.0.2";
                    var xnroute = "10.255.0.1";
                    var xnprxo = "2884";
                    CmdClass.ExecuteRunCode(ipstr, xnip, xnroute, xnprxo);
                    await Task.Delay(5000);
                    var returnCmd = CmdClass.ExecuteRoute(xnroute);
                    await Task.Delay(3000);
                }
            }




        }

        [GlobalProgress]
        private async Task ExitSpeedUp(int id)
        {
            var mod = Modules.Where(p => p.Id == id).First();
            mod.VitibilityRun = (int)ModuleVisibilityEum.Visible;
            mod.VitibilityStop = (int)ModuleVisibilityEum.Collapsed;
            try
            {
                Process[] processes = Process.GetProcessesByName("core");
                if (processes.Length > 0)
                {
                    foreach (Process p in processes)
                    {
                        p.Kill();
                    }
                }
            }
            catch (Exception e)
            {
                SnackBar(e.Message);

            }
            var returnCmd = CmdClass.ExecuteCoreStop();
            await Task.Delay(1000);

        }
        [GlobalProgress]
        private async Task OpenDialogShow()
        {
            await Task.Delay(5000);

        }

        private async Task DelUserGame(int id)
        {
            string xdid = IPMack.GetNetworkAdpaterID();
            GameDeleteUserRequest urls = new GameDeleteUserRequest();
            var url = "/" + urls.route + "/" + id.ToString();
            HeaderDto header = HeaderInfo.SetHeaderInfo("", xdid, url);
            var Result = await repository.GameDeleteUserAsync(id, header);
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
            if (Result.code == (int)ActionReturnCode.ok)
            {
                var m = Modules.Where(p => p.Id == id).First();
                Modules.Remove(m);
            }

        }
        private async Task SeachUserGame()
        {
            string plat = "windows";
            string xdid = IPMack.GetNetworkAdpaterID();
            GameSearchUserRequest urls = new GameSearchUserRequest();
            var url = "/" + urls.route + "/" + plat;
            HeaderDto header = HeaderInfo.SetHeaderInfo("", xdid, url);
            var Result = await repository.GameSearchUserAsync(plat, header);
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
            games = games.OrderByDescending(p => p.Id).ToList();
            Modules = new ObservableCollection<ModuleGameInfo>(games);
        }
        public async Task InitBind()
        {
            await this.SeachUserGame();
        }

    }

}
