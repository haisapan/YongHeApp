using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using BaseLibrary;

namespace Model
{
    public class ViewModelBase : LambdaViewModelBase, IDataErrorInfo
    {
        protected virtual void OnPropertyChanged<R>(Expression<Func<ViewModelBase, R>> propertyExpr)
        {
            OnPropertyChanged(this.GetPropertySymbol(propertyExpr));
        }

        /// <summary>
        /// IDataEorrorInfo 验证
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get
            {
                /* 参考 http://blog.csdn.net/fangxing80/article/details/8170441
                //这种验证方式会写很多switch-case,转而使用 System.ComponentModel.DataAnnotations验证
                //if (columnName == "Charge"&&this.Charge<0)  
                //{
                //    return "金额不能为负值";
                //}
                //return null;
                 */

                ValidationContext context = new ValidationContext(this, null, null);
                context.MemberName = columnName;
                var resultMessageList = new List<ValidationResult>();
                var results = Validator.TryValidateProperty(this.GetType().GetProperty(columnName).GetValue(this, null), context, resultMessageList);
                if (resultMessageList.Any())
                {
                    this.Error=string.Join(Environment.NewLine, resultMessageList.Select(p => p.ErrorMessage));
                    return this.Error;
                }

                this.Error = string.Empty;
                return string.Empty;

            }
        }

        public string Error { get; private set; }
    }
}
