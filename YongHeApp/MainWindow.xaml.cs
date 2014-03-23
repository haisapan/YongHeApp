using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BAL;
using BaseLibrary;
using MahApps.Metro.Controls;
using Model;
using YongHeApp.ViewModel;
using YongHeApp.Views;

namespace YongHeApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        private void LogoutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow=new LoginWindow();
            Application.Current.MainWindow = loginWindow;
            loginWindow.Show();
            this.Close();
        }

        private void ChangePasswordMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ModifyUserInfoWindow modifyUserInfoWindow=new ModifyUserInfoWindow();
            modifyUserInfoWindow.ShowDialog();

        }

        private void AddEventMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddRemoveEventWindow addRemoveEventWindow=new AddRemoveEventWindow();
            addRemoveEventWindow.ShowDialog();
        }

       
    }
}
