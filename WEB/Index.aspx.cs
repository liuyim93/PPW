using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Ext.Net;
using BLL.Home;
using Model.Entities;
using System.Web.Security;
using Tools;

namespace WEB
{
  
    public partial class Index : Page
    {
        IndexBll indexbll = new IndexBll();
        UserBll userbll = new UserBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Expires = -1;
            if (!X.IsAjaxRequest)
            {
                List<Component> list = pantopmenu.Items.Where(i => i.ID == "topmenudefault").ToList();
                if (list.Count >0)
                {
                    Ext.Net.Button btn = (Ext.Net.Button)list[0];
                    btn.Pressed = true;
                    LoadDefaultTree(Convert.ToInt32(btn.Tag));
                }
                btnuser.Text = new UserBll().GetUserNameById(Session["userid"].ToString());
            }
        }
        

        protected override void OnInit(EventArgs e)
        {
            LoadTopMenu();
            base.OnInit(e);
        }
        private void LoadTopMenu()
        {
            List<CaiDan> list = indexbll.GetOneCaidan();
            int i = 0;
            foreach (CaiDan c in list)
            {
                Ext.Net.Button btn = new Ext.Net.Button();
                btn.Text = "<font style='font-size:12px;font-weight:bold;padding-right:5px;padding-left:2px;'>" + c.Name + "</font>";
                btn.ToggleGroup = "topmenu";
                btn.AllowDepress = false;
                btn.Tag = c.Id;
                btn.Icon = Icon.Female;
                btn.Scale = ButtonScale.Small;
                string hand = "reftree(#{TreePanel1},'" + c.Id + "','" + c.Name + "');";
                if (c.Url!=null&&c.Url!="")
                {
                    hand += "#{Panel9}.load('" + c.Url + "');#{Panel9}.setTitle('操作向导')";
                }
                btn.OnClientClick = hand;
                if (i==0)
                {
                    btn.ID = "topmenudefault";
                    TreePanel1.Title = c.Name;
                    if (c.Url!=null&&c.Url!=""&&!X.IsAjaxRequest)
                    {
                        Panel9.LoadContent(c.Url);
                    }
                }
                pantopmenu.Items.Add(btn);
                i++;
            }
        }

        public void LoadDefaultTree(int id)
        {
            List<CaiDan> data = indexbll.GetChildCaidan();
            List<Ext.Net.TreeNode> nodes = CreateTreeNode(data, id);
            Ext.Net.TreeNode root = new Ext.Net.TreeNode();
            root.Nodes.AddRange(nodes);
            TreePanel1.Root.Add(root);
        }
        [DirectMethod]
        public string RefreshMenu(int id)
        {
            List<CaiDan> data = indexbll.GetChildCaidan();
            List<Ext.Net.TreeNode> nodes = CreateTreeNode(data, id);
            Ext.Net.TreeNode root = new Ext.Net.TreeNode();
            root.Nodes.AddRange(nodes);
            root.NodeID = "root";
            root.Text = "root";
            Ext.Net.TreeNodeCollection roots = new Ext.Net.TreeNodeCollection();
            roots.Add(root);
            return roots.ToJson();
        }

        private List<Ext.Net.TreeNode> CreateTreeNode(List<CaiDan> data, int pid)
        {
            List<Ext.Net.TreeNode> root = new List<Ext.Net.TreeNode>();
            List<CaiDan> list = data.Where(c => c.Parent_Id == pid).ToList();
            foreach (CaiDan c in list)
            {
                Ext.Net.TreeNode t = new Ext.Net.TreeNode();
                string url = Page.ResolveUrl(c.Url);
                t.CustomAttributes.Add(new ConfigItem("menuurl", url,ParameterMode.Value));
                t.NodeID = c.Id.ToString();
                List<TreeNode> childs = CreateTreeNode(data, c.Id);
                if (childs.Count > 0)
                {
                    t.Icon = Icon.House;
                    t.Nodes.AddRange(childs);
                    t.Text = "<span style='font-size:15px;font-weight:bold;font-family:微软雅黑 新宋体;color:#626262;'>" + c.Name + "</span>";
                }
                else
                {
                   t.Icon = Icon.Layers;
                    t.Text = "<span style='font-size:13px;font-weight:bold;font-family:微软雅黑 新宋体;color:#626262;'>" + c.Name + "</span>";
                }
                
                root.Add(t);
            }
            return root;
        }

        protected void Exit(object sender, DirectEventArgs e)
        {
            FormsAuthentication.SignOut();
            Cookies.Clear(Session["userid"].ToString());
            Session.Clear();
            Response.Redirect("Login.aspx");
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
            if (user == null || hidoldhym.Value.ToString() == yhm.Trim())
            {
                e.Success = true;
            }
            else
            {
                e.ErrorMessage = "该用户名已经存在！";
                e.Success = false;
            }
        }

        //修改用户信息
        protected void EidYHXX(object sender,DirectEventArgs e)
        {
            string id = Session["userid"].ToString();
            YongHu YH=userbll.GetUserById(id);
            yhm.Text = YH.YHM;
            hidoldhym.Value = YH.YHM;
            window_adduser.Show();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Edit(object sender, DirectEventArgs e)
        {
            string mm = EncryptionAndDecryption.Encryption(txtmm.Text, Constant.Key);
            string id = Session["userid"].ToString();
            string name = yhm.Text;
            if (userbll.UpdateUser(id, name, mm) > 0) 
            {
                X.Msg.Notify("提示","修改成功！").Show();
                window_adduser.Hide();
            }
        }
    }
}