using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Model.Entities;
using BLL.Home;
using Tools;

namespace WEB.SystemSeting.QuanXian
{
  
    public partial class User : BasePage
    {
        UserBll userbll = new UserBll();
        QuanXianBll qxbll = new QuanXianBll();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
               
                BindUsers();
                BindQX();
                BindJueSe();
            }
        }
        /// <summary>
        /// 绑定按钮权限
        /// </summary>
        protected void BindQX()
        {
            int cdid = Convert.ToInt32(Request.QueryString["cdid"]);
            btnadd.Visible = qxbll.IsHaveQx(cdid,"add");
            btndel.Visible = qxbll.IsHaveQx(cdid, "delete");
            //操作列
            CommandColumn cmd = (CommandColumn)GridPanel1.ColumnModel.Columns.First(c => c.ColumnID == "cmd");
            //第二个行内按钮
            GridCommand cmdedit = (GridCommand)cmd.Commands[1];
            //第三个行内按钮
            GridCommand cmdsq = (GridCommand)cmd.Commands[2];
            GridCommand cmdqxfw = (GridCommand)cmd.Commands[3];
            //根据权限设置隐藏
            cmdedit.Hidden = !qxbll.IsHaveQx(cdid, "edit");
            cmdsq.Hidden = !qxbll.IsHaveQx(cdid, "sq");
            cmdqxfw.Hidden = !qxbll.IsHaveQx(cdid, "QXFW");
        }
        /// <summary>
        /// 绑定用户信息
        /// </summary>
        private void BindUsers()
        {
            string xm = txtname.Text;
            string hym = txtyhm.Text;
            string zt = cmbstarus.GetValue();
            var data = new UserBll().GetUsers(xm,hym,zt);
            Store1.DataSource = data.Select(u => new { 
                YongHuId = u.YongHuId,
                name = userbll.GetUserNameById(u.YongHuId), 
                YHM =u.YHM,
                MM = EncryptionAndDecryption.Decryption(u.MM,Constant.Key),
                jsname = u.JueSe!=null?u.JueSe.JSMC:"",
                BZ = u.BZ,
                status = (Status)u.status
            });
            Store1.DataBind();
        }
        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SelData(object sender, DirectEventArgs e)
        {
            BindUsers();
        }
        /// <summary>
        /// 属性数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RefData(object sender, StoreRefreshDataEventArgs e)
        {
            BindUsers();
        }
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DelData(object sender, DirectEventArgs e)
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)GridPanel1.SelectionModel[0];
            if (s.SelectedRows.Count == 0)
            {
                X.Msg.Notify("提示","请选择需要删除的数据！").Show();
                return;
            }
            foreach (var item in s.SelectedRows)
            {
                if (userbll.DeleteUser(item.RecordID) > 0) 
                {
                    X.Msg.Alert("提示", item.RecordID+"删除失败！").Show();
                }
            }
            BindUsers();
        }
        /// <summary>
        /// 行内操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Cmd(object sender, DirectEventArgs e)
        {
            string type = e.ExtraParams["type"];
            string id = e.ExtraParams["id"];
            hiduserid.Value = id;
            switch (type)
            {
                case "edit":
                    window_adduser.Title = "修改用户信息";
                    YongHu user = userbll.GetUserById(id);
                    trgname.Text = userbll.GetUserNameById(user.YongHuId);
                    hidname.Value = user.RenYuanId;
                    yhm.Text = user.YHM;
                    txtmm.Text = EncryptionAndDecryption.Decryption(user.MM,Constant.Key);
                    txtmm1.Text = txtmm.Text;
                    cmbjs.Value = user.JSID;
                    cmbstatus1.Value = user.status;
                    txtbz.Text = user.BZ;
                    hidoldhym.Value = user.YHM;
                    cmbstatus1.Show();
                    btneditsave.Show();
                    btnaddsave.Hide();
                    btnreset.Hide();
                    txtyhm.RemoteValidation.Success = "true";
                    window_adduser.Show();
                    break;
                case "sel":
                    window_seluser.LoadContent("SelUser.aspx?id=" + id+"&cdid="+Request.QueryString["cdid"]);
                    window_seluser.Show();
                    break;
                case "sq":
                    X.Redirect("ShouQuan.aspx?type=yh&id=" + id + "&cdid=" + Request.QueryString["cdid"], "加载中...");
                    break;
                case"qxfw":
                    //List<XueYuan> have = qxbll.GetQuanXianFanWeiDW(id);
                    //List<XueYuan> all = qxbll.GetAllXiaoNeiDanWei();
                    //storealldw.DataSource = all.Where(s =>have.Count(c => c.XueYuanId == s.XueYuanId)==0).Select(s => new { Id=s.XueYuanId,MC=s.MC});
                    //storealldw.DataBind();
                    //storeseldw.DataSource = have.Select(s => new { Id = s.XueYuanId, MC = s.MC });
                    //storeseldw.DataBind();
                    //window_qxfw.Show();
                    break;
            }
        }


        /// <summary>
        /// 验证用户名是否重复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void YzHym(object sender, RemoteValidationEventArgs e)
        {
            string yhm = e.Value.ToString();
            YongHu user = userbll.GetUser(yhm);
            if (user == null||hidoldhym.Value.ToString() == yhm.Trim())
            {
                e.Success = true;
            }
            else
            {
                e.ErrorMessage = "该用户名已经存在！";
                e.Success = false;
            }
        }
        /// <summary>
        /// 绑定角色
        /// </summary>
        private void BindJueSe()
        {
            StoreJueSe.DataSource = userbll.GetJueSe();
            StoreJueSe.DataBind();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Save(object sender, DirectEventArgs e)
        {
                YongHu user = new YongHu() { 
                BZ = txtbz.Text,
                JSID = cmbjs.SelectedItem !=null?cmbjs.SelectedItem.Value:null,
                MM = EncryptionAndDecryption.Encryption(txtmm.Text,Constant.Key),
                RenYuanId = hidname.Value.ToString(),
                status = 1,
                YHM = yhm.Text.Trim(),
                YongHuId = Guid.NewGuid().ToString()
            };
            string str = userbll.AddUser(user);
            if (str=="ok")
            {
                X.Msg.Notify("提示", "添加成功！").Show();
                window_adduser.Hide();
                BindUsers();
            }
            else
            {
                X.Msg.Alert("提示",str).Show();
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Edit(object sender, DirectEventArgs e)
        {
            YongHu user = new YongHu()
            {
                BZ = txtbz.Text,
                JSID = cmbjs.SelectedItem != null ? cmbjs.SelectedItem.Value : null,
                MM = EncryptionAndDecryption.Encryption(txtmm.Text, Constant.Key),
                RenYuanId = hidname.Value.ToString(),
                status = Convert.ToInt32(cmbstatus1.SelectedItem.Value),
                YHM = yhm.Text.Trim(),
                YongHuId = hiduserid.Value.ToString()
            };

            string str = userbll.UpdateUser(user);
            if (str == "ok")
            {
                X.Msg.Notify("提示", "修改成功！").Show();
                window_adduser.Hide();
                BindUsers();
            }
            else
            {
                X.Msg.Alert("提示", str).Show();
            }
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
            if (list!=null&&list.Count>0)
            {
                string id = list[0];
                RenYuan js = new UserBll().GetRenYuanById(id);
                trgname.Text = js.PersonName;
                hidname.Value = js.RenYuanId;
                window_selRy.Hide();
            }
        }

        protected void SaveSelDW(object sender, StoreSubmitDataEventArgs e)
        {
            var data = e.Object<TmpSelDW>();
            //qxbll.UpdateQuanXianFanWeiDW(hiduserid.Value.ToString(), data.Select(s => s.Id).ToList());
            window_qxfw.Hide();
            X.Msg.Notify("提示", "保存成功！").Show();
        }
    }
    [Serializable]
    public class TmpSelDW
    {
        public string Id { get; set; }
        public string MC { get; set; }
    }
}