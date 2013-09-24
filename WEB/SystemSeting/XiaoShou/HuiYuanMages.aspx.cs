using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.SystemSeting;
using Model.Entities;
using Ext;
using Ext.Net;
namespace WEB.SystemSeting.XiaoShou
{
    public partial class HuiYuanMages : System.Web.UI.Page
    {
        HuiYuanXinXiBll HYBll = new HuiYuanXinXiBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest&&!IsPostBack)
            {
                DataBin();
            }
        }

        //绑定会员列表
        protected void DataBin()
        {
            string HuiYuanName = textsHuiYanName.Text;
            string Name = textName.Text;
            string DJ = comxDj.SelectedItem.Value;
            List<HuiYuan> list= HYBll.GetHuiYuan(HuiYuanName, Name, DJ);
            StoreList.DataSource = list.Select(x => new 
            {
                HuiYuanID = x.HuiYuanID,
                HuiYuanName = x.HuiYuanName,
                MM = x.MM,
                email = x.email,
                prName = x.prName,
                sex = x.sex,
                sfz = x.sfz,
                sjh = x.sjh,
                PaiDian = x.PaiDian,
                JiFen = x.JiFen,
                DJ = x.DJ,
                CreatTime = x.CreatTime.Value.ToString("yyyy-MM-dd")
            });
            StoreList.DataBind();
        }

        protected void Ref(object sender,StoreRefreshDataEventArgs e)
        {
            DataBin();
        }
    }
}