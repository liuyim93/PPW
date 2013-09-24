using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using BLL;
using Model.Entities;
namespace WEB.SystemSeting.XinXi
{
    public partial class GongShiXX :BasePage
    {
        GonShiXXBll xxBll = new GonShiXXBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                GetBin();
            }
        }

        //绑定公司信息
        protected void GetBin() 
        {
            GsXX xx = xxBll.GetXX();
            if (xx!=null)
            {
                txtSgName.Text = xx.SgName;
                txtMode.Text = xx.Mode;
                txtWz.Text = xx.WZ;
                txtDz.Text = xx.Dz;
                bqrn.Text = xx.banQuanSY;
            }
        }


        protected void ok(object sender,DirectEventArgs d) 
        {
            GsXX xx = xxBll.GetXX();
            GsXX xxx = new GsXX();
            if (xx!=null)
            {
                xxx.SgName = txtSgName.Text;
                xxx.Mode = txtMode.Text;
                xxx.WZ = txtWz.Text;
                xxx.Dz = txtDz.Text;
                xxx.banQuanSY = bqrn.Text;
                xxx.GsXXId = xx.GsXXId;
                if (xxBll.Eid(xxx) > 0) 
                {
                    X.Msg.Notify("提示", "修改成功").Show();
                }
            }
            else
            {
                xxx.SgName = txtSgName.Text;
                xxx.Mode = txtMode.Text;
                xxx.WZ = txtWz.Text;
                xxx.Dz = txtDz.Text;
                xxx.banQuanSY = bqrn.Text;
                xxx.GsXXId = Guid.NewGuid().ToString();
                if (xxBll.Add(xxx)>0)
	            {
                    X.Msg.Notify("提示","添加成功").Show();
	            } 
            }
        }
    }
}