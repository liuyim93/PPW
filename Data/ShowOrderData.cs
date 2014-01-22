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
    public class ShowOrderData
    {
        SQLHelper sh = new SQLHelper();

        #region 晒单

        /// <summary>
        /// 添加晒单
        /// </summary>
        /// <param name="showOrder"></param>
        /// <returns></returns>
        public int AddShowOrder(ShowOrder showOrder) 
        {
            string sql = "insert into ShowOrder (OrderID,Title,Detail,LoadTime,IsCheck,IsRead,IsShow)values(@OrderID,@Title,@Detail,@LoadTime,@IsCheck,@IsRead,@IsShow)";
            return sh.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@OrderID",showOrder.OrderID),
                new SqlParameter("@Title",showOrder.Title),
                new SqlParameter("@Detail",showOrder.Detail),
                new SqlParameter("@LoadTime",showOrder.LoadTime),
                new SqlParameter("@IsCheck",showOrder.IsCheck),
                new SqlParameter("@IsRead",showOrder.IsRead),
                new SqlParameter("@IsShow",showOrder.IsShow));
        }

        /// <summary>
        /// 查询晒单
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<ShowOrder> GetShowOrder(string sql) 
        {
            List<ShowOrder> list = new List<ShowOrder>();
            DataSet ds = sh.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count>0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ShowOrder showOrder = new ShowOrder();
                    showOrder.ShowOrderID=row["ShowOrderID"].ToString();
                    showOrder.OrderID=row["OrderID"].ToString();
                    showOrder.Title=row["Title"].ToString();
                    showOrder.Detail=row["Detail"].ToString();
                    showOrder.LoadTime = Convert.ToDateTime(row["LoadTime"]);
                    showOrder.Reply=row["Reply"]==DBNull.Value?"":row["Reply"].ToString();
                    showOrder.Points=row["Points"]==DBNull.Value?0:Convert.ToInt32(row["Points"]);
                    showOrder.IsCheck = Convert.ToInt32(row["IsCheck"]);
                    showOrder.IsRead = Convert.ToInt32(row["IsRead"]);
                    showOrder.IsShow = Convert.ToInt32(row["IsShow"]);
                    list.Add(showOrder);
                }
            }
            return list;
        }

        /// <summary>
        /// 管理员回复晒单
        /// </summary>
        /// <param name="showOrder"></param>
        /// <returns></returns>
        public int UpdateShowOrderbyAdmin(ShowOrder showOrder) 
        {
            string sql = "update ShowOrder set IsCheck=@IsCheck,IsShow=@IsShow,IsRead=@IsRead,Points=@Points,Reply=@Reply where ShowOrderID=@ShowOrderID";
            return sh.ExecuteNonQuery(null, CommandType.Text, sql, new SqlParameter("@ShowOrderID", showOrder.ShowOrderID),
                new SqlParameter("@IsCheck", showOrder.IsCheck),
                new SqlParameter("@IsShow", showOrder.IsShow),
                new SqlParameter("@IsRead", showOrder.IsRead),
                new SqlParameter("@Points", showOrder.Points),
                new SqlParameter("@Reply", showOrder.Reply));
        }

        /// <summary>
        /// 删除晒单
        /// </summary>
        /// <param name="showOrderId"></param>
        /// <returns></returns>
        public int DeleteShowOrder(string showOrderId) 
        {
            string sql = "delete from ShowOrder where ShowOrderID=@ShowOrderID";
            return sh.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@ShowOrderID",showOrderId));
        }

        /// <summary>
        /// 根据是否通过审核查询 晒订单
        /// </summary>
        /// <param name="isCheck">是否通过审核</param>
        /// <returns></returns>
        public DataTable getShowOrderbyCheck(int isCheck) {
            string sql = "select * from ShowOrder where IsCheck=" + isCheck;
            DataSet ds=sh.GetDataSet(sql);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else {
                return null;
            }
        }
        #endregion

        #region 晒单图片

        /// <summary>
        /// 添加晒单图片
        /// </summary>
        /// <param name="showOrderId"></param>
        /// <param name="imgUrl"></param>
        /// <returns></returns>
        public int AddShowOrderImg(string showOrderId,string imgUrl) 
        {
            string sql = "insert into ShowOrderImg (ShowOrderID,ImgUrl)values(@ShowOrderID,@ImgUrl)";
            return sh.ExecuteNonQuery(null,CommandType.Text,sql,new SqlParameter("@ImgUrl",imgUrl),
                new SqlParameter("@ShowOrderID",showOrderId));
        }

        /// <summary>
        /// 查询晒单图片
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<ShowOrderImg> GetShowOrderImg(string sql) 
        {
            List<ShowOrderImg> list = new List<ShowOrderImg>();
            DataSet ds = sh.GetDataSet(sql);
            if (ds.Tables[0].Rows.Count>0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
	            {
		            ShowOrderImg showOrderImg=new ShowOrderImg();
                    showOrderImg.ShowOrderImgID=row["ShowOrderImgID"].ToString();
                    showOrderImg.ShowOrderID=row["ShowOrderID"].ToString();
                    showOrderImg.ImgUrl=row["ImgUrl"].ToString();
                    list.Add(showOrderImg);
	            }
            }
            return list;
        }
        #endregion
    }
}
