using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using DAL;
using System.Data;
namespace Data.SystemSeting
{
   public class GonGaoDat
    {
       SQLHelper BLLdat = new SQLHelper();

        #region 公告类型
       public List<GgType> GetGgType(string sql)
       {
           List<GgType> list = new List<GgType>();
           DataSet dat = BLLdat.GetDataSet(sql);
           if (dat.Tables.Count>0)
           {
               foreach (DataRow item in dat.Tables[0].Rows)
               {
                   GgType type = new GgType();
                   type.GgTypeID = item["GgTypeID"].ToString();
                   type.TypeName = item["TypeName"].ToString();
                   list.Add(type);
               }
           }
           return list;
       }
        #endregion

        #region 公告
       public List<Gg> GetGg(string sql) 
       {
           List<Gg> list = new List<Gg>();
           DataSet dat= BLLdat.GetDataSet(sql);
           if (dat.Tables.Count>0)
           {
               foreach (DataRow item in dat.Tables[0].Rows)
               {
                   Gg gg = new Gg();
                   gg.GgId = item["GgId"].ToString();
                   gg.Tile = item["Tile"].ToString();
                   gg.GgTypeID = item["GgTypeID"].ToString();
                   gg.RenYuanId = item["RenYuanId"].ToString();
                   gg.CreatTime = item["CreatTime"] as DateTime?;
                   list.Add(gg);
               }

           }
           return list;
       }
       /// <summary>
       /// 查询公告所有字段
       /// </summary>
       /// <param name="sql"></param>
       /// <returns></returns>
       public List<Gg> GetGgAll(string sql)
       {
           List<Gg> list = new List<Gg>();
           DataSet dat = BLLdat.GetDataSet(sql);
           if (dat.Tables.Count > 0)
           {
               foreach (DataRow item in dat.Tables[0].Rows)
               {
                   Gg gg = new Gg();
                   gg.GgId = item["GgId"].ToString();
                   gg.Tile = item["Tile"].ToString();
                   gg.GgTypeID = item["GgTypeID"].ToString();
                   gg.RenYuanId = item["RenYuanId"].ToString();
                   gg.Contents = item["Contents"].ToString();
                   gg.CreatTime = item["CreatTime"] as DateTime?;
                   list.Add(gg);
               }

           }
           return list;
       }
        #endregion
    }
}
