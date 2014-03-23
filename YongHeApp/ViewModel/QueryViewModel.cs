using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using BAL;
using BaseLibrary;
using GalaSoft.MvvmLight.Command;
using Model;
using System.Collections.ObjectModel;
using Model.Unity;

namespace YongHeApp.ViewModel
{
    public class QueryViewModel : ValidationViewModel, IDataErrorInfo//ViewModelBase
    {
        private DateTime _startDate = DateTime.Now.AddDays(-1);
        private DateTime _endDate = DateTime.Now;
        private string _selectedEvent;
        private float _income;
        private float _expenses;
        private float _profit;

        [Required]
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                RaisePropertyChanged(p => p.StartDate);

            }
        }

        //[Required]
        [DateCompare(CompareToOperation.GreaterThanOrEqual, "StartDate", ErrorMessage = "结束日期必须大于等于起始日期")]
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                RaisePropertyChanged(p => p.EndDate);
            }
        }

        public int SelectedEventIndex { get; set; }

        public string SelectedEvent
        {
            get { return _selectedEvent; }
            set
            {
                _selectedEvent = value;
                RaisePropertyChanged(p => p.SelectedEvent);
            }
        }

        public List<string> EventList { get; set; }

        public float Income
        {
            get { return _income; }
            set
            {
                _income = value;
                RaisePropertyChanged(p => p.Income);
            }
        }

        public float Expenses
        {
            get { return _expenses; }
            set
            {
                _expenses = value;
                RaisePropertyChanged(p => p.Expenses);
            }
        }


        public float Profit
        {
            get
            {
                return _profit;
            }
            set
            {
                _profit = value;
                RaisePropertyChanged(p => p.Profit);
            }
        }

        public ObservableCollection<NoteModel> ResultNotes { get; private set; }

        public ICommand QueryCommand { get; set; }

        public QueryViewModel()
        {
            this.UseExplicitValidation = true;

            ResultNotes = new ObservableCollection<NoteModel>();
            this.EventList = EventManager.Instance.GetList().Select(p=>p.Event).Distinct().ToList(); //TODO 从数据库获取可用事件
            this.EventList.Insert(0,string.Empty);   // 添加一项为空的，用来查询所有的事项
            EventManager.Instance.OnEventListChanged=()=>
                                                         {
                                                             this.EventList = EventManager.Instance.GetList().Select(p => p.Event).Distinct().ToList();
                                                             this.EventList.Insert(0, string.Empty);        // 添加一项为空的，用来查询所有的事项
                                                         };


            this.QueryCommand = new RelayCommand(() =>
                                                   {
                                                       //TODO 需要处理开始日期大于结束日期的问题

                                                       var noteList = NoteManager.Instance.GetNodeListByCondition(this.StartDate,
                                                                                                   this.EndDate,
                                                                                                   this.SelectedEvent);

                                                       this.ResultNotes.Clear();  //清空原有数据
                                                       this.Income = this.Expenses = this.Profit = 0;
                                                       foreach (NoteModel noteModel in noteList)
                                                       {
                                                           this.ResultNotes.Add(noteModel);
                                                           if (noteModel.IsIncome)
                                                           {
                                                               this.Income += noteModel.Charge;
                                                           }
                                                           else
                                                           {
                                                               this.Expenses += noteModel.Charge;
                                                           }
                                                       }

                                                       this.Profit = this.Income - this.Expenses;

                                                   }, () =>
                                                          {
                                                              return !this.HasErrors;
                                                             //return string.IsNullOrEmpty(this.Error);
                                                         });

        }


        /// <summary>
        /// 每个ViewModel都实现
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="propertyExpr"></param>
        protected virtual void RaisePropertyChanged<R>(Expression<Func<QueryViewModel, R>> propertyExpr)
        {
            if (UseExplicitValidation)
            {
                this.Validate();
            }

            OnPropertyChanged(this.GetPropertySymbol(propertyExpr));
        }

    }
}
