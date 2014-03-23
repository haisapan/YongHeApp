using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Data.SQLite;
using BaseLibrary;
using DAL;
using Model;

//Please add references
namespace BAL
{
	/// <summary>
	/// 数据访问类:Event
	/// </summary>
    public class EventManager : SingleTonHelper<EventManager>
	{
	    public Action OnEventListChanged { get; set; }

	    public EventManager()
		{}
		#region  BasicMethod
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(EventInfo model)
		{
		    if (model==null)
		    {
		        return -1;
		    }
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Event(");
			strSql.Append("event)");
			strSql.Append(" values (");
			strSql.Append("@event)");
			strSql.Append(";select LAST_INSERT_ROWID()");

			SQLiteParameter[] parameters = {
					new SQLiteParameter("@event", DbType.String)};
			parameters[0].Value = model.Event;

            SqLiteDataBaseHelper sqLiteDataBaseHelper=new SqLiteDataBaseHelper();


            //int result=sqLiteDataBaseHelper.ExecuteNonQuery(strSql.ToString(), parameters);
            //return result == 1;

            object result = sqLiteDataBaseHelper.ExecuteScalar(strSql.ToString(), parameters);
            return Convert.ToInt32(result);

		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(EventInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Event set ");
			strSql.Append("event=@event");
			strSql.Append(" where id=@id");
            DbParameter[] parameters = new SQLiteParameter[]
		                                   {
		                                       new SQLiteParameter("@id", model.Id),
		                                       new SQLiteParameter("@event", model.Event),
		                                   };


            //SQLiteParameter[] parameters = {
            //        new SQLiteParameter("@event", DbType.String)};
            //parameters[0].Value = model.Event;

			 SqLiteDataBaseHelper sqLiteDataBaseHelper=new SqLiteDataBaseHelper();
		    int result=sqLiteDataBaseHelper.ExecuteNonQuery(strSql.ToString(), parameters);
			return result == 1;
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Event ");
			strSql.Append(" where id=@id");

			SQLiteParameter[] parameters = {
					new SQLiteParameter("@id", DbType.Int32,4)
			};
			parameters[0].Value = id;

			 SqLiteDataBaseHelper sqLiteDataBaseHelper=new SqLiteDataBaseHelper();
		    int result=sqLiteDataBaseHelper.ExecuteNonQuery(strSql.ToString(), parameters);
			return result == 1;
		}
	

		

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<EventInfo> GetList()
		{
            List<EventInfo> eventList = new List<EventInfo>();
            string sql = "select * from Event";

            SqLiteDataBaseHelper sqLiteDataBaseHelper = new SqLiteDataBaseHelper();
            var reader = sqLiteDataBaseHelper.ExecuteReader(sql, null);
            while (reader.Read())
            {
                EventInfo eventInfo=new EventInfo();
                eventInfo.Id = Convert.ToInt32(reader["id"]);
                eventInfo.Event=reader["event"].ToString();

                eventList.Add(eventInfo);
            }
            return eventList;
			
		}

		

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}


