using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WEB.Auction.UserControl
{
    public partial class Top : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["HuiYuanID"] != null && Session["HuiYuanName"] != null)
                {
                    hlnkUserName.Text = Session["HuiYuanName"].ToString();
                    Panel1.Visible = false;
                    Panel2.Visible = true;
                }
            }
        }


        protected void lbtnLoginOut_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Panel1.Visible = true;
            Panel2.Visible = false;
        }
    }
}