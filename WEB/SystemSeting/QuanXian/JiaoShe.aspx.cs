using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Model.Entities;
using Model.Mapping;
using BLL.Home;
namespace WEB.SystemSeting.QuanXian
{
 
    public partial class JiaoShe : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                BindQX();
                Select();
            }
        }
        /// <summary>
        ///  刷新
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        protected void Ref(object obj,StoreRefreshDataEventArgs e) 
        {
            Select();
        }

        /// <summary>
        ///  查询所有
        /// </summary>
        protected void Select() 
        {
            JiaoSeBll bll = new JiaoSeBll();
            string Stat = cmbstarus.SelectedItem.Value;
            string Name = txtJSMC.Text;
            List<JueSe> JiaoShes = bll.SelectBy(Name, Stat);
            this.Store1.DataSource = JiaoShes.Select(js => new
            {
                JSMC = js.JSMC,
                BZ = js.Remark,
                JueSeId = js.JueSeId,
                status = (Tools.Status)js.status
            });
            this.Store1.DataBind();
        }

        /// <summary>
        /// 行内操作
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="sent"></param>
        protected void cmds(object obj,DirectEventArgs sent) 
        {
            string type = sent.ExtraParams["ComandName"];
            string id=sent.ExtraParams["id"];
            switch (type)
            {
                case "eid":
                    JiaoSeBll jsbll = new JiaoSeBll();
                    JueSe js=jsbll.SelectById(id);
                    JSMC.Text = js.JSMC;
                    BZ.Text = js.Remark;
                    addstatus.Value = js.status;
                    hiddenjsid.Text = js.JueSeId;
                    this.Button1.Hidden =true;
                    this.bntCZ.Hidden = true;
                    this.Window1.Title = "修改角色";
                    this.Window1.Show();
                    this.addstatus.Show();
                    this.bnteid.Show();
                    break;
                case "select":
                    Window2.LoadContent("SelectJiaoSe.aspx?id=" + id+"&cdid="+Request.QueryString["cdid"]);
                    Window2.Show();
                    break;
                case "sq":
                    X.Redirect("ShouQuan.aspx?type=js&id=" + id + "&cdid=" + Request.QueryString["cdid"],"加载中...");
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        protected void Add(object obj,DirectEventArgs e) 
        {
            JueSe JiaoSe = new JueSe();
            JiaoSe.JSMC = JSMC.Text;
            JiaoSe.status =Convert.ToInt32(addstatus.SelectedItem.Value);
            JiaoSe.Remark = this.BZ.Text;
            JiaoSe.JueSeId = Guid.NewGuid().ToString();
            JiaoSeBll bll = new JiaoSeBll();
            bll.Add(JiaoSe);
            X.Msg.Notify("提示", "保存成功").Show();
            Window1.Hide();
            Select();

        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        protected void Eid(object obj,DirectEventArgs e) 
        {
            JueSe JiaoSe = new JueSe();
            JiaoSe.JueSeId = hiddenjsid.Text;
            JiaoSe.Remark = BZ.Text;
            JiaoSe.JSMC = JSMC.Text;
            JiaoSe.status =Convert.ToInt32(addstatus.SelectedItem.Value);
            JiaoSeBll bll = new JiaoSeBll();
            bll.Update(JiaoSe);
            Window1.Hide();
            Select();
            X.Msg.Notify("提示","修改成功！").Show();
        }
        /// <summary>
        /// 按条件查
        /// </summary>
        public void SelectWhere(object obj,DirectEventArgs e) 
        {
            JiaoSeBll bll = new JiaoSeBll();
            string Stat = cmbstarus.SelectedItem.Value;
            string Name = txtJSMC.Text;
            this.Store1.DataSource = bll.SelectBy(Name, Stat).Select(js => new
            {
                JSMC = js.JSMC,
                BZ = js.Remark,
                JueSeId = js.JueSeId,
                status = (Tools.Status)js.status
            });
            this.Store1.DataBind();
        }
        /// <summary>
        /// 选中删除的列
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public void Delect(object obj,DirectEventArgs e) 
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)GridPanel1.SelectionModel[0];
            if (s.SelectedRows.Count == 0)
            {
                X.Msg.Notify("提示", "请选择需要删除的数据！").Show();
                return;
            }
            JiaoSeBll bll = new JiaoSeBll();
            UserBll userBll = new UserBll();
            foreach (var item in s.SelectedRows)
            {
                object ob=userBll.GetByJsID(item.RecordID);
                if (ob!=null)
                {
                    X.Msg.Alert("提示", ob.ToString() + "不能删除，请先将对应的用户信息删除。").Show();
                    continue;
                }
                if (bll.Delect(item.RecordID) == 0) 
                {
                    X.Msg.Alert("提示",item.RecordID+"删除失败").Show();
                }
            }
            Select();
        }

        /// <summary>
        /// 绑定按钮权限
        /// </summary>
        protected void BindQX()
        {
            int cdid = Convert.ToInt32(Request.QueryString["cdid"]);
            QuanXianBll qxbll = new QuanXianBll();
            btnAdd.Visible = qxbll.IsHaveQx(cdid, "add");
            btnDele.Visible = qxbll.IsHaveQx(cdid, "delete");
            //操作列
            CommandColumn cmd = (CommandColumn)GridPanel1.ColumnModel.Columns.First(c => c.ColumnID == "sel");
            //第二个行内按钮
            GridCommand cmdedit = (GridCommand)cmd.Commands[0];
            //第三个行内按钮
            GridCommand cmdsq = (GridCommand)cmd.Commands[2];
            //根据权限设置隐藏
            cmdedit.Hidden = !qxbll.IsHaveQx(cdid, "edit");
            cmdsq.Hidden = !qxbll.IsHaveQx(cdid, "sq");
        }
    }
}