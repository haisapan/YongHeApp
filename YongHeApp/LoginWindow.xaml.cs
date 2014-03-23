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
using System.Windows.Shapes;
using BAL;
using MahApps.Metro.Controls;

namespace YongHeApp
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string resultMessage;
            if (LoginManager.Instance.Login(this.UserNameTextbox.Text.Trim(), this.Password_PwdBox.Password, out resultMessage))
            {
                Application.Current.MainWindow=new MainWindow();
                Application.Current.MainWindow.Show();
                this.Close();
            }
            else
            {
                if (string.IsNullOrEmpty(resultMessage))
                {
                    resultMessage = "登陆失败";
                }

                MessageBox.Show(resultMessage);
                
            }
            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
