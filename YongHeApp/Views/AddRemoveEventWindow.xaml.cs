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
using MahApps.Metro.Controls;
using YongHeApp.ViewModel;

namespace YongHeApp.Views
{
    /// <summary>
    /// AddRemoveEventWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddRemoveEventWindow : MetroWindow
    {
        public AddRemoveEventWindow()
        {
            InitializeComponent();
            this.DataContext = new EventManageViewModel();
        }
    }
}
