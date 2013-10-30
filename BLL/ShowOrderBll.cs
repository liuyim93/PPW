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
        /// 添加晒单图片
        /// </summary>
        /// <param name="showOrderId"></param>
        /// <param name="imgUrl"></param>
        /// <returns></returns>
        public int AddShowOrderImg(string showOrderId,string imgUrl) 
        {
            return showOrderDat.AddShowOrderImg(showOrderId,imgUrl);
        }

        /// <summary>
        /// 根据晒单ID查询晒单图片
        /// </summary>
        /// <param name="showOrderId"></param>
        /// <returns></returns>
        public List<ShowOrderImg> GetShowOrderImg(string showOrderId) 
        {
            string sql = "select * from ShowOrderImg where ShowOrderID='"+showOrderId+"'";
            return showOrderDat.GetShowOrderImg(sql);
        }
    }
}
