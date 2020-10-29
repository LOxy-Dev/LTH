using System.Windows;
using LTHWindow.Windows.CreateNew;

namespace LTHWindow.Windows.Home
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            
        }

        private void CreateNewButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var createNewWnd = new CreateNewWindow(); 
            
            createNewWnd.ShowDialog();
        }

        private void LoadButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void PreferencesButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}