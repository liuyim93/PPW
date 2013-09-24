using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model.Entities;
using Data;
using Tools;
namespace BLL.Basei
{
   public class RenYuanBll:BaseBll
    {
       ReYuanDal rydal = new ReYuanDal();
       /// <summary>
       /// 跟据条件查找人员信息
       /// </summary>
       /// <param name="name"></param>
       /// <param name="bm"></param>
       /// <param name="sex"></param>
       /// <param name="ZhiWhi"></param>
       /// <returns></returns>
       public List<RenYuan> GetRenYuaSele(string name,string sex,string ZhiWhi,string RenYuanId)
        {
            string st =((int)Status.正常).ToString();
            string sql = "select * from RenYuan where PersonName  like '%" + name + "%'";
            if (sex!=null&&sex!="")
            {
                sql += "and Sex='"+sex+"'";
            }
            if (ZhiWhi!="")
            {
                sql += "and ZhiWei like '%" + ZhiWhi + "%'";
            }
            if (RenYuanId!=""&&RenYuanId!=null)
            {
                sql += "and RenYuanId='"+RenYuanId+"'";
            }
          return  rydal.GetRenYuanAll(sql);
        }

       /// <summary>
       /// 添加人员
       /// </summary>
       /// <param name="boj"></param>
       public void Add(RenYuan boj) 
       {
           db.RenYuans.Add(boj);
           db.SaveChanges();
       }

       public int Eid(RenYuan obj) 
       {
           string sql = "update RenYuan set PersonName='"+obj.PersonName+"',Sex='"+obj.Sex+"',ZhiWei='"+obj.ZhiWei+"',SFZ='"+obj.SFZ+"',Email='"+obj.Email+"',Mobile='"+obj.Mobile+"',QQ='"+obj.QQ+"',Remark='"+obj.Remark+"' where RenYuanId='"+obj.RenYuanId+"'";
           return rydal.UpdatState(sql);
       }
       /// <summary>
       /// 删除数据
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public string  Dele(string id) 
       {
           string sql = "delete from RenYuan where RenYuanId='" + id + "'";
           try
           {
                int i = rydal.UpdatState(sql);
                return "ok";
           }
           catch (Exception)
           {
               return "请检查相连的数据！";
           }
       }
    }
}
