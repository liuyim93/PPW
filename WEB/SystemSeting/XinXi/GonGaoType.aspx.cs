using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using BLL.SystemSeting;
using Model.Entities;
using BLL.Home;
namespace WEB.SystemSeting.XinXi
{
    public partial class GonGaoType : BasePage
    {
        GonGaoBLL GgBll = new GonGaoBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                DataBindList();
                QX();
            }
        }

        protected void DataBindList() 
        {
            string name = txtName.Text;
            List<GgType> list = GgBll.GetGgType("",name);
            StoreList.DataSource = list.Select(y => new 
            {
                GgTypeID=y.GgTypeID,
                TypeName=y.TypeName
            });
            StoreList.DataBind();
        }

        protected void QX() 
        {
            QuanXianBll qxbll = new QuanXianBll();
            int cdid = Convert.ToInt32(Request.QueryString["cdid"]);
            bntAdd.Visible = qxbll.IsHaveQx(cdid, "add");
            bntDele.Visible = qxbll.IsHaveQx(cdid, "delete");
            bntEid.Visible = qxbll.IsHaveQx(cdid, "edit");
        }

        protected void Add(object sender,DirectEventArgs e) 
        {
            string id = Guid.NewGuid().ToString();
            string TypeName = textTypeName.Text;
            if (GgBll.AddType(id, TypeName) == "ok") 
            {
                X.Msg.Notify("提示","添加成功！").Show();
                window_addGgType.Hide();
                DataBindList();
            }
        }

        protected void Ref(object sender,StoreRefreshDataEventArgs e)
        {
            DataBindList();
        }

        protected void EidShow(object sender,DirectEventArgs e)
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)GridPanel1.SelectionModel[0];
            if (s.SelectedRows.Count == 0)
            {
                X.Msg.Alert("提示", "请选择需修改的数据！").Show();
                return;
            }
            string id = s.SelectedRow.RecordID;
            List<GgType> GgType=GgBll.GetGgType(id,"");
            textTypeName.Text = GgType[0].TypeName;
            window_addGgType.Title = "修改公告类型";
            bntAdds.Hide();
            bntEids.Show();
            window_addGgType.Show();
        }

        protected void EidSeve(object sender,DirectEventArgs e)
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)GridPanel1.SelectionModel[0];
            if (s.SelectedRows.Count == 0)
            {
                return;
            }
            string id = s.SelectedRow.RecordID;
            if (GgBll.EidType(id, textTypeName.Text) == "ok") 
            {
                X.Msg.Notify("提示","修改成功！").Show();
                window_addGgType.Hide();
                DataBindList();
            }
        }

        protected void Delete(object sender,DirectEventArgs e) 
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)GridPanel1.SelectionModel[0];
            if (s.SelectedRows.Count == 0)
            {
                X.Msg.Alert("提示", "请选择需删除的数据！").Show();
                return;
            }
            string id = s.SelectedRow.RecordID;
           List<Gg> Gg=GgBll.GetGg(id, "", "", null, null);
           if (Gg.Count>0)
           {
               X.Msg.Alert("提示","请先删除公告类型下的公告！").Show();
               return;
           }
           if (GgBll.DeleType(id)>0) 
           {
               X.Msg.Notify("提示","删除成功！").Show();
               DataBindList();
           }
        }
    }
}