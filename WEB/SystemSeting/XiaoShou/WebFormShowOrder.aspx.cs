using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BLL.Home;
using BLL.SystemSeting;
using Model.Entities;
using Ext.Net;

namespace WEB.SystemSeting.XiaoShou
{
    public partial class WebFormShowOrder : System.Web.UI.Page
    {
        ShowOrderBll showorderBll = new ShowOrderBll();
        ProductBLL proBll = new ProductBLL();
        DingDanBll orderBll = new DingDanBll();
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        AuctionBll actBll = new AuctionBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("../../Login.aspx");
            }
            else 
            {
                BindData();
            }
        }

        public void BindData() 
        {
            string title = txtTitle.Text;
            string isCheck;
            string isRead;
            if (cboxCheck.Value == null)
            {
                isCheck = "";
            }
            else 
            {
                isCheck = cboxCheck.Value.ToString();
            }
            if (cboxRead.Value == null)
            {
                isRead = "";
            }
            else 
            {
                isRead = cboxRead.Value.ToString();
            }
            List<ShowOrder> list = showorderBll.GetShowOrder(title,isCheck,isRead);
            storeShowOrder.DataSource = list.Select(x => new
            {
                ShowOrderID=x.ShowOrderID,
                OrderID=x.OrderID,
                ProductName=proBll.GetById(orderBll.GetDingDan(x.OrderID)[0].ProductID)[0].productName,
                ProductPrice = proBll.GetById(orderBll.GetDingDan(x.OrderID)[0].ProductID)[0].productPrice,
                HuiYuan=hyBll.GetHuiYuan(orderBll.GetDingDan(x.OrderID)[0].HuiYuanID).HuiYuanName,
                AuctionPrice=orderBll.GetDingDan(x.OrderID)[0].ProductPrice,
                Title=x.Title,
                Detail=x.Detail,
                Reply=x.Reply,
                IsCheck=x.IsCheck==1?"通过":"未通过",
                IsShow=x.IsShow==1?"是":"否",
                Points=x.Points,
                LoadTime=x.LoadTime.ToString()
            });
            storeShowOrder.DataBind();
        }

        public void ReplyShow(object sender,EventArgs e)
        {
            CheckboxSelectionModel check=gridShowOrder.SelectionModel[0]as CheckboxSelectionModel;
            if (check.SelectedRows.Count == 0)
            {
                X.Msg.Alert("提示", "请选择数据!").Show();
                return;
            }
            else 
            {
                string showOrderId = check.SelectedRow.RecordID;
                window_update.Title = "回复";
                List<ShowOrder> list = showorderBll.GetShowOrder(showOrderId);
                disProName.Text=proBll.GetById(orderBll.GetDingDan(list[0].OrderID)[0].ProductID)[0].productName;
                disProPrice.Text = proBll.GetById(orderBll.GetDingDan(list[0].OrderID)[0].ProductID)[0].productPrice.ToString();
                disHuiYuan.Text=hyBll.GetHuiYuan(orderBll.GetDingDan(list[0].OrderID)[0].HuiYuanID).HuiYuanName;
                disActPrice.Text=orderBll.GetDingDan(list[0].OrderID)[0].ProductPrice.ToString();
                disTitle.Text=list[0].Title;
                disDetail.Text=list[0].Detail;
                disReply.Text=list[0].Reply;
                disTime.Text=list[0].LoadTime.ToString();
                btnSave.Show();
                btnReturn.Show();
                window_update.Show();
                window_update.Reload();
            }            
        }

        public void UpdateShow(object sender,EventArgs e) 
        {
            CheckboxSelectionModel check = gridShowOrder.SelectionModel[0] as CheckboxSelectionModel;
            if (check.SelectedRows.Count == 0)
            {
                X.Msg.Alert("提示", "请选择数据!").Show();
                return;
            }
            else 
            {
                string showOrderId = check.SelectedRow.RecordID;
                window_update.Title = "修改回复";
                List<ShowOrder> list = showorderBll.GetShowOrder(showOrderId);
                disProName.Text = proBll.GetById(orderBll.GetDingDan(list[0].OrderID)[0].ProductID)[0].productName;
                disProPrice.Text = proBll.GetById(orderBll.GetDingDan(list[0].OrderID)[0].ProductID)[0].productPrice.ToString();
                disHuiYuan.Text = hyBll.GetHuiYuan(orderBll.GetDingDan(list[0].OrderID)[0].HuiYuanID).HuiYuanName;
                disActPrice.Text = orderBll.GetDingDan(list[0].OrderID)[0].ProductPrice.ToString();
                disTitle.Text = list[0].Title;
                disDetail.Text = list[0].Detail;
                disTime.Text = list[0].LoadTime.ToString();
                txtPoints.Text=list[0].Points.ToString();
                txtReply.Text=list[0].Reply;
                cboxPass.Value=list[0].IsCheck;
                cboxHome.Value=list[0].IsShow;
                window_update.Show();
                window_update.Reload();
            }
        }

        public void ShowDetail(object sender,EventArgs e) 
        {
             CheckboxSelectionModel check = gridShowOrder.SelectionModel[0] as CheckboxSelectionModel;
             if (check.SelectedRows.Count == 0)
             {
                 X.Msg.Alert("提示", "请选择数据!").Show();
                 return;
             }
             else 
             {
                 string showOrderId = check.SelectedRow.RecordID;
                 List<ShowOrder> list = showorderBll.GetShowOrder(showOrderId);
                 disProductName.Text=proBll.GetById(orderBll.GetDingDan(list[0].OrderID)[0].ProductID)[0].productName;
                 disProductPrice.Text = proBll.GetById(orderBll.GetDingDan(list[0].OrderID)[0].ProductID)[0].productPrice.ToString();
                 disMemberName.Text = hyBll.GetHuiYuan(orderBll.GetDingDan(list[0].OrderID)[0].HuiYuanID).HuiYuanName;
                 disAuctionPrice.Text = orderBll.GetDingDan(list[0].OrderID)[0].ProductPrice.ToString();
                 disShowOrderDetail.Text=list[0].Detail;
                 disShowOrderTitle.Text=list[0].Title;
                 disLoadTime.Text=list[0].LoadTime.ToString();
                 disPoints.Text=list[0].Points.ToString();
                 disReply.Text=list[0].Reply;
                 disIsCheck.Text=list[0].IsCheck==1?"已通过":"未通过";
                 disIsShow.Text=list[0].IsShow==1?"是":"否";
                 window_detail.Title = "详细信息";
                 window_detail.Show();
                 BindData();
             }
        }

        public void Save(object sender,EventArgs e) 
        {
            ShowOrder showOrder = new ShowOrder();
            CheckboxSelectionModel check=gridShowOrder.SelectionModel[0]as CheckboxSelectionModel;
            showOrder.ShowOrderID = check.SelectedRow.RecordID;
            showOrder.IsCheck = Convert.ToInt32(cboxPass.Value);
            showOrder.IsShow = Convert.ToInt32(cboxHome.Value);
            showOrder.IsRead = 1;
            showOrder.Points = Convert.ToInt32(txtPoints.Text.Trim());
            showOrder.Reply = txtReply.Text;
            showorderBll.UpdateShowOrderbyAdmin(showOrder);
            X.Msg.Alert("提示","保存成功！").Show();
            BindData();
            window_update.Hide();
        }

        protected void Del(object sender,DirectEventArgs e) 
        {
            CheckboxSelectionModel check = gridShowOrder.SelectionModel[0] as CheckboxSelectionModel;
            if (check.SelectedRows.Count == 0)
            {
                X.Msg.Alert("提示", "请选择要删除的数据！").Show();
                return;
            }
            else 
            {
                string showOrderId = check.SelectedRow.RecordID;
                showorderBll.DeleteShowOrder(showOrderId);
                X.Msg.Alert("提示","删除成功！").Show();
                BindData();
            }
        }
    }
}