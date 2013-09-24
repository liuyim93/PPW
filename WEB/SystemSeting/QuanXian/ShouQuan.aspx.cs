using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using BLL.Home;
using Model.Entities;

namespace WEB.SystemSeting.QuanXian
{
    /*
     * 张彤彤
     */
    public partial class ShouQuan : BasePage
    {
        IndexBll indexbll = new IndexBll();
        QuanXianBll qxbll = new QuanXianBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                if (!qxbll.IsHaveQx(Convert.ToInt32(Request.QueryString["cdid"]),"sq"))
                {
                    Response.Redirect("~/ErrorMsg.aspx",true);
                }
                treeqx.Title = Request.QueryString["type"]=="yh"?"用户权限设置":"角色权限设置";
                BindAllCd();
                var tmp = GetEditQuanxXian(Request["type"], Request["id"]);
                if (tmp !=null)
                {
                    hidlasttime.Value = tmp.LastTime.ToString();
                }
            }
        }
        /// <summary>
        /// 绑定所有菜单
        /// </summary>
        private void BindAllCd()
        {
            List<CaiDan> data = indexbll.GetCaiDan();
            Ext.Net.TreeNode root = new Ext.Net.TreeNode();
            root.Text = "root";
            root.Nodes.AddRange(CreateNodes(data,0));
            treeqx.Root.Add(root);
        }
        /// <summary>
        /// 创建菜单树节点
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        private List<Ext.Net.TreeNode> CreateNodes(List<CaiDan> data, int pid)
        {
            List<CaiDan> list = data.Where(c=>c.Parent_Id == pid).ToList();
            List<GongNengDian> gnds = indexbll.GetAllGongNengDian();
            List<Ext.Net.TreeNode> roots = new List<Ext.Net.TreeNode>();
            foreach (CaiDan c in list)
            {
                Ext.Net.TreeNode t = new Ext.Net.TreeNode();
                t.Text = c.Name;
                t.NodeID = c.Id.ToString();
                List<Ext.Net.TreeNode> childnodes = CreateNodes(data,c.Id);
                if (c.CaiDan_GongNengDian.Count>0)
                {
                    CreateCNDNode(t, c.CaiDan_GongNengDian.OrderBy(g=>Convert.ToInt32(g.GNID)).ToList());
                }
                t.Nodes.AddRange(childnodes);
                roots.Add(t);
            }
            return roots;
        }
        /// <summary>
        /// 创建根菜单节点下的功能点
        /// </summary>
        /// <param name="root"></param>
        /// <param name="gnds"></param>
        private void CreateCNDNode(Ext.Net.TreeNode root,List<CaiDan_GongNengDian> gnds)
        {
            foreach (CaiDan_GongNengDian g in gnds)
            {
                Ext.Net.TreeNode t = new Ext.Net.TreeNode();
                t.Text = g.GongNengDian.name;
                t.CustomAttributes.Add(new ConfigItem("cdid",root.NodeID));
                t.CustomAttributes.Add(new ConfigItem("gnid", g.GNID));
                string type = Request.QueryString["type"];
                string id = Request.QueryString["id"];
                bool b = type == "yh" ? qxbll.IsHaveUserQx(id,Convert.ToInt32(root.NodeID), g.GNID) : qxbll.IsHaveJueSeQx(id,Convert.ToInt32(root.NodeID), g.GNID);
                if (b)
                {
                    t.Checked = ThreeStateBool.True;
                    
                }
                else
                {
                    t.Checked = ThreeStateBool.False;
                }
                root.Nodes.Add(t);
            }
        }
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Back(object sender, DirectEventArgs e)
        {
            if (Request.QueryString["type"] == "yh")
            {
                Response.Redirect("User.aspx?cdid="+Request.QueryString["cdid"]);
            }
            else
            {
                Response.Redirect("JiaoShe.aspx?cdid=" + Request.QueryString["cdid"]);
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Save(object sender, DirectEventArgs e)
        {
            var tmp = GetEditQuanxXian(Request["type"], Request["id"]);
            if (tmp!=null&&hidlasttime.Value.ToString()!=""&&tmp.LastTime.ToString()!=hidlasttime.Value.ToString())
            {
                X.Msg.Alert("提示", "在保存之前权限已被他人修改，请刷新页面重试！", new JFunction("window.location.reload();")).Show();
                return;
            }
            string yhid = null;
            string jsid = null;
            string type = Request.QueryString["type"];
            string id = Request.QueryString["id"];
            List<CaiDan> listcd = indexbll.GetCaiDan();
            if (type == "yh")//用户
            {
                yhid = id;
            }
            else//角色
            {
                jsid = id;
            }
            var ids = e.ExtraParams["ids"];
            List<Dictionary<string,string>> objs = JSON.Deserialize<List<Dictionary<string,string>>>(ids);
            List<Model.Entities.QuanXian> list = new List<Model.Entities.QuanXian>();
            //遍历选中的树节点
            foreach (Dictionary<string,string> r in objs)
            {
                //菜单Id
                int cdid = Convert.ToInt32(r["cdid"]);
                //功能id
                string gnid = r["gnid"];
                Model.Entities.QuanXian qx = new Model.Entities.QuanXian();
                qx.Id = Guid.NewGuid().ToString();
                qx.CDId = cdid;
                qx.GNDID = gnid;
                qx.HYID = yhid;
                qx.JSID = jsid;
                if (gnid == "1")
                {
                    List<int> printIds = GeiAllPrintCaiDanId(cdid, listcd);
                    foreach (var item in printIds)
                    {
                        if (!list.Exists(s=>s.CDId==item))
                        {
                            list.Add(new Model.Entities.QuanXian
                            { 
                                Id = Guid.NewGuid().ToString(),
                                CDId = item,
                                GNDID = "1",
                                HYID = yhid,
                                JSID = jsid
                            });
                        }
                    }
                }
                list.Add(qx);
            }
            string str = qxbll.UpdateQx(yhid, jsid, list);
            if (str == "ok")
            {
                SetEditQuanxXian(Request["type"], Request["id"]);
                hidlasttime.SetValue("");
                X.Msg.Notify("提示", "权限修改成功！").Show();
            }
            else
            {
                X.Msg.Alert("提示", str).Show();
            }
        }


        private List<int> GeiAllPrintCaiDanId(int thiscdid, List<CaiDan> data)
        {
            List<int> list = new List<int>();
            var tmp = data.Where(s=>s.Id == thiscdid);
            if (tmp.Count()>0)
            {
                int pid = tmp.First().Parent_Id;
                if (pid!=0)
                {
                    list.Add(pid);
                }
                
                list.AddRange(GeiAllPrintCaiDanId(pid,data));
            }
            return list;
        }
    }
}