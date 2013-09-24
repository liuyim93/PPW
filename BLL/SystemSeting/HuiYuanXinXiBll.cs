using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using Data;
using System.Data;
namespace BLL.SystemSeting
{
    //会员信息
  public class HuiYuanXinXiBll
    {
      HuiYuanDat dat = new HuiYuanDat();
      /// <summary>
      /// 跟据条件查找会员管
      /// </summary>
      /// <param name="HuiYuanName"></param>
      /// <param name="prName"></param>
      /// <param name="DJ"></param>
      /// <returns></returns>
      public List<HuiYuan> GetHuiYuan(string HuiYuanName,string prName, string DJ) 
      {
          string sql = "select * from HuiYuan where prName like '%"+prName+"%'";
          if (HuiYuanName!="")
          {
              sql += " and HuiYuanName='"+HuiYuanName+"'";
          }
          if (DJ!=""&&DJ!=null)
          {
              sql += " and DJ='"+DJ+"'";
          }
          return dat.GetHuiYuan(sql);
      }

      /// <summary>
      /// 跟据用户名，密码查找会员
      /// </summary>
      /// <param name="HuiYuanName"></param>
      /// <param name="MM"></param>
      /// <returns></returns>
      public HuiYuan GetHuiYuan(string HuiYuanName, string MM) 
      {
          return dat.GetHuiYuanBySelect(HuiYuanName,MM);
      }

      /// <summary>
      /// 跟据会员ID查找会员信息
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      public HuiYuan GetHuiYuan(string id)
      {
          string sql = "select * from HuiYuan where HuiYuanID='"+id+"'";
          List<HuiYuan> hys=dat.GetHuiYuan(sql);
          if (hys.Count>0)
          {
              return hys[0];
          }
          else
          {
              return null;
          }
      }

      /// <summary>
      /// 修改会员拍点和返点
      /// </summary>
      /// <param name="hy"></param>
      /// <returns></returns>
      public int UpdateHuiYuanPoint(HuiYuan hy) 
      {
          return dat.UpdatePoint(hy);
      }
      
      /// <summary>
      /// 会员注册
      /// </summary>
      /// <param name="hy"></param>
      /// <returns></returns>
      public int AddHuiYuan(HuiYuan hy) 
      {
          return dat.AddHuiYuan(hy);
      }
    }
}
