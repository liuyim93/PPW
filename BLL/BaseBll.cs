using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Model.Entities;
using Tools;
using BLL.Home;
using System.Data;
using System.IO;
using System.Data.OleDb;
using DAL;

namespace BLL
{
    public class BaseBll
    {
        public BaseBll()
        {
            db = new PPWContext();
        }
        protected PPWContext db = null;
       
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        public void RunSQL(string sql)
        {
            db.Database.ExecuteSqlCommand(sql);
        }

        /// <summary>
        /// 执行SQL语句（ADO）
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int setSql(string sql)
        {
            SQLHelper dbdat = new SQLHelper(); 
            return dbdat.RunSQL(sql);
        }
    }
}
