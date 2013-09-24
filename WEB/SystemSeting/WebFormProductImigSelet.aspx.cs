using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Entities;
using BLL.SystemSeting;
using System.IO;

namespace WEB.SystemSeting
{
    public partial class WebFormProductImig : System.Web.UI.Page
    { 
        ProductBLL pcBll = new ProductBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinImg();
                Datab();
                Dele();
            }
        }

        protected void DataBinImg() 
        {
            string id = Request["id"].ToString();
            List<ProductImeg> list = pcBll.GetProtductImeg("",id);
            datalist.DataSource = list.Select(y => new
            {
                id=y.ProductImegId,
                ProductID=y.ProductID,
                xh = y.xh,
                img = y.img
            });
            datalist.DataBind();
        }

        protected void Datab() 
        {
           string id = Request["id"].ToString();
           List<Product> cp=pcBll.GetById(id);
           if (cp.Count > 0) 
           {
               txtBh.Text = cp[0].coding;
               pinPai.Text = cp[0].productBrand;
               PaiMaiJG.Text = cp[0].PmJGproduct == null ? "" : cp[0].PmJGproduct.Value.ToString();
               SetTime.Text = cp[0].TimePoint == null ? "" : cp[0].TimePoint.Value.ToString();
               txtName.Text = cp[0].productName;
               type.Text = GetLX(cp[0].ProductTypeID); 
               txtJG.Text = cp[0].productPrice.Value.ToString();
               paisj.Text = cp[0].AuctionTime == null ? "" : cp[0].AuctionTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
               discpSeled.Text = cp[0].ProductDetails;
               txtCreaTime.Text = cp[0].CreateTime.Value.ToString("yyyy-MM-dd");
               disIsSaoye.Text = cp[0].isshouYei == 0 ? "不显示" : "显示";
               txtIntro.Text = cp[0].Intro == null ? "" : cp[0].Intro;
               txtPriceAdd.Text=cp[0].PriceAdd.ToString();
               txtAuctionPoint.Text=cp[0].AuctionPoint.ToString();
               txtFee.Text = cp[0].Fee.ToString();
               txtShipFee.Text = cp[0].ShipFee.ToString();
           }
        }

        protected void Dele() 
        {
            if (Request["tuid"] != null) 
            {
                string id = Request["tuid"].ToString();
                List < ProductImeg > list = pcBll.GetProtductImeg(id,"");
                string ph = list[0].img;
                string sotph = Server.MapPath(ph);
                if (File.Exists(sotph)) 
                {
                    File.Delete(sotph);
                }
                if (pcBll.DeletImg(id) > 0)
                {
                    Datab();
                    DataBinImg();
                }
            }
            
        }

        //跟据产品类型ID反回类型名
        protected string GetLX(string id)
        {
            ProductTypeBll bll = new ProductTypeBll();
            List<ProductType> lx = bll.GetAll("", id, "");
            if (lx.Count > 0)
            {
                return lx[0].TypeName;
            }
            else
            {
                return "";
            }
        }
    }
}