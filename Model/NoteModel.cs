using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using BaseLibrary;
using Model.Unity;
using PropertyChanged;

namespace Model
{
    [ImplementPropertyChanged]
    public class NoteModel : ViewModelBase, ICloneable //: LambdaViewModelBase
    {
        private string _noteId;
        private string _event;
        private bool _isIncome;
        private float _charge;
        private string _chargePeopleName;
        private string _location;
        private DateTime _noteDate;
        private string _note;
        private string _userId;
        private string _incomeType;

        /// <summary>
        /// 只有在数据库读取值的时候才进行赋值，其他时候都自动生成NoteId
        /// </summary>
        public string NoteId
        {
            get { return _noteId; }
            set
            {
                _noteId = value;
                OnPropertyChanged("NoteId");
            }
        }

        public string Event
        {
            get { return _event; }
            set
            {
                _event = value;
                OnPropertyChanged("Event");
            }
        }

        public string IncomeType
        {
            get { return this.IsIncome ? "收入" : "支出"; }
        }

        public bool IsIncome
        {
            get { return _isIncome; }
            set
            {
                _isIncome = value;
                OnPropertyChanged("IsIncome");
                OnPropertyChanged("IncomeType");
            }
        }

        [Required(ErrorMessage = "金额不能为空")]
        [Range(0,float.MaxValue,ErrorMessage = "数值必须大于0")]
        public float Charge
        {
            get { return _charge; }
            set
            {
                _charge = value;
                OnPropertyChanged("Charge");
            }
        }

        [Required(ErrorMessage = "人员不能为空！")]
        //[StringLength(500,ErrorMessage = "最短长度为2",MinimumLength =2 )]
        public string ChargePeopleName
        {
            get { return _chargePeopleName; }
            set
            {
                _chargePeopleName = value;
                OnPropertyChanged("ChargePeopleName");
            }
        }

        [Required(ErrorMessage = "人员不能为空！")]
        //[StringLength(200,ErrorMessage ="请填入位置，长度最短为2",MinimumLength = 2)]
        public string Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged("Location");
            }
        }

        //登陆用户，暂时不用
        public string UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                OnPropertyChanged("UserId");
            }
        }

        public DateTime NoteDate
        {
            get { return _noteDate; }
            set
            {
                _noteDate = value;
                OnPropertyChanged("NoteDate");
            }
        }

        public string Note
        {
            get { return _note; }
            set
            {
                _note = value;
                OnPropertyChanged("Note");
            }
        }

        public NoteModel()
        {
            //this.UseExplicitValidation = true;

            this.NoteId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 该类的浅复制
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

     
    }
}
