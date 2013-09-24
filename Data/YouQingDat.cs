using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model.Entities;
using System.Data;
namespace Data
{
    //友情连接
   public class YouQingDat
    {
       SQLHelper dbdat = new SQLHelper();
        /// <summary>
        /// 执行SQL语句（ADO）
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int setSql(string sql)
        {
           
            return dbdat.RunSQL(sql);
        }

       /// <summary>
       /// 跟据SQL语句查找友情连接
       /// </summary>
       /// <param name="sql"></param>
       /// <returns></returns>
        public List<YouQinLianJi> GetYQ(string sql) 
        {
            List<YouQinLianJi> list = new List<YouQinLianJi>();
            DataSet datset = dbdat.GetDataSet(sql);
            if (datset.Tables.Count>0)
            {
                foreach (DataRow item in datset.Tables[0].Rows)
                {
                    YouQinLianJi yq = new YouQinLianJi();
                    yq.YouQinLianJiId = item["YouQinLianJiId"].ToString();
                    yq.GsName = item["GsName"].ToString();
                    yq.img = item["img"].ToString();
                    yq.Url = item["Url"].ToString();
                    yq.DispType = item["DispType"].ToString();
                    yq.xh = Convert.ToInt32(item["xh"]);
                    list.Add(yq);
                }
            }
            return list;
        }
    }
}
