using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseLibrary
{
    /// <summary>
    /// 继承该类即可具有单例特性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingleTonHelper<T> where T : class ,new()
    {
        private static T _instance;
        private static object _lock=new object();
        //public SingleTonHelper()
        //{
        //    throw new Exception("单例类不能通过构造函数实例化");
        //}

        protected SingleTonHelper()
        {
            
        }

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new T();
                    }
                }
               
                return _instance;
            }
        }

    }
}
