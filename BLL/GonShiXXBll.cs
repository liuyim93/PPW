using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using Data;

namespace BLL
{
    //公司信息
   public class GonShiXXBll
    {
       GongShiXX xxDat = new GongShiXX();
       /// <summary>
       /// 查询公司信息
       /// </summary>
       /// <returns></returns>
       public GsXX GetXX() 
       {
           string sql = "select * from GsXX";
           return xxDat.GetGsxx(sql);
       }

       //修改公司信息
       public int Eid(GsXX xx) 
       {
           string sql = "update GsXX  set SgName='"+xx.SgName+"',Mode='"+xx.Mode+"',Dz='"+xx.Dz+"',WZ='"+xx.WZ+"',banQuanSY='"+xx.banQuanSY+"' where GsXXId='"+xx.GsXXId+"'";
           return xxDat.SetSql(sql);
       }

       /// <summary>
       /// 添加
       /// </summary>
       /// <param name="xx"></param>
       /// <returns></returns>
       public int Add(GsXX xx) 
       {
           string sql = "insert into GsXX (GsXXId,SgName,Mode,Dz,WZ,banQuanSY) values('" + xx.GsXXId + "','" + xx.SgName + "','" + xx.Mode + "','" + xx.Dz + "','" + xx.WZ + "','" + xx.banQuanSY + "')";
           return xxDat.SetSql(sql);
       }
    }
}
