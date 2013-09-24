using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using BLL.Home;
using Model.Entities;
using BLL.SystemSeting;
using BLL.Basei;

namespace WEB.SystemSeting.XinXi
{
    public partial class GongGao :BasePage
    {
        GonGaoBLL GgBll = new GonGaoBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                BindType();
                DataBindList();
                QX();
            }
        }

        protected void QX() 
        {
            QuanXianBll qxbll = new QuanXianBll();
            int cdid = Convert.ToInt32(Request.QueryString["cdid"]);
            bntAdd.Visible = qxbll.IsHaveQx(cdid, "add");
            bntDelect.Visible = qxbll.IsHaveQx(cdid, "delete");
            bntEid.Visible = qxbll.IsHaveQx(cdid, "edit");
        }

        protected void BindType() 
        {
            List<GgType> list = GgBll.GetGgType("", "");
            StorType.DataSource = list.Select(x => new 
            {
                GgTypeID=x.GgTypeID,
                TypeName=x.TypeName
            });
            StorType.DataBind();
        }

        protected void DataBindList() 
        {
            string type=comType.SelectedItem.Value;
            string tile=txtTile.Text;
            string ry=hidname.Value==null?null:hidname.Value.ToString();
            DateTime? beg=xtxtbegin.GetTime();
            DateTime? end=xtxtbend.GetTime();
            List<Gg> list = GgBll.GetGg(type, tile, ry, beg, end);
            StorList.DataSource = list.Select(x => new 
            {
                GgId=x.GgId,
                Tile=x.Tile,
                GgTypeID = GetType(x.GgTypeID),
                RenYuanId = GetRyName(x.RenYuanId),
                CreatTime=x.CreatTime.Value.ToString("yyyy-MM-dd")
            });
            StorList.DataBind();
        }

        //跟据类型ID反回类型名
        string GetType(string id) 
        {
            List<GgType> list = GgBll.GetGgType(id,"");
            if (list.Count>0)
            {
                return list[0].TypeName;
            }
            return "";
        }

        //跟据人员ID查找人员姓名
        string GetRyName(string id) 
        {
            RenYuanBll rybll = new RenYuanBll();
            List<RenYuan> List = rybll.GetRenYuaSele("", "", "", id);
            if (List.Count>0)
            {
                return List[0].PersonName;
            }
            return "";
        }


        protected void Ref(object sender, StoreRefreshDataEventArgs e)
        {
            DataBindList();
        }

        protected void SelRY(object sender, DirectEventArgs e)
        {
            window_selRy.LoadContent("/SystemSeting/Basei/ReYuanSelectd.aspx?type=o&key=selry&cdid=" + Request.QueryString["cdid"]);
            window_selRy.Title = "选择人员";
            window_selRy.Show();
        }

        protected void SelectedRY(object sender, DirectEventArgs e)
        {
            List<string> list = (List<string>)Session["selry"];
            if (list != null && list.Count > 0)
            {
                string id = list[0];
                RenYuan js = new UserBll().GetRenYuanById(id);
                trgname.Text = js.PersonName;
                hidname.Value = js.RenYuanId;
                window_selRy.Hide();
            }
        }


        protected void Add(object obj, DirectEventArgs e)
        {
            Gg gg = new Gg();
            gg.GgId = Guid.NewGuid().ToString();
            gg.GgTypeID = combType.SelectedItem.Value.ToString();
            gg.Tile = txtTileAdd.Text;
            gg.Contents=e.ExtraParams["txt_TiMuNeiRong"];
            gg.CreatTime = DateTime.Now;
            gg.RenYuanId = YongHuXinXi.RenYuanId;
            if (GgBll.AddGg(gg) > 0) 
            {
                X.Msg.Alert("提示", "添加成功！").Show();
                window_addGonGao.Hide();
                DataBindList();
            }
        }

        //修改显示
        protected void EidShow(object obj,DirectEventArgs e) 
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)GridPanel1.SelectionModel[0];
            if (s.SelectedRows.Count == 0)
            {
                X.Msg.Alert("提示", "请选择数据！").Show();
                return;
            }
            string id = s.SelectedRow.RecordID;
            Gg go=GgBll.GetGgByid(id);
            combType.SelectedItem.Value = go.GgTypeID;
            txtTileAdd.Text = go.Tile;
            hidTiMuNeiRong.Value = go.Contents;
            window_addGonGao.Title = "修改公告信息";
            bntAdds.Hide();
            bntEids.Show();
            window_addGonGao.Show();
        }

        protected void EidSever(object sender,DirectEventArgs e) 
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)GridPanel1.SelectionModel[0];
            string id = s.SelectedRow.RecordID;
            Gg go = new Gg();
            go.GgTypeID = combType.SelectedItem.Value;
            go.Tile = txtTileAdd.Text;
            go.Contents = e.ExtraParams["txt_TiMuNeiRong"];
            go.GgId = id;
            if (GgBll.EidGg(go) > 0) 
            {
                X.Msg.Alert("提示", "修改成功！").Show();
                DataBindList();
                window_addGonGao.Hide();
            }
        }

        //删除
        protected void Delet(object sender,DirectEventArgs e) 
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)GridPanel1.SelectionModel[0];
            if (s.SelectedRows.Count == 0)
            {
                X.Msg.Alert("提示", "请选择数据！").Show();
                return;
            }
            string id = s.SelectedRow.RecordID;
            if (GgBll.Dele(id) > 0) 
            {
                X.Msg.Notify("提示", "删除成功！").Show();
                DataBindList();
            }
        }

        //查看
        protected void Selectd( object sender,DirectEventArgs e)
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)GridPanel1.SelectionModel[0];
            if (s.SelectedRows.Count == 0)
            {
                X.Msg.Alert("提示", "请选择数据！").Show();
                return;
            }
            string id = s.SelectedRow.RecordID;
            Gg go=GgBll.GetGgByid(id);
            disType.Text = GetType(go.GgTypeID);
            TextSeleTile.Text = go.Tile;
            disConts.Text = go.Contents;
            disTime.Text = go.CreatTime.Value.ToString("yyyy-MM-dd");
            window_GgSele.Show();
        }

    }
}