using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using BaseLibrary;

namespace Model
{
    /// <summary>
    /// Event:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class EventInfo:ViewModelBase
    {
        public EventInfo()
        { }
        #region Model

        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                this.RaisePropertyChanged(p=>p.Id);
            }
        }

        private string _event;
        private int _id;

        /// <summary>
        /// 
        /// </summary>
        public string Event
        {
            get{return _event;}
            set
            {
                _event = value;
                this.RaisePropertyChanged(p => p.Event);
            }

        }
        #endregion Model



         /// <summary>
        /// 每个ViewModel都实现
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="propertyExpr"></param>
        protected virtual void RaisePropertyChanged<R>(Expression<Func<EventInfo, R>> propertyExpr)
        {
            OnPropertyChanged(this.GetPropertySymbol(propertyExpr));
        }

    }
}
