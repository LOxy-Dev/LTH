using System.Windows;
using LTHWindow.Windows.Home;
using LTHWindow.Windows.Main;

namespace LTHWindow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static HomeWindow _homeWnd;
        public static MainWindow MainWnd { get; set; }
        
        public static Tournament.Tournament Tournament { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _homeWnd = new HomeWindow();
            
            _homeWnd.Show();
        }

        public static void LoadMainWindow(Tournament.Tournament tournament)
        {
            if (_homeWnd.IsEnabled)
                _homeWnd.Close();
            
            MainWnd = new MainWindow(tournament);
            MainWnd.Show();
        }
    }
}