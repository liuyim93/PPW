using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.SystemSeting;
using Model.Entities;

namespace WEB.UserInfo
{
    public partial class Biding : System.Web.UI.Page
    {
        ProductBLL proBll = new ProductBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        public void Bind() 
        {
            if (Session["HuiYuanName"] == null || Session["HuiYuanID"] == null)
            {
                Response.Redirect("../Auction/Index.aspx");
            }
            else 
            {
                string hyId = Session["HuiYuanID"].ToString();
                int status = 3;
                dlstBiding.DataSource = proBll.GetProductbyStatus(hyId,status);
                dlstBiding.DataBind();
            }
        }
    }
}