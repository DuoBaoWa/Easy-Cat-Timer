using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CatTimer_WpfProject
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        //当程序启动完成时，触发此方法
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            try
            {
                AppManager.MainApp = this;
                AppManager.Awake();

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                string errorLog = ex.ToString();
                if (ex.InnerException != null)
                {
                    errorLog += "\nINNER ERROR: " + ex.InnerException.ToString();
                }
                System.IO.File.WriteAllText("startup_error_v2.log", errorLog);
                Console.WriteLine("CRITICAL ERROR: " + errorLog);
                Shutdown(1);
            }
        }

        //当程序退出时，触发此方法
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            AppManager.Exit();
        }
    }
}
