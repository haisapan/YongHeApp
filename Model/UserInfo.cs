using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using BaseLibrary;

namespace Model
{
    public class UserInfo:LambdaViewModelBase
    {
        private int _userId;
        private string _userName;
        private string _userPassword;

        public int UserId
        {
            get { return this._userId; }
            set { this._userId = value;
            OnPropertyChanged(o=>o.UserId);
            }
        }
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged(p=>p.UserName);
            }
        }

        public string UserPassword
        {
            get { return _userPassword; }
            set
            {
                _userPassword = value;
                OnPropertyChanged(p=>p.UserPassword);
            }
        }

        public string OldPassword { get; set; }


        protected virtual void OnPropertyChanged<R>(Expression<Func<UserInfo, R>> propertyExpr)
        {
            OnPropertyChanged(this.GetPropertySymbol(propertyExpr));
        }

    }
}
