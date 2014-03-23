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
using Model;

namespace YongHeApp.Views
{
    /// <summary>
    /// ModifyUserInfoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ModifyUserInfoWindow : MetroWindow
    {
        public ModifyUserInfoWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.NewPasswordBox.Password)||this.NewPasswordBox.Password.Length<5)
            {
                MessageBox.Show("密码必须为5位以上");
                return;
            }

            if (this.ConfirmPasswordBox.Password!=this.NewPasswordBox.Password)
            {
                MessageBox.Show("确认密码必须与新密码一致");
                return;
            }
            UserInfo userInfo=new UserInfo()
                                  {
                                      UserName = GlobalInfo.CurrentUserName, 
                                      OldPassword = this.OldPasswordBox.Password,
                                      UserPassword = this.NewPasswordBox.Password,
                                  };

            string error=null;
            if (LoginManager.Instance.ModifyPassword(userInfo, out error))
            {
                MessageBox.Show("修改密码成功");
                this.Close();
            }
            else
            {
                MessageBox.Show(error);
            }
            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
