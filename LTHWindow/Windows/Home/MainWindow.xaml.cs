﻿using System.Windows;
using LTHWindow.Windows.CreateNew;

namespace LTHWindow.Windows.Home
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void CreateNewButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var createNewWnd = new CreateNewTournament(); 
            
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