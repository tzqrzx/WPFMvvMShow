using System;
using System.Diagnostics;

namespace Consumption.Shared.Common
{
    public class CmdClass
    {
        public static string serverpath = AppDomain.CurrentDomain.BaseDirectory;
        #region cmd命令
        public static string ExecuteWithOutput(string command)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/S /C " + command)
            {
                CreateNoWindow = false,
                UseShellExecute = false,
                // WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardOutput = true
            };

            var process = new Process { StartInfo = processInfo };
            process.Start();
            var outpup = process.StandardOutput.ReadToEnd();

            process.WaitForExit();
            return outpup;
        }

        public static void Execute(string command)
        {
            //Process p = new Process();
            //p.StartInfo.FileName = "cmd.exe";
            //p.StartInfo.Arguments = ""; //输入的参数
            //p.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
            //p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            //p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            //p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            //p.StartInfo.CreateNoWindow = false;//不显示程序窗口
            //p.Start();//启动程序
            //p.StandardInput.WriteLine("c:");//目录装到C盘
            //p.StandardInput.WriteLine(@"cd C:\Users\weixin\Desktop\latex");//目录跳转到目标目录下
            //p.StandardInput.WriteLine("pdflatex 杂谈勾股定理.tex");//编译
            //p.StandardInput.WriteLine("exit");//结束标志
            //string output = p.StandardOutput.ReadToEnd();//获取cmd窗口的输出信息，即便并无获取的需要也需要写这句话，不然程序会假死
            //p.WaitForExit();//等待程序执行完
            //p.Close();//退出进程
            var processInfo = new ProcessStartInfo("cmd.exe", "/S /C " + command)
            {
                CreateNoWindow = false,
                UseShellExecute = true,
                //WindowStyle = ProcessWindowStyle.Hidden
            };

            Process.Start(processInfo);
        }

        #endregion
        /// <summary>
        /// 运行tun2服务 运行调点服务一直运行
        /// </summary>
        /// <returns></returns>
        public static void ExecuteRunCode(string ip, string xnIp, string xnRoute, string Prex)
        {
            string corepath = serverpath + "tun/core.bat";
            var strIP = ip;
            var configPath = serverpath + "tun";
            corepath += " " + xnIp + " " + xnRoute + " " + Prex + " " + strIP + " " + configPath;
            CmdClass.Execute(corepath);
        }

        /// <summary>
        /// 启动tun2服务后 配置路由文件
        /// </summary>
        /// <param name="ip"></param>
        public static string ExecuteRoute(string route)
        {
            string routePath = serverpath + "tun/config_route.bat";
            string strrout = route;
            routePath += " " + strrout;
            return CmdClass.ExecuteWithOutput(routePath);
        }

        public static string ExecuteCoreStop()
        {

            return CmdClass.ExecuteWithOutput(serverpath + "tun/recover_route.bat");
        }

        public static string ExecuteSetupTopNetCareStop()
        {

            return CmdClass.ExecuteWithOutput(serverpath + "tun/ensure_tap_device.bat");
        }



    }
}
