using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using LTHWindow.Windows.CreateNew;
using LTHWindow.Windows.Home;

namespace LTHWindow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static HomeWindow _homeWnd;
        
        public static Tournament.Tournament Tournament { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _homeWnd = new HomeWindow();
            
            _homeWnd.Show();
        }
    }
}