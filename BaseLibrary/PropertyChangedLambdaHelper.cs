using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BaseLibrary
{
    public static class PropertyChangedLambdaHelper
    {
        public static string GetPropertySymbol<T, R>(this T obj, Expression<Func<T, R>> expr)
        {
            return ((MemberExpression)expr.Body).Member.Name;
        }
    }
}
