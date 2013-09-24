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
   //查看BBS详情
    public partial class BBSgengTei : System.Web.UI.Page
    {
        bbsBll tzbll = new bbsBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest&&!IsPostBack)
            {
                DataBindList();
                DataList();
            }
        }

        //绑定主贴信息
        protected void DataBindList() 
        {
            string id = Request["id"].ToString();
            List<Tz> zt = tzbll.GetTz(id, "", null, null);
            disTile.Text = zt[0].Tile;
            disFTr.Text=zt[0].HuiYuanID!=null?GetHuiYuanName(zt[0].HuiYuanID):zt[0].Ip;
            disTime.Text=zt[0].CreateTime.Value.ToString("yyyy-MM-dd");
            Creata.Text=zt[0].Creatr;
        }

        //绑定跟贴列表
        protected void DataList() 
        {
            string id = Request["id"].ToString();
            List<GtTz> list=tzbll.GetGTtz("", id);
            storeList.DataSource = list.Select(x => new 
            {
                GtTzID=x.GtTzID,
                HuiYuanID=x.HuiYuanID!=null&&x.HuiYuanID!=""?GetHuiYuanName(x.HuiYuanID):x.Ip,
                Contents=x.Contents,
                CreateTime=x.CreateTime.Value.ToString("yyyy-MM-dd")
            });
            storeList.DataBind();
        }

        //跟据会员ID查找会员真实姓名
        protected string GetHuiYuanName(string id)
        {
            HuiYuanXinXiBll hybll = new HuiYuanXinXiBll();
            HuiYuan yh = hybll.GetHuiYuan(id);
            if (yh != null)
            {
                return yh.prName;
            }
            return "";
        }
        
        //行内操作
        protected void cmd(object sender,DirectEventArgs e) 
        {
            string type = e.ExtraParams["type"];
            string id = e.ExtraParams["id"];
            tzid.Value = id;
            List<HfTz> list=tzbll.GetHfTz(id, "");
            storGetZ.DataSource = list.Select(x => new 
            {
                HfTzId=x.HfTzId,
                HuiYuanID = x.HuiYuanID != "" && x.HuiYuanID != null ? GetHuiYuanName(x.HuiYuanID) : x.Ip,
                creats=x.creats,
                CreateTime=x.CreateTime.Value.ToString("yyyy-MM-dd")
            });
            storGetZ.DataBind();
            wind1.Show();
        }

        //删除所有跟贴下的子贴
        protected void DelZAll(object sdnder,DirectEventArgs e)
        {
            string id = tzid.Value.ToString();
            if (tzbll.DeleHFT("", id)>0) 
            {
                X.Msg.Notify("提示", "删除成功！").Show();
            }
        }

        //删除所有贴子下的跟贴
        protected void DeleAll(object sender,DirectEventArgs e) 
        {
            string id = Request["id"].ToString();
            try
            {
                tzbll.DeleGetZ("", id);
                X.Msg.Notify("提示","删除成功！").Show();
            }
            catch (Exception)
            {
                X.Msg.Alert("提示","请先删除跟贴下的所有子贴！").Show();
            }
        }

        //删除选中跟贴信息
        protected void deleGet(object sender,DirectEventArgs e) 
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)grid1.SelectionModel[0];
            if (s.SelectedRows.Count==0)
            {
                X.Msg.Alert("提示","请选择数据").Show();
                return;
            }
            try
            {
                foreach (var item in s.SelectedRows)
                {
                    tzbll.DeleGetZ(item.RecordID,"");
                }

                X.Msg.Notify("提示", "删除成功！").Show();  
            }
            catch (Exception)
            {
                X.Msg.Alert("提示","请先删除跟贴下的所有子贴！").Show();
            }
        }

        //删除选中子贴
        protected void DeleZt(object sender,DirectEventArgs e) 
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)gridZt.SelectionModel[0];
            if (s.SelectedRows.Count == 0)
            {
                X.Msg.Alert("提示", "请选择数据").Show();
                return;
            }
            foreach (var item in s.SelectedRows)
            {
                    tzbll.DeleHFT(item.RecordID,"");
            }
            X.Msg.Notify("提示","删除成功！").Show();
        }
    }
}