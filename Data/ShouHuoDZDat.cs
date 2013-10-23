using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace Data
{
    public class ShouHuoDZDat
    {
        SQLHelper sh = new SQLHelper();

        /// <summary>
        /// 添加收货地址
        /// </summary>
        /// <param name="adress"></param>
        /// <returns></returns>
        public int AddShouHuoDZ(ShouHuoDZ adress) 
        {
            string sql = "Insert into ShouHuoDZ(HuiYuanID,ShouHuoName,Mode,DZ,YouBian,Remark,CreateTime,IsSelect) values(@HuiYuanID,@ShouHuoName,@Mode,@DZ,@YouBian,@Remark,@CreateTime,@IsSelect)";
            try
            {
                return sh.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@HuiYuanID",adress.HuiYuanID),
                    new SqlParameter("@ShouHuoName",adress.ShouHuoName),
                    new SqlParameter("@Mode",adress.Mode),
                    new SqlParameter("@DZ",adress.DZ),
                    new SqlParameter("@YouBian",adress.YouBian),
                    new SqlParameter("@Remark",adress.Remark),
                    new SqlParameter("@CreateTime",adress.CreateTime),
                    new SqlParameter("@IsSelect",adress.IsSelect));
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// 查询收货地址
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<ShouHuoDZ> GetShouHuoDZ(string sql) 
        {
            List<ShouHuoDZ> list = new List<ShouHuoDZ>();
            DataSet ds = sh.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count>0)
            {                
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ShouHuoDZ adress = new ShouHuoDZ();
                    adress.ShouHuoDZID=row["ShouHuoDZID"].ToString();
                    adress.HuiYuanID=row["HuiYuanID"].ToString();
                    adress.ShouHuoName=row["ShouHuoName"].ToString();
                    adress.DZ = row["DZ"].ToString();
                    adress.Mode = row["Mode"].ToString();
                    adress.YouBian = row["YouBian"].ToString();
                    adress.Remark = row["Remark"].ToString();
                    adress.CreateTime=Convert.ToDateTime(row["CreateTime"]);
                    adress.IsSelect=Convert.ToInt32(row["IsSelect"]);
                    list.Add(adress);
                }               
            }
            return list;
        }

        /// <summary>
        /// 根据会员ID修改收货地址选择状态
        /// </summary>
        /// <param name="hyId"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        public int UpdateStatusbyhyId(string hyId,int select) 
        {
            string sql = "update ShouHuoDZ set IsSelect=@IsSelect where HuiYuanID=@HuiYuanID";
            try
            {
                return sh.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@HuiYuanID",hyId),
                    new SqlParameter("@IsSelect",select));
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// 根据收货地址ID修改选择状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        public int UpdateStatus(string id,int select) 
        {
            string sql = "update ShouHuoDZ set IsSelect=@IsSelect where ShouHuoDZID=@ShouHuoDZID";
            return sh.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@ShouHuoDZID",id),
                new SqlParameter("@IsSelect",select));
        }

        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="adressId"></param>
        /// <returns></returns>
        public int DelShouHuoDZ(string adressId) 
        {
            string sql = "delete from ShouHuoDZ where ShouHuoDZID=@ShouHuoDZID";
            return sh.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@ShouHuoDZID",adressId));
        }

        /// <summary>
        /// 修改收货地址
        /// </summary>
        /// <param name="adress"></param>
        /// <returns></returns>
        public int UpdateShouHuoDZ(ShouHuoDZ adress) 
        {
            string sql = "update ShouHuoDZ set ShouHuoName=@ShouHuoName,Mode=@Mode,DZ=@DZ,YouBian=@YouBian,Remark=@Remark,IsSelect=@IsSelect where ShouHuoDZID=@ShouHuoDZID";
            return sh.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@ShouHuoName",adress.ShouHuoName),
                new SqlParameter("@Mode",adress.Mode),
                new SqlParameter("@DZ",adress.DZ),
                new SqlParameter("@Remark",adress.Remark),
                new SqlParameter("@IsSelect",adress.IsSelect),
                new SqlParameter("@ShouHuoDZID",adress.ShouHuoDZID),
                new SqlParameter("@YouBian",adress.YouBian));
        }
    }
}
