using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BaseLibrary
{

    //    Net 4.5 introduces a new Caller Information Attributes.

    //private void OnPropertyChanged<T>([CallerMemberName]string caller = null) {
    //     // make sure only to call this if the value actually changes

    //     var handler = PropertyChanged;
    //     if (handler != null) {
    //        handler(this, new PropertyChangedEventArgs(caller));
    //     }
    //}
    //It's probably a good idea to add a comparer to the function as well.

    //EqualityComparer<T>.Default.Equals
    //More examples here and here

    //Also see Caller Information (C# and Visual Basic)

    public class ValidatePropertyViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Set Value的时候会进行属性的名称是否与约定属性名相同
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                throw new ArgumentException("属性名有误：" + propertyName);
                return false;
            }
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        // props
        private string name;
        public string Name
        {
            get { return name; }
            set { SetField(ref name, value, "Name"); }
        }

    }
}
