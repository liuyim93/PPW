using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Data
{
    //会员
   public class HuiYuanDat
    {
       SQLHelper BLLdat = new SQLHelper();

       public List<HuiYuan> GetHuiYuan(string sql) 
       {
           DataSet dat = BLLdat.GetDataSet(sql);
           List<HuiYuan> list = new List<HuiYuan>();
           if (dat.Tables.Count > 0) 
           {
               foreach (DataRow item in dat.Tables[0].Rows)
               {
                   HuiYuan hy = new HuiYuan();
                   hy.HuiYuanID = item["HuiYuanID"].ToString();
                   hy.HuiYuanName = item["HuiYuanName"].ToString();
                   hy.MM = item["MM"].ToString();
                   hy.email = item["email"].ToString();
                   hy.prName = item["prName"].ToString();
                   hy.sex = item["sex"].ToString();
                   hy.sfz = item["sfz"].ToString();
                   hy.sjh = item["sjh"].ToString();
                   hy.PaiDian = (int)item["PaiDian"];
                   hy.JiFen = (int)item["JiFen"];
                   hy.DJ = item["DJ"].ToString();
                   hy.CreatTime = item["CreatTime"] as DateTime?;
                   hy.LoginTime = item["LoginTime"] == DBNull.Value ? null : item["LoginTime"] as DateTime?;
                   hy.FreePoint = item["FreePoint"] == DBNull.Value ? 0 : Convert.ToInt32(item["FreePoint"]);
                   hy.ContinueLogins = item["ContinueLogins"] == DBNull.Value ? 0 : Convert.ToInt32(item["ContinueLogins"]);
                   hy.Adress=item["Adress"]==DBNull.Value?"":item["Adress"].ToString();
                   hy.IsEmailVerify = item["IsEmailVerify"] == DBNull.Value ? 0 : Convert.ToInt32(item["IsEmailVerify"]);
                   hy.IsPhoneVerify = item["IsPhoneVerify"] == DBNull.Value ? 0 : Convert.ToInt32(item["IsPhoneVerify"]);
                   hy.Points = Convert.ToInt32(item["Points"]);
                   list.Add(hy);
               }
           }
           return list;
       }

       /// <summary>
       /// 跟据用户名和密码查找会员信息
       /// </summary>
       /// <param name="yhName"></param>
       /// <param name="mm"></param>
       /// <returns></returns>
       public HuiYuan GetHuiYuanBySelect(string yhName,string mm) 
       {
           string sql = "select * from HuiYuan where HuiYuanName=@name and MM=@mm";
           SqlParameter[] spr = { new SqlParameter("@name", yhName), new SqlParameter("@mm", mm) };
           DataSet dat=BLLdat.ExecuteDataSet(CommandType.Text, sql, spr);
           HuiYuan hy = null;
           if (dat.Tables[0].Rows.Count>0&&dat!=null)
           {
               hy = new HuiYuan();
               DataRow row=dat.Tables[0].Rows[0];
               hy.HuiYuanID = row["HuiYuanID"].ToString();
               hy.HuiYuanName = row["HuiYuanName"].ToString();
               hy.MM = row["MM"].ToString();
               hy.email = row["email"].ToString();
               hy.prName = row["prName"].ToString();
               hy.sex = row["sex"].ToString();
               hy.sfz = row["sfz"].ToString();
               hy.sjh = row["sjh"].ToString();
               hy.PaiDian = row["PaiDian"] as int?;
               hy.JiFen = row["JiFen"] as int?;
               hy.DJ = row["DJ"].ToString();
               hy.CreatTime = row["CreatTime"] as DateTime?;
               hy.LoginTime = row["LoginTime"] == DBNull.Value ? null : row["LoginTime"] as DateTime?;
           }
           return hy;
       }

       /// <summary>
       /// 修改会员拍点和返点
       /// </summary>
       /// <param name="hy"></param>
       /// <returns></returns>
       public int UpdatePoint(HuiYuan hy) 
       {
           string sql = "update HuiYuan set PaiDian=PaiDian+@PaiDian,FreePoint=FreePoint+@FreePoint where HuiYuanID=@HuiYuanID";
           try
           {
               return BLLdat.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@HuiYuanID",hy.HuiYuanID),
                   new SqlParameter("@PaiDian",hy.PaiDian),
                   new SqlParameter("@FreePoint",hy.FreePoint));
           }
           catch (Exception)
           {
               
               throw;
           }
       }

       /// <summary>
       /// 会员注册
       /// </summary>
       /// <param name="hy"></param>
       /// <returns></returns>
       public int AddHuiYuan(HuiYuan hy) 
       {
           string sql = "insert into HuiYuan(HuiYuanName,MM,email,DJ)values(@HuiYuanName,@MM,@email,@DJ)";
           try
           {
               return BLLdat.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@HuiYuanName",hy.HuiYuanName),
                   new SqlParameter("@MM",hy.MM),
                   new SqlParameter("@email",hy.email),
                   new SqlParameter("@DJ",hy.DJ));
           }
           catch (Exception)
           {
               
               throw;
           }
       }

       /// <summary>
       /// 判断用户名是否已经注册
       /// </summary>
       /// <param name="username"></param>
       /// <returns></returns>
       public object IsUserNameAvailable( string username) 
       {
           string sql = "select * from HuiYuan where HuiYuanName=@HuiYuanName";
           try
           {
               object obj = BLLdat.ExecuteScalar(CommandType.Text,sql,new SqlParameter("@HuiYuanName",username));
               return obj;
           }
           catch (Exception)
           {
               
               throw;
           }
       }

       /// <summary>
       /// 判断邮箱是否被使用
       /// </summary>
       /// <param name="email"></param>
       /// <returns></returns>
       public object IsEmailAvailable(string email) 
       {
           string sql = "select * from HuiYuan where email=@email";
           try
           {
               object obj = BLLdat.ExecuteScalar(CommandType.Text,sql,new SqlParameter("@email",email));
               return obj;
           }
           catch (Exception)
           {
               
               throw;
           }
       }

       /// <summary>
       /// 修改会员个人资料
       /// </summary>
       /// <param name="hy"></param>
       /// <returns></returns>
       public int UpdateUserInfo(HuiYuan hy) 
       {
           string sql = "update HuiYuan set prName=@prName,sex=@sex,sfz=@sfz,sjh=@sjh,Adress=@Adress where HuiYuanID=@HuiYuanID";
           return BLLdat.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@prName",hy.prName),
               new SqlParameter("@Sex",hy.sex),
               new SqlParameter("@sfz",hy.sfz),
               new SqlParameter("@sjh",hy.sjh),
               new SqlParameter("@Adress",hy.Adress),
               new SqlParameter("@HuiYuanID",hy.HuiYuanID));
       }

       /// <summary>
       /// 修改会员密码
       /// </summary>
       /// <param name="hyId"></param>
       /// <param name="pwd"></param>
       /// <returns></returns>
       public int UpdatePwd(string hyId,string pwd) 
       {
           string sql = "update HuiYuan set MM=@MM where HuiYuanID=@HuiYuanID";
           return BLLdat.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@HuiYuanID",hyId),
               new SqlParameter("@MM",pwd));
       }

       /// <summary>
       /// 修改会员可用积分
       /// </summary>
       /// <param name="hyId"></param>
       /// <param name="points"></param>
       /// <returns></returns>
       public int UpdatePoints(string hyId,int points) 
       {
           string sql = "update HuiYuan set Points=Points+@Points where HuiYuanID=@HuiYuanID";
           return BLLdat.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@HuiYuanID",hyId),
               new SqlParameter("@Points",points));
       }
    }
}
