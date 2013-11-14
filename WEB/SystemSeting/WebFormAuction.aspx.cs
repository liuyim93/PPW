using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BLL.SystemSeting;
using Ext.Net;
using Model.Entities;
using BLL.Home;

namespace WEB.SystemSeting
{
    public partial class WebFormAuction : System.Web.UI.Page
    {
        AuctionBll auctionBll = new AuctionBll();
        ProductBLL proBll = new ProductBLL();
        HuiYuanXinXiBll hyBll = new HuiYuanXinXiBll();
        QuanXianBll qxBll = new QuanXianBll();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("../../Login.aspx");
            }
            else 
            {
                BindData();
                BindProduct();
                BindAuctionType();
                Permission();
            }            
        }

        /// <summary>
        /// 权限
        /// </summary>
        public void Permission() 
        {
            int menuId = Convert.ToInt32(Request.QueryString["cdid"]);
            btnAdd.Visible = qxBll.IsHaveQx(menuId,"add");
            btnEdit.Visible = qxBll.IsHaveQx(menuId,"edit");
            btnDel.Visible = qxBll.IsHaveQx(menuId,"delete");
            btnDetail.Visible = qxBll.IsHaveQx(menuId,"browser");
        }

        public void AddShow(object sender,EventArgs e) 
        {
            window_update.Title = "添加竞拍信息";
            txtAuctionPoint.Value= "100";
            txtAuctionPrice.Value = "0";
            txtPriceAdd.Value = "0.01";
            numFreePoint.Value = "0";
            cboxRecommend.Value = "0";
            btnAdds.Show();
            btnEdits.Hide();
            btnClose.Show();
            window_update.Show();

        }

        public void EditShow(object sender,EventArgs e) 
        {
            window_update.Title = "修改竞拍信息";
            CheckboxSelectionModel check=GridPanel1.SelectionModel[0]as CheckboxSelectionModel;
            if (check.SelectedRows.Count == 0)
            {
                X.Msg.Alert("提示", "请选择要修改的数据").Show();
                return;
            }
            else 
            {
                string actId = check.SelectedRow.RecordID;
                List<auction> list_act = auctionBll.GetAuction(actId);
                if (list_act[0].Status == 3)
                {
                    X.Msg.Alert("提示", "该拍品已经成交，不能修改！").Show();
                    return;
                }
                else 
                {
                    cboxPro.Value = list_act[0].ProductID;
                    cboxAuctionTypes.Value = list_act[0].AuctionTypeID;
                    txtAuctionPrice.Text = list_act[0].AuctionPrice.ToString();
                    txtAuctionPoint.Text = list_act[0].AuctionPoint.ToString();
                    txtPriceAdd.Text = list_act[0].PriceAdd.ToString();
                    cboxRecommend.Value = list_act[0].IsRecommend;
                    txtAuctionTime.Value = list_act[0].AuctionTime.ToString("yyyy-MM-dd hh:mm:ss");
                    numFreePoint.Text=list_act[0].FreePoint.ToString();
                    btnAdds.Hide();
                    btnEdits.Show();
                    btnClose.Show();
                    window_update.Show();
                }
            }                       
        }

        protected void Del(object sender,DirectEventArgs e) 
        {
            CheckboxSelectionModel check=GridPanel1.SelectionModel[0]as CheckboxSelectionModel;
            if (check.SelectedRows.Count == 0)
            {
                X.Msg.Alert("提示", "请选择要删除的数据").Show();
                return;
            }
            else 
            {
                string actId = check.SelectedRow.RecordID;
                List<auction> list_act = auctionBll.GetAuction(actId);
                if (list_act[0].Status == 3)
                {
                    X.Msg.Alert("提示", "该商品已成交，不能删除！").Show();
                    return;
                }
                else 
                {
                    auctionBll.DeleteAuction(actId);
                    X.Msg.Alert("提示", "删除成功！").Show();
                    BindData();
                }
                
            }
        }

        protected void ShowDetail(object sender,DirectEventArgs e) 
        {
            CheckboxSelectionModel check=GridPanel1.SelectionModel[0]as CheckboxSelectionModel;
            if (check.SelectedRows.Count == 0)
            {
                X.Msg.Alert("提示", "请选择数据").Show();
                return;
            }
            else 
            {
                string actId = check.SelectedRow.RecordID;
                List<auction> list_act = auctionBll.GetAuction(actId);
                disAuctionPoint.Text=list_act[0].AuctionPoint.ToString();
                disAuctionPrice.Text="￥"+list_act[0].AuctionPrice.ToString();
                disAuctionTime.Text=list_act[0].AuctionTime.ToString();
                disAuctionType.Text=auctionBll.GetAuctionTypebyId(list_act[0].AuctionTypeID)[0].TypeName;
                disCoding.Text=list_act[0].Coding.ToString();
                disEndTime.Text=list_act[0].EndTime==null?"":list_act[0].EndTime.ToString();
                disFreePoint.Text=list_act[0].FreePoint.ToString();
                disHuiYuan.Text =list_act[0].HuiYuanID==""?"":hyBll.GetHuiYuan(list_act[0].HuiYuanID).HuiYuanName;
                disIsRecommend.Text=list_act[0].IsRecommend==1?"是":"否";
                disProName.Text=proBll.GetById(list_act[0].ProductID)[0].productName;
                disProPrice.Text = "￥" + proBll.GetById(list_act[0].ProductID)[0].productPrice.ToString();
                disStatus.Text=list_act[0].Status==3?"已成交":"未成交";
                disPriceAdd.Text = "￥" + list_act[0].PriceAdd.ToString();
                btnClose1.Show();
                window_detail.Show();
            }
        }

        protected void AddAuction(object sender,DirectEventArgs e) 
        {
            auction act = new auction();            
            if (txtAuctionTime.Value == "")
            {
                X.Msg.Alert("提示","竞拍时间不能为空").Show();
                return;
            }
            else 
            {
                act.AuctionPoint = Convert.ToInt32(txtAuctionPoint.Text);
                act.AuctionPrice = Convert.ToDecimal(txtAuctionPrice.Text);
                act.AuctionTime = Convert.ToDateTime(txtAuctionTime.Value);
                act.AuctionTypeID = cboxAuctionTypes.Value.ToString();
                act.FreePoint = Convert.ToInt32(numFreePoint.Text);
                act.TimePoint = 10;
                act.Status = 4;
                act.IsRecommend = Convert.ToInt32(cboxRecommend.Value);
                act.PriceAdd = Convert.ToDecimal(txtPriceAdd.Text);
                act.ProductID = cboxPro.Value.ToString();
                auctionBll.AddAuction(act);
                X.Msg.Alert("提示","添加成功！").Show();
                window_update.Hide();
                BindData();
            }
        }

        protected void EditAuction(object sender,DirectEventArgs e) 
        {
            if (txtAuctionTime.Value == "")
            {
                X.Msg.Alert("提示", "竞拍时间不能为空").Show();
                txtAuctionTime.Focus();
                return;
            }
            else 
            {
                CheckboxSelectionModel check=GridPanel1.SelectionModel[0]as CheckboxSelectionModel;
                auction act=new auction();
                act.AuctionID = check.SelectedRow.RecordID;
                act.AuctionPoint = Convert.ToInt32(txtAuctionPoint.Text);
                act.AuctionPrice = Convert.ToDecimal(txtAuctionPrice.Text);
                act.FreePoint = Convert.ToInt32(numFreePoint.Text);
                act.IsRecommend = Convert.ToInt32(cboxRecommend.Value);
                act.AuctionTypeID = cboxAuctionTypes.Value.ToString();
                act.ProductID = cboxPro.Value.ToString();
                act.AuctionTime = Convert.ToDateTime(txtAuctionTime.Value);
                act.PriceAdd = Convert.ToDecimal(txtPriceAdd.Text);
                auctionBll.UpdateAuction(act);
                X.Msg.Alert("提示","修改成功!").Show();
                window_update.Hide();
                BindData();
            }
        }

        /// <summary>
        /// 绑定拍品信息
        /// </summary>
        public void BindData() 
        {
            string proName = txtProName.Text;
            string auctionType = cboxAuctionType.Text;
            string status;
            if (cboxStatus.Value == null)
            {
                status = "";
            }
            else 
            {
                status = cboxStatus.Value.ToString();
            }
            List<auction> list_act = auctionBll.GetAuction(proName,auctionType,status);
            storeAuction.DataSource = list_act.Select(x=> new 
            {
               AuctionID=x.AuctionID,
               ProductName=proBll.GetById(x.ProductID)[0].productName,
               ProductPrice=proBll.GetById(x.ProductID)[0].productPrice,
               Coding=x.Coding,
               HuiYuan=x.HuiYuanID==""?"":hyBll.GetHuiYuan(x.HuiYuanID).HuiYuanName,
               AuctionPrice=x.AuctionPrice,
               AuctionPoint=x.AuctionPoint,
               FreePoint=x.FreePoint,
               AuctionTime=x.AuctionTime.ToString(),
               CreateTime=x.CreateTime,
               Status=x.Status==3?"已成交":"未成交",
               PriceAdd=x.PriceAdd,
               EndTime=x.EndTime==null?"":x.EndTime.ToString(),
               AuctionType=auctionBll.GetAuctionTypebyId(x.AuctionTypeID)[0].TypeName,
               IsRecommend=x.IsRecommend==1?"是":"否"
            });
            storeAuction.DataBind();
        }

        /// <summary>
        /// 绑定竞拍类型
        /// </summary>
        public void BindAuctionType() 
        {
            storeAuctionType.DataSource = auctionBll.GetAuctionType();
            storeAuctionType.DataBind();
        }

        /// <summary>
        /// 绑定产品
        /// </summary>
        public void BindProduct() 
        {
            List<Product> list_pro = proBll.GetPort("","","","");
            storePro.DataSource = list_pro.Select(x => new
            {
                ProductID=x.ProductID,
                ProductName=x.productName
            });
            storePro.DataBind();
        }
    }
}