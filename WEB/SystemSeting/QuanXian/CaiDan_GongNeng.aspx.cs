using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Text;
using BLL.Home;
using Model.Entities;

namespace WEB.SystemSeting.QuanXian
{
    /*
     * 张彤彤
     */
    public partial class CaiDan_GongNeng : BasePage
    {
        IndexBll indexbll = new IndexBll();
        QuanXianBll qxbll = new QuanXianBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                BindCaiDan();
            }
        }

        private void BindCaiDan()
        {
            List<CaiDan> data = indexbll.GetCaiDan();
            Ext.Net.TreeNode root = new Ext.Net.TreeNode();
            root.Text = "root";
            root.Nodes.AddRange(CreateTree(data,0));
            TreePanel1.Root.Add(root);
        }

        private List<Ext.Net.TreeNode> CreateTree(List<CaiDan> data, int pid)
        {
            List<CaiDan> list = data.Where(c=>c.Parent_Id == pid).ToList();
            List<GongNengDian> gnds = indexbll.GetAllGongNengDian();
            List<Ext.Net.TreeNode> roots = new List<Ext.Net.TreeNode>();
            foreach (CaiDan c in list)
            {
                Ext.Net.TreeNode t = new Ext.Net.TreeNode();
                t.Text = c.Name;
                t.NodeID = c.Id.ToString();
                List<Ext.Net.TreeNode> childnode = CreateTree(data, c.Id);
                if (childnode.Count == 0&&c.Url!= null &&c.Url!="")
                {
                    CreateGND(t, gnds);
                }
                t.Nodes.AddRange(childnode);
                roots.Add(t);
            }
            return roots;
        }

        private void CreateGND(Ext.Net.TreeNode root,List<GongNengDian> list)
        { 
            foreach (GongNengDian g in list)
            {
                Ext.Net.TreeNode t = new Ext.Net.TreeNode();
                t.Text = g.name;
                if (qxbll.IsCaiDanQuanXian(Convert.ToInt32(root.NodeID),g.Id))
                {
                    t.Checked = ThreeStateBool.True;
                }
                else
                {
                    t.Checked = ThreeStateBool.False;
                }
                t.CustomAttributes.Add(new ConfigItem("cdid",root.NodeID));
                t.CustomAttributes.Add(new ConfigItem("gnid", g.Id));
                root.Nodes.Add(t);
            }
        }

        protected void Save(object sender, DirectEventArgs e)
        {

            var ids = e.ExtraParams["ids"];
            List<Dictionary<string, string>> objs = JSON.Deserialize<List<Dictionary<string, string>>>(ids);
            List<CaiDan_GongNengDian> list = new List<CaiDan_GongNengDian>();
            foreach (Dictionary<string,string> s in objs)
            {
                string mid = s["cdid"];
                string gid = s["gnid"];
                CaiDan_GongNengDian c = new CaiDan_GongNengDian()
                {
                    Id = Guid.NewGuid().ToString(),
                    CDID = Convert.ToInt32(mid),
                    GNID = gid
                };
                list.Add(c);
            } 
            string str = qxbll.UpdateCaiDan_GongNengDian(list);
            if (str == "ok")
            {
                X.Msg.Alert("提示", "保存成功！").Show();
            }
            else
            {
                X.Msg.Alert("错误", str).Show();
            }
        }
    }
}