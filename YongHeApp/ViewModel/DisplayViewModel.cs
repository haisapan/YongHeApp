using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Windows.Input;
using BAL;
using BaseLibrary;
using Model;
using GalaSoft.MvvmLight.Command;

namespace YongHeApp.ViewModel
{
    public class DisplayViewModel : ViewModelBase
    {
        private DateTime _datePickerDate = DateTime.Now;

        public DateTime DatePickerDate
        {
            get { return _datePickerDate; }
            set
            {
                _datePickerDate = value;
                RaisePropertyChanged(p => p.DatePickerDate);
            }
        }

        public ObservableCollection<NoteModel> NoteModelList { get; set; }

        public ICommand QueryCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public DisplayViewModel()
        {
            this.NoteModelList = NoteManager.Instance.GetNodeListOfByDate(this.DatePickerDate);

            this.QueryCommand = new RelayCommand(() =>
                                                   {
                                                       this.NoteModelList = NoteManager.Instance.GetNodeListOfByDate(this.DatePickerDate);
                                                   });
        }

        /// <summary>
        /// 每个ViewModel都实现
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="propertyExpr"></param>
        protected virtual void RaisePropertyChanged<R>(Expression<Func<DisplayViewModel, R>> propertyExpr)
        {
            OnPropertyChanged(this.GetPropertySymbol(propertyExpr));
        }


    }
}
