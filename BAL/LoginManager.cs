using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using BaseLibrary;
using DAL;
using Model;

namespace BAL
{
    public class LoginManager : SingleTonHelper<LoginManager>
    {

        /// <summary>
        /// 读取数据库信息判断是否允许登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool Login(string userName, string password, out string message)
        {
            string loginSql = "select * from User where userName=@userName and password=@password";
            DbParameter[] parameters = new SQLiteParameter[]{ 
                                                 new SQLiteParameter("@userName",userName), 
                                                 new SQLiteParameter("@password", MD5Encrypt.Encrypt(password))   //更改为md5加密后的字符串
                                         };

            SqLiteDataBaseHelper sqLiteDataBaseHelper = new SqLiteDataBaseHelper();
            Object loginResult = sqLiteDataBaseHelper.ExecuteScalar(loginSql, parameters);
            //UserInfo loginUser = loginResult as UserInfo;
            if (loginResult != null)
            {
                message = "登陆成功！";
                GlobalInfo.CurrentUserName = userName; 

                return true;
            }
            else
            {
                message = "登陆失败！";
                return false;
            }

        }

        public bool ModifyPassword(UserInfo userInfo, out string message)
        {
            string modifyPasswordSql = "update User set password=@password where userName=@userName and password=@oldPassword";
            DbParameter[] parameters = new SQLiteParameter[]{ 
                                                 new SQLiteParameter("@userName",userInfo.UserName), 
                                                 new SQLiteParameter("@oldPassword",MD5Encrypt.Encrypt(userInfo.OldPassword)), 
                                                 new SQLiteParameter("@password", MD5Encrypt.Encrypt(userInfo.UserPassword))   //更改为md5加密后的字符串
                                         };

            SqLiteDataBaseHelper sqLiteDataBaseHelper = new SqLiteDataBaseHelper();
            int modifyResult = sqLiteDataBaseHelper.ExecuteNonQuery(modifyPasswordSql, parameters);

            if (modifyResult!=-1)
            {
                message = "修改成功！";
                return true;
            }
            else
            {
                message = "密码修改失败！";
                return false;
            }
        }

    }
}
