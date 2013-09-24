using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Entities;
using BLL.Basei;
using Ext.Net;
using Tools;
using BLL.Home;
namespace WEB.SystemSeting.Basei
{
    public partial class ReYuan :BasePage
    {
        QuanXianBll qxbll = new QuanXianBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest&&!IsPostBack)
            {
                DataBin();
                QX();
            }
        }

        public void QX()
        {
            int cdid = Convert.ToInt32(Request.QueryString["cdid"]);
            bntAdd.Visible = qxbll.IsHaveQx(cdid, "add");
            bntDel.Visible = qxbll.IsHaveQx(cdid, "delete");
            bntEid.Visible = qxbll.IsHaveQx(cdid, "edit");
        }
        //绑定人员表
        public void DataBin() 
        {
            string name = txtXM.Text;
            string sex =comXB.SelectedItem.Value;
            string zw = txtZW.Text;
            RenYuanBll bll = new RenYuanBll();
            List<RenYuan> list=  bll.GetRenYuaSele(name,sex,zw,"").ToList();
            ReYuanList.DataSource = list.Select(x => new 
            {
                RenYuanId=x.RenYuanId,
                PersonName=x.PersonName,
                Sex=x.Sex,
                ZhiWei=x.ZhiWei,
                SFZ=x.SFZ,
                Email=x.Email,
                Mobile=x.Mobile
            });
            ReYuanList.DataBind();
        }
        public void Ref(object obj,StoreRefreshDataEventArgs e)
        {
            DataBin();
        }

        //添加
        public void AddSeve(object obj,DirectEventArgs e) 
        {
            RenYuan ry = new RenYuan();
            RenYuanBll bll = new RenYuanBll();
            ry.RenYuanId = Guid.NewGuid().ToString();
            ry.PersonName = txtName.Text;
            ry.Sex = comSex.SelectedItem.Value;
            ry.ZhiWei = txtZWM.Text;
            ry.SFZ = txtSFZ.Text;
            ry.Email = txtEml.Text;
            ry.Mobile = txtMobile.Value!=null?txtMobile.Value.ToString():null;
            ry.QQ = txtQQ.Value==null?null:txtQQ.Value.ToString();
            ry.Remark = txtBZ.Text;
            bll.Add(ry);
            X.MessageBox.Notify("提示", "添加成功！").Show();
            window_addRenYuan.Hide();
            DataBin();
        }

        //修改
        public void Eid(object obj,DirectEventArgs e) 
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)GridPanel1.SelectionModel[0];
            if (s.SelectedRows.Count == 0)
            {
                X.Msg.Alert("提示", "请选择需修改的数据！").Show();
                return;
            }
            string id = s.SelectedRow.RecordID;
            RenYuanBll bll = new RenYuanBll();
            List<RenYuan> list = bll.GetRenYuaSele("", "", "", id).ToList();
            txtName.Text = list[0].PersonName;
            txtEml.Text = list[0].Email;
            comSex.Value = list[0].Sex;
            txtZWM.Text = list[0].ZhiWei;
            txtSFZ.Text = list[0].SFZ;
            txtMobile.Value = list[0].Mobile;
            txtQQ.Value = list[0].QQ;
            txtBZ.Text = list[0].Remark;
            bntAddSeve.Hide();
            bntEidSeve.Show();
            window_addRenYuan.Title = "修改人员信息";
            window_addRenYuan.Show();
        }

        public void EidSeve(object obj,DirectEventArgs e) 
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)GridPanel1.SelectionModel[0];
            if (s.SelectedRows.Count == 0)
            {
                X.Msg.Alert("提示", "请选择需修改的数据！").Show();
                return;
            }
            string id = s.SelectedRow.RecordID;
            RenYuan ry = new RenYuan();
            ry.RenYuanId = id;
            ry.PersonName = txtName.Text;
            ry.Sex = comSex.SelectedItem.Value;
            ry.ZhiWei = txtZWM.Text;
            ry.SFZ = txtSFZ.Text;
            ry.Email = txtEml.Text;
            ry.Mobile = txtMobile.Value==null?null:txtMobile.Value.ToString();
            ry.QQ = txtQQ.Value==null?null:txtQQ.Value.ToString();
            ry.Remark = txtBZ.Text;
            RenYuanBll bll = new RenYuanBll();
            if (bll.Eid(ry)>0)
            {
                X.Msg.Notify("提示","修改成功！").Show();
                DataBin();
                window_addRenYuan.Hide();
            }
        }

        //删除 
        public void Dele(object obj,DirectEventArgs e) 
        {
            RenYuanBll bll = new RenYuanBll();
            CheckboxSelectionModel s = (CheckboxSelectionModel)GridPanel1.SelectionModel[0];
            if (s.SelectedRows.Count == 0)
            {
                X.Msg.Alert("提示", "请选择需删除的数据！").Show();
                return;
            }
            string id = s.SelectedRow.RecordID;
           string i=bll.Dele(id);
           if (i=="ok")
           {
               X.Msg.Notify("提示", "删除成功！").Show();
               DataBin();
           }
           else
           {
               X.Msg.Alert("提示",i).Show();
           }

        }
        //查看详情
        public void Selectd(object obj,DirectEventArgs e) 
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)GridPanel1.SelectionModel[0];
            if (s.SelectedRows.Count == 0)
            {
                X.Msg.Alert("提示", "请选择需查看的数据！").Show();
                return;
            }
            string id = s.SelectedRow.RecordID;
            Window_RenYuanSele.LoadContent("RenYuanSelected.aspx?id=" + id + "&cdid=" + Request.QueryString["cdid"]);
            Window_RenYuanSele.Show();
        }
    }
}