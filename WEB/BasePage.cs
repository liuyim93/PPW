using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Entities;
using BLL.Home;
using Tools;
using Ext.Net;
using System.Collections;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Xml.Xsl;
using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;



namespace WEB
{
    public class BasePage : Page
    {
        private LoginBll loginBll = new LoginBll();
        private QuanXianBll qxbll = new QuanXianBll();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.ClientScript.RegisterStartupScript(typeof(Page), "", "<!--script>document.oncontextmenu = function(){event.returnValue=false;}</script--><link href=\"/Styles/BaseStyle.css\" rel=\"stylesheet\" type=\"text/css\" />");
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!X.IsAjaxRequest && !Page.IsPostBack)
            {
                bool b = IsHaveQX(Request.QueryString["cdid"], "browser");
                if (!b)
                {
                    Response.Write("没有相关权限！");
                    Response.End();
                    return;
                }
            }
            base.OnLoad(e);
        }

        /// <summary>
        /// 判断当前用户是否拥有指定菜单和功能点权限
        /// </summary>
        /// <param name="cdid">菜单id</param>
        /// <param name="gntag">功能点标记</param>
        /// <returns></returns>
        public bool IsHaveQX(string cdid,string gntag)
        {
            return qxbll.IsHaveQx(Convert.ToInt32(cdid), gntag);
        }

        protected YongHu YongHuXinXi
        {
            get
            {
                return loginBll.GetYongHu((Session["userid"]??"-1").ToString());
            }
        }

        protected CaiDan CaiDanXinXi
        {
            get
            {
                return qxbll.GetCaiDanById(Convert.ToInt32(Request["cdid"].Trim()));
            }
        }

        /// <summary>
        /// 获取树节点深度（根节点为0）
        /// </summary>
        /// <param name="node"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        protected int GetTreeNodeDepth(Ext.Net.TreeNode node, ref int depth)
        {
            if (node.ParentNode != null)
            {
                depth++;
                GetTreeNodeDepth(node.ParentNode as Ext.Net.TreeNode, ref depth);
            }
            return depth;
        }


        private List<EditQuanxXian> GetEditQuanxXians()
        {
            List<EditQuanxXian> list = (List<EditQuanxXian>)System.Web.HttpContext.Current.Application["EditQuanxXian"];
            if (list == null)
            {
                list = new List<EditQuanxXian>();
            }
            return list;
        }

        protected void SetEditQuanxXian(string lx, string id)
        {
            var list = GetEditQuanxXians();
            var tmp = list.Where(s => s.Id == id && s.LX == lx);
            if (tmp.Count() > 0)
            {
                tmp.First().LastTime = DateTime.Now;
            }
            else
            {
                list.Add(new EditQuanxXian() { Id = id, LX = lx, LastTime = DateTime.Now });
            }
            System.Web.HttpContext.Current.Application["EditQuanxXian"] = list;
        }

        protected EditQuanxXian GetEditQuanxXian(string lx, string id)
        {
            var list = GetEditQuanxXians();
            var tmp = list.Where(s => s.Id == id && s.LX == lx);
            if (tmp.Count() > 0)
            {
                return tmp.First();
            }
            else
            {
                return null;
            }
        }
    }


    public static class ExpandMethod
    {
        /// <summary>
        /// 未选返回null
        /// </summary>
        /// <param name="cmb"></param>
        /// <returns></returns>
        public static string GetValue(this ComboBox cmb)
        {
            if (cmb.SelectedItem == null)
            {
                return null;
            }
            else
            {
                return cmb.SelectedItem.Value == null?null:cmb.SelectedItem.Value.ToString();
            }
        }
      
        /// <summary>
        /// 未选返回null选择返回datetime对象
        /// </summary>
        /// <param name="df"></param>
        /// <returns></returns>
        public static DateTime? GetTime(this DateField df)
        {
            DateTime t = Convert.ToDateTime(df.Value);
            DateTime a = DateTime.MinValue;
            if (t == a)
            {
                return null;
            }
            DateTime dt = Convert.ToDateTime(df.Text);
            return dt;
        }
    }
}
