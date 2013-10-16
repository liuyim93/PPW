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
            string sql = "Insert into ShouHuoDZ values(@HuiYuanID,@ShouHuoName,@Mode,@DZ,@YouBian,@Remark,@CreateTime,@IsDefault,@IsSelect)";
            try
            {
                return sh.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@HuiYuanID",adress.HuiYuanID),
                    new SqlParameter("@ShouHuoName",adress.ShouHuoName),
                    new SqlParameter("@Mode",adress.Mode),
                    new SqlParameter("@DZ",adress.DZ),
                    new SqlParameter("@YouBian",adress.YouBian),
                    new SqlParameter("@Remark",adress.Remark),
                    new SqlParameter("@CreateTime",DateTime.Now),
                    new SqlParameter("@IsDefault",adress.IsDefault),
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
                ShouHuoDZ adress = new ShouHuoDZ();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    adress.ShouHuoDZID=row["ShouHuoDZID"].ToString();
                    adress.HuiYuanID=row["HuiYuanID"].ToString();
                    adress.ShouHuoName=row["ShouHuoName"].ToString();
                    adress.DZ = row["DZ"].ToString();
                    adress.Mode = row["Mode"].ToString();
                    adress.YouBian = row["YouBian"].ToString();
                    adress.Remark = row["Remark"].ToString();
                    adress.CreateTime=Convert.ToDateTime(row["CreateTime"]);
                    adress.IsDefault=Convert.ToInt32(row["IsDefault"]);
                    adress.IsSelect=Convert.ToInt32(row["IsSelect"]);
                    list.Add(adress);
                }               
            }
            return list;
        }
    }
}
