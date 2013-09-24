using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using Data.SystemSeting;

namespace BLL.SystemSeting
{
   public class GonGaoBLL:BaseBll
    {
       GonGaoDat dat = new GonGaoDat();
        #region 公告类型
       /// <summary>
       /// 添加公告类型
       /// </summary>
       /// <param name="id"></param>
       /// <param name="name"></param>
       /// <returns></returns>
       public string AddType(string id,string name)
       {
           try
           {
               string sql = "insert into GgType (GgTypeID,TypeName) values('" + id + "','" + name + "')";
               setSql(sql);
               return "ok";
           }
           catch (Exception)
           {

               return "添加失败！";
           }
       }
       /// <summary>
       /// 修改公告类型
       /// </summary>
       /// <param name="id"></param>
       /// <param name="name"></param>
       /// <returns></returns>
       public string EidType(string id,string name) 
       {
            string sql = "update GgType set TypeName='"+name+"' where GgTypeID='"+id+"'";
            setSql(sql);
            return "ok";
       }
       /// <summary>
       /// 删除公告类型
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int DeleType(string id) 
       {
           string sql = "delete from GgType where GgTypeID='"+id+"'";
          return setSql(sql);
       }

       /// <summary>
       /// 查询公告类型
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public List<GgType> GetGgType(string id,string name) 
       {
           string sql = "select * from GgType";
           if (id!="")
           {
               sql += " where GgTypeID='"+id+"'";
           }
           if (name!="")
           {
               sql += " where TypeName='"+name+"'";
           }
           return dat.GetGgType(sql);
       }
        #endregion

        #region 公告
       /// <summary>
       /// 按条件查询公告
       /// </summary>
       /// <param name="typeid"></param>
       /// <param name="tile"></param>
       /// <param name="userid"></param>
       /// <param name="beg"></param>
       /// <param name="end"></param>
       /// <returns></returns>
       public List<Gg> GetGg(string typeid,string tile,string userid, DateTime? beg,DateTime? end) 
       {
           string sql = "select * from Gg where Tile like '%"+tile+"%'";

           if (typeid!=""&&typeid!=null)
           {
               sql += " and GgTypeID='"+typeid+"'";
           }
           if (userid!=""&&userid!=null)
           {
               sql += " and RenYuanId='"+userid+"'";
           }
           if (beg!=null)
           {
               string begg=beg.Value.ToString("yyyy-MM-dd");
               sql += " and CreatTime>='"+begg+"'";
           }
           if (end!=null)
           {
               string endd = end.Value.ToString("yyyy-MM-dd");
               sql += " and CreatTime<='"+endd+"'";
           }
           sql += "order by CreatTime desc";
           return dat.GetGg(sql);
       }

       /// <summary>
       /// 添加公告信息
       /// </summary>
       /// <param name="go"></param>
       /// <returns></returns>
       public int AddGg(Gg go) 
       {
           string time=go.CreatTime.Value.ToString("yyyy-MM-dd");
           string sql = "insert into Gg(GgId,Tile,Contents,GgTypeID,RenYuanId,CreatTime)values('"+go.GgId+"','"+go.Tile+"','"+go.Contents+"','"+go.GgTypeID+"','"+go.RenYuanId+"','"+time+"')";
           return setSql(sql);
       }

       /// <summary>
       /// 修改公告信息
       /// </summary>
       /// <param name="go"></param>
       /// <returns></returns>
       public int EidGg(Gg go) 
       {
           string sql = "update Gg set Tile='"+go.Tile+"',Contents='"+go.Contents+"',GgTypeID='"+go.GgTypeID+"' where GgId='"+go.GgId+"'";
           return setSql(sql);
       }

       /// <summary>
       /// 删除
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int Dele(string id) 
       {
           string sql = "delete from Gg where GgId='"+id+"'";
           return setSql(sql);
       }

       /// <summary>
       /// 跟据Id查找公告
       /// </summary>
       /// <param name="Ggid"></param>
       /// <returns></returns>
       public Gg GetGgByid(string Ggid) 
       {
           string sql = "select * from Gg where GgId='" + Ggid + "'";
           return dat.GetGgAll(sql)[0];
       }
       /// <summary>
       /// 首页最新5条公告
       /// </summary>
       /// <returns></returns>
       public List<Gg> GetGgTop5() 
       {
           string sql = "select top 5 * from Gg where GgTypeID=(select GgTypeID from GgType where TypeName='新闻公告') Order by CreatTime DESC";
           return dat.GetGgAll(sql);
       }
        #endregion
    }
}
