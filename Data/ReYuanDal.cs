using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using System.Data;
using DAL;
namespace Data
{
   public class ReYuanDal
    {
       SQLHelper BLLdat = new SQLHelper();
        #region 人员
        /// <summary>
        /// 返回所有人员信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
       public List<RenYuan> GetRenYuanAll(string sql) 
       {
          
           SQLHelper BLLdat = new SQLHelper();
           DataSet ry = BLLdat.GetDataSet(sql);
           List<RenYuan> list = new List<RenYuan>();
           //-------------人员-------------------------------------------------
           if (ry.Tables.Count>0)
           {
               if (ry.Tables[0].Rows.Count>0)
               {
                   foreach (DataRow item in ry.Tables[0].Rows)
                   {
                       RenYuan obj = new RenYuan();
                       obj.RenYuanId = item["RenYuanId"].ToString();
                       obj.PersonName = item["PersonName"].ToString();
                       obj.Sex = item["Sex"] == null ? null : item["Sex"].ToString();
                       obj.ZhiWei = item["ZhiWei"] == null ? null : item["ZhiWei"].ToString();
                       obj.SFZ = item["SFZ"] == null ? null : item["SFZ"].ToString();
                       obj.Email = item["Email"] == null ? null : item["Email"].ToString();
                       obj.QQ = item["QQ"] == null ? null : item["QQ"].ToString();
                       obj.Remark = item["Remark"] == null ? null : item["Remark"].ToString();
                       obj.Mobile = item["Mobile"] == null ? null : item["Mobile"].ToString();
                       list.Add(obj);
                   }
               }
           }
          
           return list;
       }

       /// <summary>
       /// 修改状态
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int UpdatState(string sql) 
       {
           SQLHelper BLLdat = new SQLHelper();
           return  BLLdat.RunSQL(sql);
       }
        #endregion
    }
}
