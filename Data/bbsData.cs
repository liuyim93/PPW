using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using DAL;
using System.Data;

namespace Data
{
  public class bbsData
    {
      SQLHelper BLLdat = new SQLHelper();

      /// <summary>
      /// 跟据Sql语句查找贴子
      /// </summary>
      /// <param name="sql"></param>
      /// <returns></returns>
      public List<Tz> GetTz(string sql) 
      {
          List<Tz> list = new List<Tz>();
           DataSet dat = BLLdat.GetDataSet(sql);
           if (dat.Tables.Count > 0)
           {
               foreach (DataRow item in dat.Tables[0].Rows)
               {
                   Tz tz = new Tz();
                   tz.TzID = item["TzID"].ToString();
                   tz.Tile = item["Tile"].ToString();
                   tz.Creatr = item["Creatr"].ToString();
                   tz.HuiYuanID = item["HuiYuanID"].ToString();
                   tz.Ip = item["Ip"].ToString();
                   tz.CreateTime = item["CreateTime"] as DateTime?;
                   list.Add(tz);
               }
           }
          return list;
      }

      /// <summary>
      /// 跟据Sql语句查找跟贴信息
      /// </summary>
      /// <param name="sql"></param>
      /// <returns></returns>
      public List<GtTz> GetGeTz(string sql) 
      {
          DataSet dat = BLLdat.GetDataSet(sql);
          List<GtTz> list = new List<GtTz>();
          if (dat.Tables.Count>0)
          {
              foreach (DataRow item in dat.Tables[0].Rows)
              {
                  GtTz tz = new GtTz();
                  tz.TzID = item["TzID"].ToString();
                  tz.GtTzID = item["GtTzID"].ToString();
                  tz.HuiYuanID = item["HuiYuanID"].ToString();
                  tz.Ip = item["Ip"].ToString();
                  tz.Contents = item["Contents"].ToString();
                  tz.CreateTime = item["CreateTime"] as DateTime?;
                  list.Add(tz);
              }
          }
          return list;
      }


      /// <summary>
      /// 跟据SQL语句查询跟贴下的所有子贴
      /// </summary>
      /// <param name="sql"></param>
      /// <returns></returns>
      public List<HfTz> GetHfTz(string sql) 
      {
         DataSet dat = BLLdat.GetDataSet(sql);
          List<HfTz> list = new List<HfTz>();
          if (dat.Tables.Count > 0)
          {
              foreach (DataRow item in dat.Tables[0].Rows)
              {
                  HfTz tz = new HfTz();
                  tz.HfTzId = item["HfTzId"].ToString();
                  tz.GtTzID = item["GtTzID"].ToString();
                  tz.HuiYuanID = item["HuiYuanID"].ToString();
                  tz.Ip = item["Ip"].ToString();
                  tz.creats = item["creats"].ToString();
                  tz.CreateTime = item["CreateTime"] as DateTime?;
                  list.Add(tz);
              }
          }
          return list;
      }

      /// <summary>
      /// 执行增删改操作
      /// </summary>
      /// <param name="sql"></param>
      /// <returns></returns>
      public int ReSetSql(string sql) 
      {
          return BLLdat.RunSQL(sql);
      }
    }
}
