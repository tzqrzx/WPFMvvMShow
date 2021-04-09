
namespace Consumption.ViewModel.Common.Aop
{
    using AspectInjector.Broker;
    using Consumption.Shared.Common;
    using Microsoft.Toolkit.Mvvm.Messaging;
    using System;

    /// <summary>
    /// 全局进度
    /// </summary>
    [Aspect(Scope.Global)]
    [Injection(typeof(GlobalProgress))]
    public class GlobalProgress : Attribute
    {

        public GlobalProgress() { }



        [Advice(Kind.Before, Targets = Target.Method)]
        public void Start([Argument(Source.Name)] string name)
        {
            UpdateLoading(true, name);
        }

        [Advice(Kind.After, Targets = Target.Method)]
        public async void End([Argument(Source.Name)] string name)
        {

            UpdateLoading(false, name);
        }

        void UpdateLoading(bool isOpen, string msg)
        {
            var Msgstr = "Loading......";
            if (msg == "SpeedUp")
                Msgstr = "启动加速中...";
            if (msg == "ExitSpeedUp")
                Msgstr = "停止加速中...";

            WeakReferenceMessenger.Default.Send(new MsgInfo()
            {
                IsOpen = isOpen,
                Msg = Msgstr
            }, "UpdateDialog");
        }
    }
}
