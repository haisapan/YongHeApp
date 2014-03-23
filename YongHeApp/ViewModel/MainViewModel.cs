using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Model;

namespace YongHeApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private int _viewIndex;
        public int ViewIndex
        {
            get { return _viewIndex; }
            set
            {
                _viewIndex = value;
                OnPropertyChanged("ViewIndex");
            }
        }

        public ICommand ChangeViewCommand { get; set; } 

        public MainViewModel()
        {
            this.ChangeViewCommand=new RelayCommand<int>(
                index=>
                    {
                        this.ViewIndex = index;
                    }
            );
        }
    }
}
