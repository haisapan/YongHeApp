using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using BAL;
using GalaSoft.MvvmLight.Command;
using Model;
using Model.Unity;
using PropertyChanged;

namespace YongHeApp.ViewModel
{
    public enum NoteEditType
    {
        Add,
        Modify
    }

    //[ImplementPropertyChanged] Fody有很多受限制的地方
    public class NoteEditViewModel : ViewModelBase, IDataErrorInfo
    {

        private int _selectedIncomeIndex;
        public List<string> EventList { get; set; }

        public List<string> IncomeTypeList { get; set; }

        public ViewModelBase ParentViewModel { get; set; }

        public NoteEditType NoteEditType { get; set; }

        public int SelectedEventIndex { get; set; }

        public int SelectedIncomeIndex
        {
            get { return _selectedIncomeIndex; }
            set
            {
                _selectedIncomeIndex = value;
                if (this.NoteModel != null)
                {
                    this.NoteModel.IsIncome = _selectedIncomeIndex == 0;
                }
                OnPropertyChanged("SelectedIncomeIndex");
                OnPropertyChanged("NoteModel");
            }
        }

        public NoteModel NoteModel { get; set; }


        public ICommand SubmitCommand { get; set; }
        public ICommand ResetCommand { get; set; }

        public NoteEditViewModel()
        {
            this.EventList = EventManager.Instance.GetList().Select(p=>p.Event).Distinct().ToList();
            //this.EventList = new List<string>()
            //                   {
            //                       "测试", "事件", "记账"
            //                   };

            this.IncomeTypeList = new List<string>()
                                    {
                                        "收入", "支出"
                                    };


            this.SubmitCommand = new RelayCommand(() =>
            {
                if (!string.IsNullOrEmpty(this.NoteModel.Error))
                {
                    //通知UI
                    return;
                }

                if (this.SelectedIncomeIndex == 0)
                {
                    this.NoteModel.IsIncome = true;
                }
                else
                {
                    this.NoteModel.IsIncome = false;
                }
                if (this.NoteEditType == NoteEditType.Add)
                {

                    this.NoteModel.NoteDate = DateTime.Now;
                    if (NoteManager.Instance.InsertNode(this.NoteModel))  //插入数据库
                    {
                        DisplayViewModel mainViewModel = this.ParentViewModel as DisplayViewModel;
                        if (mainViewModel != null && mainViewModel.NoteModelList.All(p => p.NoteId != this.NoteModel.NoteId))
                        {
                            mainViewModel.NoteModelList.Add(this.NoteModel);
                        }
                    }
                }
                else if (this.NoteEditType == NoteEditType.Modify)
                {

                    DisplayViewModel mainViewModel = this.ParentViewModel as DisplayViewModel;
                    if (mainViewModel != null)
                    {
                        NoteModel relatedNote = mainViewModel.NoteModelList.FirstOrDefault(p => p.NoteId == this.NoteModel.NoteId);
                        if (relatedNote != null)
                        {
                            relatedNote.Event = this.NoteModel.Event;
                            relatedNote.IsIncome = this.NoteModel.IsIncome;
                            relatedNote.Charge = this.NoteModel.Charge;
                            relatedNote.ChargePeopleName = this.NoteModel.ChargePeopleName;
                            relatedNote.Location = this.NoteModel.Location;
                            relatedNote.Note = this.NoteModel.Note;
                            relatedNote.UserId = this.NoteModel.UserId;
                            relatedNote.NoteDate = DateTime.Now;
                            NoteManager.Instance.UpdateNode(relatedNote);   //更新数据库
                        }
                    }

                }




            });

            this.ResetCommand = new RelayCommand(() =>
            {
                this.NoteModel.Event = string.Empty;
                //this.NoteModel.IsIncome = true;
                this.NoteModel.Charge = 0;
                this.NoteModel.ChargePeopleName = string.Empty;
                this.NoteModel.Location = string.Empty;
                this.NoteModel.Note = string.Empty;
                //this.NoteModel.UserId = this.NoteModel.UserId;
            });
        }

    }
}
