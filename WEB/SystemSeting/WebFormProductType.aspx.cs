using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Model.Entities;
using BLL.SystemSeting;
using Tools;
using BLL.Home;
namespace WEB.SystemSeting
{
    //产品类型
    public partial class WebFormProductType :BasePage
    {
        ProductTypeBll bll = new ProductTypeBll();
        QuanXianBll qxbll = new QuanXianBll();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                hidSelectedTreeNode.Value = "0";
                TreePanel1.Title = "选择产品类型";
                BindDicTree();
                TreePanel1.ExpandNode("0", false);
                BindData();
                QX();
            }
        }


        public void QX()
        {
            int cdid = Convert.ToInt32(Request.QueryString["cdid"]);
            btnAdd.Visible = qxbll.IsHaveQx(cdid, "add");
            btnDel.Visible = qxbll.IsHaveQx(cdid, "delete");
            CommandColumn cmd = (CommandColumn)GridPanel1.ColumnModel.Columns.First(c => c.ColumnID == "cmd");
            GridCommand cmdedit = (GridCommand)cmd.Commands[0];
            cmdedit.Hidden = !qxbll.IsHaveQx(cdid, "edit");
        }
        /// <summary>
        /// 绑定字典树
        /// </summary>
        private void BindDicTree()
        {
            Ext.Net.TreeNode rootNode = new Ext.Net.TreeNode();
            rootNode.NodeID = "0";
            rootNode.Text = "全部产品类型";
            
           // Ext.Net.TreeNode rootNode = TreePanel1.Root.First() as Ext.Net.TreeNode;
         //   CreateTree(mddBLL.GetAllAvailableItems(DicTabName).ToList(), rootNode);
            CreateTree(bll.GetAll("","",""), rootNode);
            TreePanel1.Root.Add(rootNode);
            TreePanel1.SelectNode("0");
        }

        [DirectMethod]
        public string CilenRefTree()
        {
            Ext.Net.TreeNode rootNode = new Ext.Net.TreeNode();
            rootNode.NodeID = "0";
            rootNode.Text = "全部" + CaiDanXinXi.Name;

            CreateTree(bll.GetAll("", "", ""), rootNode);
            Ext.Net.TreeNodeCollection roots = new Ext.Net.TreeNodeCollection();
            roots.Add(rootNode);
            return roots.ToJson();
        }

        public void TreeSelect()
        {
            BindDicTree();
            TreePanel1.CollapseNode(hidSelectedTreeNode.Value.ToString(), false);
            TreePanel1.ExpandNode(hidSelectedTreeNode.Value.ToString(), false);
        }

        //创建树
        private void CreateTree(List<ProductType> data, Ext.Net.TreeNode pNode)
        {
            List<ProductType> lst = data.Where(c => c.Fid == pNode.NodeID).ToList();
            List<Ext.Net.TreeNode> childNodes = new List<Ext.Net.TreeNode>();
            foreach (ProductType item in lst)
            {
                Ext.Net.TreeNode tnd = new Ext.Net.TreeNode();
                tnd.Text = item.TypeName;
                tnd.NodeID = item.ProductTypeID;
                CreateTree(data, tnd);
                childNodes.Add(tnd);
            }
            pNode.Nodes.AddRange(childNodes);
        }

        /// <summary>
        /// 绑定列表数据
        /// </summary>
        private void BindData()
        {
            var dics = bll.GetAll(txtName.Text,"", hidSelectedTreeNode.Value.ToString());
            StoreDic.DataSource = dics.Select(x => new 
            {
                Id=x.ProductTypeID,
                MC=x.TypeName,
                FID=x.Fid,
                BZ=x.remark
            });
            StoreDic.DataBind();
        }

        protected void TreeNodeClick(object source, DirectEventArgs e)
        {
            hidSelectedTreeNode.Value = e.ExtraParams["nodeId"];
            TreePanel1.CollapseNode(e.ExtraParams["nodeId"], true);
            TreePanel1.ExpandNode(e.ExtraParams["nodeId"], false);
            BindData();
        }

        [DirectMethod]
        public void BindFname()
        {
            List<ProductType> fdic = bll.GetAll("", hidSelectedTreeNode.Value.ToString(), "");
            if (fdic.Count > 0)
            {
                disFname.Text = fdic[0].TypeName;
            }
            else
            {
                disFname.Text = "无父ID";
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void SearchDics(object source, DirectEventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void BindDics(object source, StoreRefreshDataEventArgs e)
        {
            BindData();
        }



        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Add(object sender, DirectEventArgs e)
        {
            ProductType dic = new ProductType()
            {
               ProductTypeID=Guid.NewGuid().ToString(),
               Fid=hidSelectedTreeNode.Value.ToString(),
               TypeName = txt_MC.Text,
               remark = txt_BZ.Text, 
            };
            bll.Add(dic);
            X.Msg.Alert("提示", "添加成功").Show();
            Window_Add_Edit.Hide();
            BindData();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void DataDelete(object source, DirectEventArgs e)
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)GridPanel1.SelectionModel[0];
            if (s.SelectedRows.Count == 0)
            {
                X.Msg.Alert("提示", "请选择需要删除的数据！").Show();
                return;
            }
             List<ProductType> list=bll.GetAll("", "", s.SelectedRows[0].RecordID);

             if (list.Count>0)
             {
                   X.Msg.Alert("提示", "请删除子分类信息后，再删除！").Show();
                   return;
             }
             ProductBLL pcBll = new ProductBLL();
             List<Product> listcp = pcBll.GetPort("", "", s.SelectedRows[0].RecordID, "");
             if (listcp.Count>0)
             {
                 X.Msg.Alert("提示","请先删除产品类型下的产品！").Show();
                 return;
             }

             bll.Dele(s.SelectedRows[0].RecordID);
  
            X.Msg.Alert("提示", "删除完成！").Show();
            BindData();
        }

        /// <summary>
        /// 行内命令按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        protected void Cmd(object sender, DirectEventArgs e)
        {
            string type = e.ExtraParams["type"];
            string id = e.ExtraParams["id"];
            hidId.Value = id;
            switch (type)
            {
                case "edit":
                    Window_Add_Edit.Title = "修改产品类型";
                    List<ProductType> dic = bll.GetAll("",id,"");
                    txt_MC.Text = dic[0].TypeName;
                    txt_BZ.Text = dic[0].remark;
                    hidSelectedTreeNode.Value = dic[0].Fid;
                    BindFname();
                    btnEditSave.Show();
                    btnAddSave.Hide();
                    Window_Add_Edit.Show();
                    break;
            }
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Edit(object sender, DirectEventArgs e)
        {
            ProductType dic = new ProductType()
            {
                TypeName = txt_MC.Text,
                remark = txt_BZ.Text,
                ProductTypeID = hidId.Value.ToString(),
                Fid = hidSelectedTreeNode.Value.ToString()
            };
            if (bll.Upudate(dic) > 0)
            {
                X.Msg.Alert("提示", "修改成功！").Show();
                Window_Add_Edit.Hide();
                BindData();
            }
            else
            {
                X.Msg.Alert("提示", "修改失败！").Show();
            }
        }

        //双击变查看
        //protected void ShowDetails(object sender, DirectEventArgs e)
        //{
        //    string id = e.ExtraParams["id"];
        //    Window_Dic_Details.LoadContent(string.Format("MultilevelDataDictionaryDetails.aspx?id={0}&cdid={1}", id, Request["cdid"]));
        //    Window_Dic_Details.Show();
        //}
    }
}