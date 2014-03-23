using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Model.Unity
{
    public enum CompareToOperation
    {
        EqualTo,
        LessThan,
        GreaterThan,
        GreaterThanOrEqual,
        LessThanOrEqual
    }

    /// <summary>
    /// 不知道为什么，自定义的ValidationAttribute不会更新UI, 暂时未找到原因
    /// </summary>
    public class DateCompareAttribute : ValidationAttribute
    {
        private CompareToOperation _operation;
        private string _comparisionPropertyName;

        /// <summary>
        /// 选择比较符，及要比较的属性，进行验证
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="comparisonPropertyName"></param>
        public DateCompareAttribute(CompareToOperation operation, string comparisonPropertyName)
        {
            _operation = operation;
            _comparisionPropertyName = comparisonPropertyName;
        }

        //private static IComparable GetComparablePropertyValue(object obj, string propertyName)
        //{
        //    if (obj == null) return null;
        //    var type = obj.GetType();
        //    var propertyInfo = type.GetProperty(propertyName);
        //    if (propertyInfo == null)
        //    {
        //        return null;
        //    }
        //    return propertyInfo.GetValue(obj, null) as IComparable;
        //}

        /// <summary>
        /// 重写IsValid 方法进行验证
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(this._comparisionPropertyName);
            var targetValue = property.GetValue(validationContext.ObjectInstance, null);
            IComparable target = targetValue as IComparable;

            IComparable current = value as IComparable;
            if (current == null || target == null)
            {
                return new ValidationResult(this.ErrorMessage, new string[] { validationContext.MemberName });
            }

            try
            {
                DateTime endDate = Convert.ToDateTime(current);
                DateTime startDate = Convert.ToDateTime(targetValue);

                var result = endDate.CompareTo(startDate);


                bool validateResult = false;
                switch (_operation)
                {
                    case CompareToOperation.LessThan:
                        validateResult = result == -1;
                        break;
                    case CompareToOperation.EqualTo:
                        validateResult = result == 0;
                        break;
                    case CompareToOperation.GreaterThan:
                        validateResult = result == 1;
                        break;
                    case CompareToOperation.LessThanOrEqual:
                        validateResult = result == -1 || result == 0;
                        break;
                    case CompareToOperation.GreaterThanOrEqual:
                        validateResult = result == 1 || result == 0;
                        break;
                    default:
                        validateResult = false;
                        break;
                }
                if (validateResult)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult(this.ErrorMessage, new string[] { validationContext.MemberName });
            }
            catch (Exception)
            {
                return new ValidationResult("时间格式不对", new string[] { validationContext.MemberName });
            }



        }



    }
}
