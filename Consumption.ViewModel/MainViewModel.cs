
namespace Consumption.ViewModel
{
    using Consumption.Shared.Common;
    using Consumption.Shared.Common.Security;
    using Consumption.ViewModel.Common;
    using Consumption.ViewModel.Interfaces;
    using Microsoft.Toolkit.Mvvm.Input;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// 应用首页
    /// </summary>
    public class MainViewModel : BaseDialogViewModel, IMainViewModel
    {
        public string defaultImg = "Assets/man01.png";
        public MainViewModel()
        {

            SetDefault();
            CloseCommand = new RelayCommand(Exit);
            OpenPageCommand = new AsyncRelayCommand<string>(OpenPageNew);
            LoginOpenPageCommand = new AsyncRelayCommand<string>(OpenPageLogin);
            ViewIndex = 0;
            //ClosePageCommand = new RelayCommand<string>(ClosePage);
            //GoHomeCommand = new RelayCommand(InitHomeView);
            //ExpandMenuCommand = new RelayCommand(() =>
            //{
            //    for (int i = 0; i < ModuleManager.ModuleGroups.Count; i++)
            //    {
            //        var arg = ModuleManager.ModuleGroups[i];
            //        arg.ContractionTemplate = !arg.ContractionTemplate;
            //    }
            //    WeakReferenceMessenger.Default.Send("", "ExpandMenu");
            //});
        }

        private void SetDefault()
        {
            string imgPath = defaultImg;
            if (CacheMack.IsConfig()) //第一次登录
            {
                if (!Contract.IsLogin)
                {
                    var cacheinfo = CacheMack.ReadJsonCache();

                    //if (DateTime.Parse(cacheinfo.expireTime) >= DateTime.Now)//此处应获取服务器时间
                    //{
                    Contract.IsLogin = true;
                    Contract.sid = cacheinfo.sid;
                    Contract.vipLevel = cacheinfo.vipLevel;
                    Contract.nickName = cacheinfo.nickName;
                    Contract.refer = cacheinfo.refer;
                    Contract.headImg = cacheinfo.headImg;
                    imgPath = cacheinfo.headImg;
                    //}
                    //else
                    //{
                    //    Contract.IsLogin = false;
                    //    imgPath = defaultImg;
                    //}
                }
                else
                {
                    imgPath = Contract.headImg;
                }
            }
            ImgUserPath = imgPath;

        }

        private int viewIndex;
        private string phone;
        #region Property

        public int ViewIndex
        {
            get { return viewIndex; }
            set { viewIndex = value; OnPropertyChanged(); }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; OnPropertyChanged(); }
        }

        private string imgPath;
        public string ImgUserPath
        {
            get { return imgPath; }
            set { imgPath = value; OnPropertyChanged(); }
        }

        private ModuleUIComponent currentModule;

        /// <summary>
        /// 当前选中模块
        /// </summary>
        public ModuleUIComponent CurrentModule
        {
            get { return currentModule; }
            set { currentModule = value; OnPropertyChanged(); }
        }


        private ObservableCollection<ModuleUIComponent> moduleList;

        /// <summary>
        /// 所有展开的模块
        /// </summary>
        public ObservableCollection<ModuleUIComponent> ModuleList
        {
            get { return moduleList; }
            set { moduleList = value; OnPropertyChanged(); }
        }

        private ModuleManager moduleManager;

        /// <summary>
        /// 模块管理器
        /// </summary>
        public ModuleManager ModuleManager
        {
            get { return moduleManager; }
            set { moduleManager = value; OnPropertyChanged(); }
        }

        #endregion

        #region Command
        /// <summary>
        /// 关闭窗体
        /// </summary>
        public RelayCommand CloseCommand { get; private set; }
        /// <summary>
        /// 菜单栏收缩
        /// </summary>
        public RelayCommand ExpandMenuCommand { get; private set; }

        /// <summary>
        /// 返回首页
        /// </summary>
        public RelayCommand GoHomeCommand { get; private set; }

        /// <summary>
        /// 打开新页面，string: 模块名称
        /// </summary>
        public AsyncRelayCommand<string> OpenPageCommand { get; private set; }

        public AsyncRelayCommand<string> LoginOpenPageCommand { get; private set; }

        /// <summary>
        /// 关闭选择页, string: 模块名称
        /// </summary>
        public RelayCommand<string> ClosePageCommand { get; private set; }

        public RelayCommand MinCommand { get; private set; } = new RelayCommand(() =>
        {
            WeakReferenceMessenger.Default.Send("", "WindowMinimize");
        });


        public RelayCommand MaxCommand { get; private set; } = new RelayCommand(() =>
        {
            WeakReferenceMessenger.Default.Send("", "WindowMaximize");
        });

