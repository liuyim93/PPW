using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data.SqlClient;
using Model.Entities;
using System.Data;
namespace Data
{
   public class UserData
    {
      /*
       public object GetAll() 
       {
           string sql = "";
           return null;
       }

       /// <summary>
       /// 跟据用户ID 查找人员
       /// </summary>
       /// <returns></returns>
       public object GetByYhongHuID(string UserID) 
       {
           YongHu obj = new YongHu();
           SQLHelper BLLdat = new SQLHelper();
           string sql = "select * from RenYuan where RenYuanId=(select RenYuanId from YongHu where YongHuId="+UserID+")";
           DataSet ry=  BLLdat.GetDataSet(sql);
           //-------------人员-------------------------------------------------
           obj.RenYuan.RenYuanId = ry.Tables[0].Rows[0]["RenYuanId"].ToString();
           obj.RenYuan.DepartmentID = ry.Tables[0].Rows[0]["DepartmentID"]==null? null:ry.Tables[0].Rows[0]["DepartmentID"].ToString();
           obj.RenYuan.PersonName = ry.Tables[0].Rows[0]["PersonName"].ToString();
           obj.RenYuan.Sex = ry.Tables[0].Rows[0]["Sex"] == null ? null : ry.Tables[0].Rows[0]["Sex"].ToString();
           obj.RenYuan.ZhiWei = ry.Tables[0].Rows[0]["ZhiWei"] == null ? null : ry.Tables[0].Rows[0]["ZhiWei"].ToString();
           obj.RenYuan.GongZhi = ry.Tables[0].Rows[0]["GongZhi"] == null ?null:ry.Tables[0].Rows[0]["GongZhi"] as int?;
           obj.RenYuan.SFZ = ry.Tables[0].Rows[0]["SFZ"] == null ? null : ry.Tables[0].Rows[0]["SFZ"].ToString();
           obj.RenYuan.Email = ry.Tables[0].Rows[0]["Email"] == null ? null : ry.Tables[0].Rows[0]["Email"].ToString();
           obj.RenYuan.Mobile = ry.Tables[0].Rows[0]["Mobile"] == null ? null : ry.Tables[0].Rows[0]["Mobile"].ToString();
           obj.RenYuan.Tel = ry.Tables[0].Rows[0]["Tel"] == null ? null : ry.Tables[0].Rows[0]["Tel"].ToString();
           obj.RenYuan.QQ = ry.Tables[0].Rows[0]["QQ"] == null ? null : ry.Tables[0].Rows[0]["QQ"].ToString();
           obj.RenYuan.Remark = ry.Tables[0].Rows[0]["Remark"] == null ? null : ry.Tables[0].Rows[0]["Remark"].ToString();
           obj.RenYuan.Status = (int)ry.Tables[0].Rows[0]["Status"];
        
           //-----------------------人员----------------------------
            
           return null;
       }*/
    }
}
