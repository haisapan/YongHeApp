using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class NoteManager : SingleTonHelper<NoteManager>
    {
        #region 查询
        public ObservableCollection<NoteModel> GetNodeListOfByDate(DateTime date)
        {
            return this.GetNodeListByCondition(date, date, null);
        }


        public List<string> GetEventList()
        {
            List<string> eventList = new List<string>();
            string sql = "select distinct event from YongHeNote";

            SqLiteDataBaseHelper sqLiteDataBaseHelper = new SqLiteDataBaseHelper();
            var reader = sqLiteDataBaseHelper.ExecuteReader(sql, null);
            while (reader.Read())
            {
                eventList.Add(reader["event"].ToString());
            }
            return eventList;
        }

        /// <summary>
        /// 根据条件进行查询
        /// </summary>
        /// <param name="startTime">startTime会自动换算成当天 00:00:00</param>
        /// <param name="endTime">endTime会自动换算成当天 23:59:59</param>
        /// <param name="eventString"></param>
        /// <returns></returns>
        public ObservableCollection<NoteModel> GetNodeListByCondition(DateTime startTime, DateTime endTime, string eventString)
        {
            //TODO UI验证失败时不进行查询
            //select * from yonghenote where noteTime>datetime('now','-2 hours','localtime') and noteTime<datetime('now','localtime')
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from yonghenote where 1=1 ");
            if (startTime != DateTime.MinValue && endTime >= startTime)
            {
                sb.Append(string.Format("and noteTime>datetime('{0}','localtime') and noteTime<datetime('{1}','localtime') ",
                                  startTime.ToString("yyyy-MM-dd 00:00:00"), endTime.ToString("yyyy-MM-dd 23:59:59")));
            }
            if (!string.IsNullOrEmpty(eventString))
            {
                sb.Append(string.Format("  and event=='{0}' ", eventString));
            }
            string sql = sb.ToString();

            SqLiteDataBaseHelper sqLiteDataBaseHelper = new SqLiteDataBaseHelper();
            DbDataReader dataReader = sqLiteDataBaseHelper.ExecuteReader(sql, null);

            ObservableCollection<NoteModel> noteList = new ObservableCollection<NoteModel>();
            while (dataReader.Read())
            {
                NoteModel note = new NoteModel();
                note.NoteId = dataReader["id"].ToString();
                note.Event = dataReader["event"].ToString();
                note.ChargePeopleName = dataReader["userId"].ToString();
                note.IsIncome = Convert.ToBoolean(dataReader["isIncome"]);
                note.Charge = float.Parse(dataReader["money"].ToString());
                note.Location = dataReader["location"].ToString();
                note.NoteDate = DateTime.Parse(dataReader["noteTime"].ToString());
                note.Note = dataReader["note"].ToString();

                noteList.Add(note);
            }
            return noteList;


        }

        #endregion

        #region 插入
        public bool InsertNode(NoteModel noteModel)
        {
            if (noteModel != null)
            {
                // insert into YongHeNote(event, isIncome, money, location, userId, noteTime, note) values(null,'Test',1,20,'ShanDong','20131001 12:00','Test Note')
                string sql = "insert into YongHeNote(id, event, isIncome, money, location, userId, noteTime, note) " +
                                  "values(@id, @event, @isIncome, @money, @location, @userId, @noteTime, @note)";
                DbParameter[] parameters = new SQLiteParameter[]{ 
                                                new SQLiteParameter("@id",noteModel.NoteId), 
                                                 new SQLiteParameter("@event",noteModel.Event), 
                                                 new SQLiteParameter("@isIncome",noteModel.IsIncome),
                                                 new SQLiteParameter("@money",noteModel.Charge), 
                                                 new SQLiteParameter("@location",noteModel.Location),
                                                 new SQLiteParameter("@userId",noteModel.ChargePeopleName),
                                                 new SQLiteParameter("@noteTime",noteModel.NoteDate), 
                                                 new SQLiteParameter("@note",noteModel.Note),
                                         };

                SqLiteDataBaseHelper sqLiteDataBaseHelper = new SqLiteDataBaseHelper();
                int loginResult = sqLiteDataBaseHelper.ExecuteNonQuery(sql, parameters);

                return loginResult == 1;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region 更新
        public bool UpdateNode(NoteModel noteModel)
        {
            // update Yonghenote set noteTime='2013/12/9' where id=1
            string sql = "update Yonghenote set event=@event, isIncome=@isIncome, money=@money, " +
                         "location=@location, userId=@userId,  noteTime=@noteTime, note=@note where id=@id";
            DbParameter[] parameters = new SQLiteParameter[]{ 
                                                 new SQLiteParameter("@event",noteModel.Event), 
                                                 new SQLiteParameter("@isIncome",noteModel.IsIncome),
                                                 new SQLiteParameter("@money",noteModel.Charge), 
                                                 new SQLiteParameter("@location",noteModel.Location),
                                                 new SQLiteParameter("@userId",noteModel.ChargePeopleName),
                                                 new SQLiteParameter("@noteTime",noteModel.NoteDate), 
                                                 new SQLiteParameter("@note",noteModel.Note),
                                                 new SQLiteParameter("@id",noteModel.NoteId),
                                         };

            SqLiteDataBaseHelper sqLiteDataBaseHelper = new SqLiteDataBaseHelper();
            int loginResult = sqLiteDataBaseHelper.ExecuteNonQuery(sql, parameters);

            return loginResult == 1;
        }
        #endregion

        #region 删除
        public bool DeleteNode(NoteModel noteModel)
        {
            string sql = "delete from YongHeNote where id=@id";
            DbParameter[] parameters = new SQLiteParameter[]{ 
                                                new SQLiteParameter("@id",noteModel.NoteId),
                                         };

            SqLiteDataBaseHelper sqLiteDataBaseHelper = new SqLiteDataBaseHelper();
            int loginResult = sqLiteDataBaseHelper.ExecuteNonQuery(sql, parameters);

            return loginResult == 1;
        }
        #endregion

    }
}