        public RelayCommand TestCommand { get; private set; } = new RelayCommand(() =>
        {
            WeakReferenceMessenger.Default.Send(string.Empty, "delegateShowUser");
        });

        public override void Exit()
        {
            WeakReferenceMessenger.Default.Send(string.Empty, "Exit");
        }

        #endregion

        /// <summary>
        /// 打开页面
        /// </summary>
        /// <param name="pageName"></param>
        /// <returns></returns>
        public async virtual Task OpenPage(string pageName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(pageName)) return;
                var pageModule = this.ModuleManager.Modules.FirstOrDefault(t => t.Name.Equals(pageName));
                if (pageModule == null) return;

                var module = this.ModuleList.FirstOrDefault(t => t.Name == pageModule.Name);
                if (module == null)
                {
                    var dialog = NetCoreProvider.ResolveNamed<IBaseCenter>(pageModule.TypeName);
                    await dialog.BindDefaultModel(pageModule.Auth);
                    ModuleList.Add(new ModuleUIComponent()
                    {
                        Code = pageModule.Code,
                        Auth = pageModule.Auth,
                        Name = pageModule.Name,
                        TypeName = pageModule.TypeName,
                        Body = dialog.GetView()
                    });
                    CurrentModule = ModuleList.Last();
                }
                else
                    CurrentModule = module;
            }
            catch (Exception ex)
            {
                Msg.Error(ex.Message);
            }
        }


        public async virtual Task OpenPageNew(string pageName)
        {
            try
            {
                ModuleList.Clear();
                if (pageName == "Escolha")
                {
                    var dialog = NetCoreProvider.ResolveNamed<IUserControlEscolhaCenter>("UserControlEscolhaCenter");
                    await dialog.InitBindShow();
                    dialog.BindDefaultModel();

                    ModuleList.Add(new ModuleUIComponent()
                    {
                        Name = "我的游戏",
                        Code = pageName,
                        Body = dialog.GetView()
                    });
                    CurrentModule = ModuleList.Last();
                }
                if (pageName == "Inicio")
                {
                    var dialog = NetCoreProvider.ResolveNamed<IUserControlInicioCenter>("UserControlInicioCenter");
                    await dialog.InitBindShow();
                    dialog.BindDefaultModel();
                    ModuleList.Add(new ModuleUIComponent()
                    {
                        Name = "全部游戏",
                        Code = pageName,
                        Body = dialog.GetView()
                    });
                    CurrentModule = ModuleList.First();
                }

            }
            catch (Exception ex)
            {
                Msg.Error(ex.Message);
            }
        }


        public async virtual Task OpenPageLogin(string pageName)
        {

            try
            {
                var Main = NetCoreProvider.ResolveNamed<ILoginCenter>("LoginCenter");
                var Resut = await Main.ShowDialog();
                if (!Resut)
                {
                    SetDefault();
                }
            }
            catch (Exception ex)
            {
                Msg.Error(ex.Message);
            }
        }
        /// <summary>
        /// 关闭页面
        /// </summary>
        /// <param name="pageName"></param>
        public void ClosePage(string pageName)
        {
            var module = this.ModuleList.FirstOrDefault(t => t.Name.Equals(pageName));
            if (module != null)
            {
                this.ModuleList.Remove(module);
                if (this.ModuleList.Count > 0)
                    this.CurrentModule = this.ModuleList.Last();
                else
                    this.CurrentModule = null;
            }
        }

        /// <summary>
        /// 初始化页面上下文内容
        /// </summary>
        /// <returns></returns>
        public async Task InitDefaultView()
        {
            /*
             *  加载首页的程序集模块
             *  1.首先获取本机的所有可用模块
             *  2.利用服务器验证,过滤掉不可用模块
             *
             *  注:理论上管理员应该可用本机的所有模块, 
             *  当检测本机用户属于管理员,则不向服务器验证
             */

            //创建模块管理器
            ModuleManager = new ModuleManager();
            ModuleList = new ObservableCollection<ModuleUIComponent>();
            //加载自身的程序集模块
            // await ModuleManager.LoadAssemblyModule();
            InitHomeView();
        }



        /// <summary>
        /// 初始化首页
        /// </summary>
        void InitHomeView()
        {
            // var dialog = NetCoreProvider.ResolveNamed<IHomeCenter>("HomeCenter");
            var dialog = NetCoreProvider.ResolveNamed<IUserControlEscolhaCenter>("UserControlEscolhaCenter");
            dialog.InitBindShow();
            dialog.BindDefaultModel();
            dialog.BindDefaultModel();
            ModuleUIComponent component = new ModuleUIComponent();
            component.Code = "Escolha";
            component.Name = "我的游戏";
            component.Body = dialog.GetView();
            viewIndex = 0;
            ModuleList.Add(component);
            //ModuleManager.Modules.Add(component);
            CurrentModule = ModuleList.Last();
        }



    }
}
