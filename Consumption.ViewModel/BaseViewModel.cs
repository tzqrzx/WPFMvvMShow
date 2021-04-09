


namespace Consumption.ViewModel
{
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;
    using Microsoft.Toolkit.Mvvm.Messaging;

    /// <summary>
    /// MVVM基类
    /// </summary>
    public class BaseDialogViewModel : ObservableObject
    {
        public BaseDialogViewModel()
        {
            ExitCommand = new RelayCommand(Exit);
        }

        public RelayCommand ExitCommand { get; private set; }

        /// <summary>
        /// 传递True代表需要确认用户是否关闭,你可以选择传递false强制关闭
        /// </summary>
        public virtual void Exit()
        {
            WeakReferenceMessenger.Default.Send("", "Exit");
        }

        private bool isOpen;

        /// <summary>
        /// 窗口是否显示
        /// </summary>
        public bool DialogIsOpen
        {
            get { return isOpen; }
            set { isOpen = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 通知异常
        /// </summary>
        /// <param name="msg"></param>
        public void SnackBar(string msg)
        {
            WeakReferenceMessenger.Default.Send(msg, "Snackbar");
        }
    }

    public class BaseViewModel : ObservableObject
    {
        public BaseViewModel()
        {
            ExitCommand = new RelayCommand(Exit);
        }

        public RelayCommand ExitCommand { get; private set; }

        /// <summary>
        /// 传递True代表需要确认用户是否关闭,你可以选择传递false强制关闭
        /// </summary>
        public virtual void Exit()
        {
            WeakReferenceMessenger.Default.Send("", "Exit");
        }



        private bool isOpen;

        /// <summary>
        /// 窗口是否显示
        /// </summary>
        public bool DialogIsOpen
        {
            get { return isOpen; }
            set { isOpen = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 通知异常
        /// </summary>
        /// <param name="msg"></param>
        public void SnackBar(string msg)
        {
            WeakReferenceMessenger.Default.Send(msg, "Snackbar");
        }
    }
}
