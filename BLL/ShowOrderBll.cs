using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using Model.Entities;

namespace BLL
{
    public class ShowOrderBll
    {

        ShowOrderData showOrderDat = new ShowOrderData();

        /// <summary>
        /// 添加晒单
        /// </summary>
        /// <param name="showOrder"></param>
        /// <returns></returns>
        public int AddShowOrder(ShowOrder showOrder) 
        {
            return showOrderDat.AddShowOrder(showOrder);
        }

        /// <summary>
        /// 根据订单ID查询晒单（不包括图片）
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<ShowOrder> GetShowOrderbyOrderId(string orderId) 
        {
            string sql = "select * from ShowOrder where OrderID='"+orderId+"'";
            return showOrderDat.GetShowOrder(sql);
        }

        /// <summary>
        /// 查询通过审核的晒单
        /// </summary>
        /// <param name="isCheck"></param>
        /// <returns></returns>
        public List<ShowOrder> GetShowOrderbyCheck(int isCheck) 
        {
            string sql = "select * from ShowOrder where IsCheck="+isCheck;
            return showOrderDat.GetShowOrder(sql);
        }

        /// <summary>
        /// 首页显示4条拍客晒图
        /// </summary>
        /// <returns></returns>
        public List<ShowOrder> GetShowOrder_Top4() 
        {
            string sql = "select top 4 * from ShowOrder where IsShow=1";
            return showOrderDat.GetShowOrder(sql);
        }

        /// <summary>
        /// 查询所有的晒单
        /// </summary>
        /// <returns></returns>
        public List<ShowOrder> GetAllShowOrder() 
        {
            string sql = "select * from ShowOrder";
            return showOrderDat.GetShowOrder(sql);
        }

        /// <summary>
        /// 根据晒单ID查询晒单信息
        /// </summary>
        /// <param name="showOrderId"></param>
        /// <returns></returns>
        public List<ShowOrder> GetShowOrder(string showOrderId) 
        {
            string sql = "select * from ShowOrder where ShowOrderID='"+showOrderId+"'";
            return showOrderDat.GetShowOrder(sql);
        }

        /// <summary>
        /// 管理员回复晒单
        /// </summary>
        /// <param name="showOrder"></param>
        /// <returns></returns>
        public int UpdateShowOrderbyAdmin(ShowOrder showOrder) 
        {
            return showOrderDat.UpdateShowOrderbyAdmin(showOrder);
        }

        /// <summary>
        /// 删除晒单
        /// </summary>
        /// <param name="showOrderId"></param>
        /// <returns></returns>
        public int DeleteShowOrder(string showOrderId) 
        {
            return showOrderDat.DeleteShowOrder(showOrderId);
        }

        /// <summary>
        /// 根据晒单标题、是否通过审核、是否已读查询晒单信息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="isCheck"></param>
        /// <param name="isRead"></param>
        /// <returns></returns>
        public List<ShowOrder> GetShowOrder(string title,string isCheck,string isRead) 
        {
            string sql = "select * from ShowOrder Where Title like '%"+title+"%'";
            if (isCheck != "")
            {
                sql += "and IsCheck=" + isCheck;
            }
            else 
            {
                if (isRead!="")
                {
                    sql += "and IsRead="+isRead;
                }
            }
            return showOrderDat.GetShowOrder(sql);
        }

        #region 晒单图片
        /// <summary>
        /// 添加晒单图片
        /// </summary>
        /// <param name="showOrderId"></param>
        /// <param name="imgUrl"></param>
        /// <returns></returns>
        public int AddShowOrderImg(string showOrderId, string imgUrl)
        {
            return showOrderDat.AddShowOrderImg(showOrderId, imgUrl);
        }

        /// <summary>
        /// 根据晒单ID查询晒单图片
        /// </summary>
        /// <param name="showOrderId"></param>
        /// <returns></returns>
        public List<ShowOrderImg> GetShowOrderImg(string showOrderId)
        {
            string sql = "select * from ShowOrderImg where ShowOrderID='" + showOrderId + "'";
            return showOrderDat.GetShowOrderImg(sql);
        }
        #endregion     
    }
}
