using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using Data;

namespace BLL.SystemSeting
{
  public class bbsBll
    {
       bbsData dat = new bbsData();
        #region 主贴

      /// <summary>
      /// 按条件查找主贴
      /// </summary>
      /// <returns></returns>
      public List<Tz> GetTz(string id,string tile, DateTime? beg,DateTime?end) 
      {
          string sql = "select * from Tz where Tile like '%"+tile+"%'";
          if (beg!=null)
          {
              string begs = beg.Value.ToString("yyyy-MM-dd");
              sql += " and CreateTime>='"+begs+"'";
          }
          if (end!=null)
          {
              string ends=end.Value.ToString("yyyy-MM-dd");
              sql += " and CreateTime<='"+end+"'";
          }
          if (id!=""&&id!=null)
          {
              sql += " and TzID='"+id+"'";
          }
          sql += " order by CreateTime desc";
          return dat.GetTz(sql);
      }

      /// <summary>
      /// 删除主贴
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      public int DeleTz(string id) 
      {
          string sql = "delete from Tz where TzID='"+id+"'";
          return dat.ReSetSql(sql);
      }

        #endregion

        #region 跟贴

      /// <summary>
      /// 查找跟贴信息
      /// </summary>
      /// <param name="id">跟贴ID</param>
      /// <param name="Ztid">主贴ID</param>
      /// <returns></returns>
      public List<GtTz> GetGTtz(string id,string Ztid) 
      {
          string sql = "select * from  GtTz";
          if (id!=null&&id!="")
          {
              sql += " where GtTzID='"+id+"'";
          }
          if (Ztid!=""&&Ztid!=null)
	      {
              sql += " where TzID='"+Ztid+"'";
	      }
          return dat.GetGeTz(sql);
      }

      /// <summary>
      /// 删除跟贴信息
      /// </summary>
      /// <param name="id">跟贴ID</param>
      /// <param name="ZtId">主贴ID</param>
      /// <returns></returns>
      public int DeleGetZ(string id,string ZtId) 
      {
          string sql = "";
          if (id!="")
          {
              sql = "delete from GtTz where GtTzID='"+id+"'";
          }
          else
          {
              sql = "delete from GtTz where TzID='"+ZtId+"'";
          }
          return dat.ReSetSql(sql);
      }

        #endregion

        #region 跟贴下子贴

      /// <summary>
      /// 查询跟贴下的子贴
      /// </summary>
      /// <param name="id"></param>
      /// <param name="hyid"></param>
      /// <returns></returns>
      public List<HfTz> GetHfTz(string id,string hyid) 
      {
          string sql = "";
          if (id!="")
          {
              sql += "select * from HfTz where GtTzID='"+id+"'";
          }
          if (hyid!="")
          {
              sql += "select * from HfTz where HuiYuanID='"+hyid+"'";
          }
         return dat.GetHfTz(sql);
      }

      /// <summary>
      /// 删除跟贴下的贴子
      /// </summary>
      /// <param name="id">子贴ID</param>
      /// <param name="GTid">跟贴ID</param>
      /// <returns></returns>
      public int DeleHFT(string id,string GTid) 
      {
          string sql = "";
          if (id!=""&&id!=null)
          {
              sql = "delete from HfTz where HfTzId='"+id+"'";
          }
          else
          {
              sql = "delete from HfTz where GtTzID='"+GTid+"'";
          }
          return dat.ReSetSql(sql);
      }

        #endregion
    }
}
