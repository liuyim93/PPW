using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using BLL.SystemSeting;
using Model.Entities;
namespace WEB.SystemSeting.BBS
{
    public partial class BBSzhuTei : System.Web.UI.Page
    {
        bbsBll bbsBLL = new bbsBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest&&!IsPostBack)
            {
                DataBinds();
            }
        }

        protected void DataBinds() 
        {
            DateTime? beg = xtxtbegin.GetTime();
            DateTime? end = xtxtbend.GetTime();
            List<Tz> list = bbsBLL.GetTz(txtTil.Text, "", beg, end);
            StoreList.DataSource = list.Select(x => new 
            {
                TzID=x.TzID,
                Tile=x.Tile,
                HuiYuanID=x.HuiYuanID!=null&&x.HuiYuanID!=""?GetHuiYuanName(x.HuiYuanID):x.Ip,
                Creatr=x.Creatr,
                CreateTime=x.CreateTime.Value.ToString("yyyy-MM-dd")
            });
            StoreList.DataBind();
        }

        protected void Ref(object sender,StoreRefreshDataEventArgs e) 
        {
            DataBinds();
        }

        //跟据会员ID查找会员真实姓名
        protected string GetHuiYuanName(string id) 
        {
            HuiYuanXinXiBll hybll = new HuiYuanXinXiBll();
            HuiYuan yh=hybll.GetHuiYuan(id);
            if (yh!=null)
            {
                return yh.prName;
            }
            return "";
        }

        protected void Dele(object sender,DirectEventArgs e) 
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)GridPanel1.SelectionModel[0];
            if (s.SelectedRows.Count==0)
            {
                X.Msg.Alert("提示","请选择数据！").Show();
                return;
            }
            string id = s.SelectedRow.RecordID;
            List<GtTz> lstGt=bbsBLL.GetGTtz("", id);
            if (lstGt.Count>0)
            {
                X.Msg.Alert("提示","请先删除主贴下的跟贴！").Show();
                return;
            }
            if (bbsBLL.DeleTz(id) > 0) 
            {
                X.Msg.Notify("提示", "删除成功！").Show();
                DataBinds();
            }

        }

        protected void Selet(object sender,DirectEventArgs e)
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)GridPanel1.SelectionModel[0];
            if (s.SelectedRows.Count == 0)
            {
                X.Msg.Alert("提示", "请选择数据！").Show();
                return;
            }
            string id = s.SelectedRow.RecordID;
            wind1.LoadContent("BBSgengTei.aspx?id="+id+"&cdid="+Request["cdid"].ToString());
            wind1.Show();
        }
    }
}