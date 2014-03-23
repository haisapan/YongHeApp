using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DAL;

namespace BAL
{
    public class DataBaseManager
    {
        private static void CreateDataBase()
        {
            string dataBasePath= Environment.CurrentDirectory + "\\DataBase\\yongheDataBase.db3";
            if (!Directory.Exists(Environment.CurrentDirectory + "\\DataBase"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\DataBase");
            }

            if (!System.IO.File.Exists(dataBasePath))
            {
                DAL.SqLiteDataBaseHelper.CreateDataBase(dataBasePath);
            } 
            
        }
    }
}
