using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BaseLibrary
{
    public abstract class LambdaViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //public  abstract void OnPropertyChanged<R>(Expression<Func<LambdaViewModelBase, R>> propertyExpr);

        //父类得不到子类的属性，这点很麻烦
        //继承类中必须使用该办法
        //protected virtual void OnPropertyChanged<R>(Expression<Func<LambdaViewModelBase, R>> propertyExpr) 
        //{
        //    OnPropertyChanged(this.GetPropertySymbol(propertyExpr));
        //}

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
