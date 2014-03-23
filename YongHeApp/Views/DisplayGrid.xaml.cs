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
using Model;
using YongHeApp.ViewModel;

namespace YongHeApp.Views
{
    /// <summary>
    /// DisplayGrid.xaml 的交互逻辑
    /// </summary>
    public partial class DisplayGrid : UserControl
    {
        public DisplayGrid()
        {
            InitializeComponent();

            this.DataContext = new DisplayViewModel();
        }

        private void AddNewNoteButton_Click(object sender, RoutedEventArgs e)
        {
            NoteEditWindow addNoteEditWindow = new NoteEditWindow()
            {
                DataContext = new NoteEditViewModel()
                {
                    ParentViewModel = (this.NoteDisplayGrid.DataContext as ViewModelBase),
                    NoteModel = new NoteModel(),
                    NoteEditType = NoteEditType.Add

                }
            };
            addNoteEditWindow.ShowDialog();


        }

        private void ModifyNoteButton_Click(object sender, RoutedEventArgs e)
        {
            Button modifyButton = sender as Button;
            if (modifyButton != null)
            {
                NoteModel noteModel = modifyButton.DataContext as NoteModel;

                if (noteModel != null)
                {
                    NoteModel newNoteModel = noteModel.Clone() as NoteModel;
                    if (newNoteModel == null)
                    {
                        //newNoteModel=new NoteModel();
                        MessageBox.Show("数据赋值出错，请检查");
                        return;
                    }
                    NoteEditViewModel modifyViewModel = new NoteEditViewModel()
                    {
                        ParentViewModel = this.DataContext as ViewModelBase,
                        NoteModel = newNoteModel,
                        NoteEditType = NoteEditType.Modify
                    };
                    NoteEditWindow modifyNoteEditWindow = new NoteEditWindow() { DataContext = modifyViewModel };
                    modifyNoteEditWindow.ShowDialog();
                }

            }
        }

        private void DeleteNoteButton_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            if (deleteButton != null)
            {
                NoteModel noteModel = deleteButton.DataContext as NoteModel;
                if (noteModel != null)
                {
                    if (NoteManager.Instance.DeleteNode(noteModel)) //从数据库删除
                    {
                        //如果删除成功的话，从界面删除
                        //这里可以重新从数据获取，但是如果重新获取数据库的话会增加事件，所以分两次删除
                        DisplayViewModel mainViewModel = this.DataContext as DisplayViewModel;
                        if (mainViewModel != null && mainViewModel.NoteModelList.Contains(noteModel))
                        {
                            mainViewModel.NoteModelList.Remove(noteModel);
                        }
                    }
                }

            }
        }
    }
}
