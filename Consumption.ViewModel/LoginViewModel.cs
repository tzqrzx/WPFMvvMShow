
namespace Consumption.ViewModel
{
    using Consumption.Core.Request;
    using Consumption.Shared.Common;
    using Consumption.Shared.Common.Enums;
    using Consumption.Shared.Common.Security;
    using Consumption.Shared.Common.Validation;
    using Consumption.Shared.Dto;
    using Consumption.ViewModel.Common;
    using Consumption.ViewModel.Interfaces;
    using Microsoft.Toolkit.Mvvm.Input;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using Newtonsoft.Json;
    using System;
    using System.Threading.Tasks;
    using System.Timers;

    /// <summary>
    /// 登录模块
    /// </summary>
    public class LoginViewModel : BaseDialogViewModel, ILoginViewModel
    {
        private bool _IsTimerBusy = false;
        private Timer timerSend = null;

        public LoginViewModel(IUserRepository repository)
        {
            this.repository = repository;
            LoginCommand = new RelayCommand(Login);
            CloseCommand = new RelayCommand(Exit);
            SMSCommand = new RelayCommand(SendSMSMessage);
            BtnContent = "发送短信";
            IsCancel = true;
        }
        #region Property
        private int _interval;//记录倒计时长
        private int _secondNum = 60;//设置倒计时长
        private string userName;
        private string phone;
        private string code;
        private string btnContent;
        private string passWord;
        private string report;
        private Boolean isCancel;
        private readonly IUserRepository repository;

        public int Interval
        {
            get { return _interval; }
            set { _interval = value; OnPropertyChanged(); }
        }

        public int SecondNum
        {
            get { return _secondNum; }
            set { _secondNum = value; OnPropertyChanged(); }
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

        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; OnPropertyChanged(); }
        }

        public string Report
        {
            get { return report; }
            set { report = value; OnPropertyChanged(); }
        }

        public bool IsCancel
        {
            get { return isCancel; }
            set { isCancel = value; OnPropertyChanged(); }
        }
        #endregion

        #region Command

        public RelayCommand LoginCommand { get; private set; }
        public RelayCommand CloseCommand { get; private set; }
        public RelayCommand SMSCommand { get; private set; }
        /// <summary>
        /// 登录系统
        /// </summary>
        private async void Login()
        {
            try
            {
                // 获得本机局域网IP地址
                string ip2 = IPMack.GetLocalIp();

                if (DialogIsOpen) return;
                if (string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(Code))
                {
                    SnackBar("电话号，验证码不能为空!");
                    return;
                }

                if (!Validation.IsPhone(Phone))
                {
                    SnackBar("您输入手机号不符合规则!");
                    return;
                }

                if (code.Length != 6)
                {
                    SnackBar("验证码长度不正确!");
                    return;
                }

                DialogIsOpen = true;
                await Task.Delay(300);
                string xdid = IPMack.GetNetworkAdpaterID();
                var requestuser = new RequestUserDto()
                {
                    channel = "windows",
                    code = Code,
                    did = xdid,
                    inviteCode = "",
                    phone = Phone,
                    platform = "windows"
                };
                UserLoginRequest urls = new UserLoginRequest();
                var url = "/" + urls.route;
                HeaderDto header = HeaderInfo.SetHeaderInfo(requestuser, xdid, url);
                var loginResult = await repository.LoginAsync(requestuser, JsonConvert.SerializeObject(requestuser), header);
                if (loginResult == null)
                {
                    SnackBar("服务接口连接错误！");
                    return;
                }
                if (loginResult.code != (int)ActionReturnCode.ok)
                {
                    SnackBar(loginResult.message);
                    return;
                }
                var cacheUser = JsonConvert.DeserializeObject<CacheInfo>(JsonConvert.SerializeObject(loginResult.result));
                CacheMack.CreateJsonCache(cacheUser);


                #region 关联用户信息/缓存
                Contract.nickName = cacheUser.nickName;
                Contract.sid = cacheUser.sid;
                Contract.vipLevel = cacheUser.vipLevel;
                Contract.headImg = cacheUser.headImg;
                Contract.createTime = cacheUser.createTime;
                Contract.expireTime = cacheUser.expireTime;
                Contract.IsLogin = false;
                #endregion

                SnackBar("登录成功");
                await Task.Delay(300);
                this.Code = "";
                this.Phone = "";
                //这行代码会发射到首页,Center中会定义所有的Messenger
                WeakReferenceMessenger.Default.Send(string.Empty, "QExit");
            }
            catch (Exception ex)
            {
                SnackBar(ex.Message);
            }
            finally
            {
                DialogIsOpen = false;
            }
        }

        #region 发送短信
        private async void SendSMSMessage()
        {
            if (DialogIsOpen) return;
            if (string.IsNullOrWhiteSpace(Phone))
            {
                SnackBar("请输入手机号!");
                return;
            }
            await Task.Delay(300);
            if (!Validation.IsPhone(Phone))
            {
                SnackBar("您输入手机号不符合规则!");
                return;
            }

            await Task.Delay(300);
            IsCancel = false;
            BtnContent = "发送短信中";
            var photodto = new phoneDto()
            {
                opType = "register",
                phone = Phone
            };
            UserSMSRegionRequest urls = new UserSMSRegionRequest();
            var url = "/" + urls.route;
            HeaderDto header = HeaderInfo.SetHeaderInfo(photodto, "", url);
            var loginResult = await repository.SendSMSAsync(photodto, JsonConvert.SerializeObject(photodto), header);
            if (loginResult == null)
            {
                SnackBar("服务接口连接错误！");
                return;
            }
            if (loginResult.code != (int)ActionReturnCode.ok)
            {
                SnackBar(loginResult.message);
                return;
            }

            if (loginResult.code == (int)ActionReturnCode.ok)
            {

                //获取验证码
                timerSend = new Timer(1000);
                timerSend.AutoReset = true;
                timerSend.Elapsed += Timer_Elapsed;
                _interval = SecondNum;
                timerSend.Start();
            }

        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_IsTimerBusy)
            {
                return;
            }
            try
            {
                _IsTimerBusy = true;
                IsCancel = false;
                BtnContent = "( " + (_interval--) + " )";
                if (_interval <= -1)
                {
                    IsCancel = true;
                    BtnContent = "获取验证码";
                    timerSend.Stop();
                    timerSend.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _IsTimerBusy = false;
            }

        }
        #endregion


        #endregion

        public override void Exit()
        {
            WeakReferenceMessenger.Default.Send(string.Empty, "QExit");
        }


    }
}
