using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Model.Entities;
using BLL.SystemSeting;
using BLL.Basei;
using Tools;
using BLL.Home;
using System.IO;
namespace WEB.SystemSeting
{
    public partial class WebFormproductBrand :BasePage
    {
         ProductTypeBll bll = new ProductTypeBll();
         ProductBLL pcBll = new ProductBLL();
         QuanXianBll qxbll = new QuanXianBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindSSXKTree();
            DataBindList();
            QX();
        }

        //绑定权限
        public void QX()
        {
            int cdid = Convert.ToInt32(Request.QueryString["cdid"]);
            bntAdd.Visible = qxbll.IsHaveQx(cdid, "add");
            bntDel.Visible = qxbll.IsHaveQx(cdid, "delete");
            bntEid.Visible = qxbll.IsHaveQx(cdid, "edit");
            bntSC.Visible = qxbll.IsHaveQx(cdid, "ShangCuan");
        }

        //绑定产品列表
        protected void Ref(object obj,StoreRefreshDataEventArgs e) 
        {
            DataBindList();
        }

        //跟据人员ID反回人员姓名
        protected string GetRyByID(string id) 
        {
            RenYuanBll rybll = new RenYuanBll();
            List<RenYuan> ry= rybll.GetRenYuaSele( "", "", "", id);
            return ry[0].PersonName;
        }

        protected void DataBindList() 
        {
            string name = txtSeleName.Text;
            string pinPai = txtPingPai.Text;
            string type = hidSelectedTreeNode2.Value == null ? null : hidSelectedTreeNode2.Value.ToString();
            List<Product> list=pcBll.GetPort(name,pinPai,type,"");
            StorlList.DataSource = list.Select(x=>new 
            {
                ProductID = x.ProductID,
                coding = x.coding,
                ProductTypeID = GetLX(x.ProductTypeID),
                productName = x.productName,
                productBrand = x.productBrand,
                productPrice = x.productPrice,
                PmJGproduct = x.PmJGproduct,
                HuiYuanID = x.HuiYuanID==null?"":x.HuiYuanID,
                AuctionTime = x.AuctionTime == null ? "" : x.AuctionTime.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                TimePoint = x.TimePoint,
                Intro=x.Intro,
                PriceAdd=x.PriceAdd,
                AuctionPoint=x.AuctionPoint,
                Fee=x.Fee,
                ShipFee=x.ShipFee,
                Status = (Status)x.Status
            });
            StorlList.DataBind();
        }

        //跟据产品类型ID反回类型名
        protected string GetLX(string id) 
        {
            List<ProductType> lx = bll.GetAll("", id, "");
            if (lx.Count>0)
            {
                return lx[0].TypeName;
            }
            else
            {
                return "";
            }
        }

        //查询树的单击事件
        protected void TreeNodeClick2(object source, DirectEventArgs e)
        {
            hidSelectedTreeNode2.Value = e.ExtraParams["nodeId"];
            TreePanel2.CollapseNode(e.ExtraParams["nodeId"]);
            TreePanel2.ExpandNode(e.ExtraParams["nodeId"], false);
            ddfSSXK.SetValue(e.ExtraParams["nodeId"], e.ExtraParams["nodeText"]);
        }

        //添加树的单击事件
        protected void TreeNodeClick1(object source, DirectEventArgs e)
        {
            hidSelectedTreeNode1.Value = e.ExtraParams["nodeId"];
            TreePanel1.CollapseNode(e.ExtraParams["nodeId"]);
            TreePanel1.ExpandNode(e.ExtraParams["nodeId"], false);
            ddsCPLX.SetValue(e.ExtraParams["nodeId"], e.ExtraParams["nodeText"]);
        }

        /// <summary>
        /// 绑定产品分类树
        /// </summary>
        private void BindSSXKTree()
        {
            Ext.Net.TreeNode rootNode = TreePanel2.Root.First() as Ext.Net.TreeNode;
            rootNode.Nodes.AddRange(GetXueKeTreeNodes("0"));
            Ext.Net.TreeNode rootNodeAdd = TreePanel1.Root.First() as Ext.Net.TreeNode;
            rootNodeAdd.Nodes.AddRange(GetXueKeTreeNodes("0"));
        }

         //创建树节点
          protected List<Ext.Net.TreeNode> GetXueKeTreeNodes(string pid,List<ProductType> data=null)
          {
            if (data == null)
            {
                data = bll.GetAll("","","");
            }
            List<Ext.Net.TreeNode> nodes = new List<Ext.Net.TreeNode>();
            List<ProductType> list = data.Where(s=>s.Fid == pid).ToList();
            foreach (var item in list)
            {
                Ext.Net.TreeNode t = new Ext.Net.TreeNode();
                t.NodeID = item.ProductTypeID;
                t.Text = item.TypeName;
                t.Nodes.AddRange(GetXueKeTreeNodes(item.ProductTypeID,data));
                nodes.Add(t);
            }
            return nodes;
        }

          protected void AddShow(object obj,EventArgs e)
          {
              window_addGongYingShang.Title = "添加产品信息";
              txtBh.Text = "";
              pinPai.Text = "";
              PaiMaiJG.Value = "0";
              SetTime.Value = null;
              txtName.Text = "";
              txtJG.Value = "0";
              hidTiMuNeiRong.Value = "";
              d241.Value = null;
              bntAdds.Show();
              bntEids.Hide();
              txtIntro.Text = "";
              txtPriceAdd.Value = "0.01";
              txtAcutionPoint.Value = "100";
              txtFee.Value = "0";
              txtShipFee.Value = "0";
              window_addGongYingShang.Show();
          }  

          protected void Add(object obj,DirectEventArgs e) 
          {
              if (ddsCPLX.Value.ToString()=="0")
              {
                  X.Msg.Alert("提示","请选择产品类型").Show();
                  return;
              }
              string dat = d241.Value.ToString();
              DateTime? datetim = dat == "" ? null : Convert.ToDateTime(dat) as DateTime?; 

              Product prd = new Product();
              prd.ProductID = Guid.NewGuid().ToString();
              prd.coding = txtBh.Text;
              prd.ProductTypeID = hidSelectedTreeNode1.Value.ToString();
              prd.productName = txtName.Text;
              prd.productBrand = pinPai.Text;
              prd.productPrice = Convert.ToDecimal(txtJG.Text);
              prd.PmJGproduct = Convert.ToDecimal(PaiMaiJG.Text);
              prd.AuctionTime = datetim;
              prd.TimePoint = SetTime.Text == "" ? 0 : Convert.ToInt32(SetTime.Text);
              prd.CreateTime = DateTime.Now;
              prd.ProductDetails = e.ExtraParams["txt_TiMuNeiRong"];
              prd.Status =(int)Status.未成交;
              prd.isshouYei =Convert.ToInt32(comx.SelectedItem.Value);
              prd.Intro = txtIntro.Text;
              prd.PriceAdd = Convert.ToDecimal(txtPriceAdd.Text);
              prd.AuctionPoint = Convert.ToInt32(txtAcutionPoint.Text);
              prd.Fee = txtFee.Text == "" ? 0 : Convert.ToDecimal(txtFee.Text);
              prd.ShipFee = txtShipFee.Text == "" ? 0 : Convert.ToDecimal(txtShipFee.Text);
              pcBll.Add(prd);
              X.Msg.Alert("提示", "添加成功！").Show();
              window_addGongYingShang.Hide();
              DataBindList();
          }

          protected void EidShow(object obj, EventArgs e) 
          {
              CheckboxSelectionModel s = (CheckboxSelectionModel)GridPanel1.SelectionModel[0];
              if (s.SelectedRows.Count == 0)
              {
                  X.Msg.Alert("提示", "请选择需修改的数据！").Show();
                  return;
              }
              string id = s.SelectedRow.RecordID;
              cpid.Value = id;
              List<Product> list = pcBll.GetById(id);
              txtBh.Text = list[0].coding;
              pinPai.Text = list[0].productBrand;
              PaiMaiJG.Text = list[0].PmJGproduct == null ? "" : list[0].PmJGproduct.Value.ToString();
              SetTime.Text = list[0].TimePoint == null ? "" : list[0].TimePoint.Value.ToString();
              txtName.Text = list[0].productName;
              ddsCPLX.SetValue(list[0].ProductTypeID, GetLX(list[0].ProductTypeID));
              hidSelectedTreeNode1.Text = list[0].ProductTypeID;
              txtJG.Text = list[0].productPrice.Value.ToString();
              //paisj.Text = list[0].AuctionTime == null ? null : list[0].AuctionTime.Value.ToString();
              d241.Value = list[0].AuctionTime == null ? null : list[0].AuctionTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
              hidTiMuNeiRong.Value = list[0].ProductDetails;
              hidBh.Value = list[0].coding;
              comx.SelectedItem.Value = list[0].isshouYei.ToString();
              txtPriceAdd.Text = list[0].PriceAdd.ToString();
              txtAcutionPoint.Text = list[0].AuctionPoint.ToString();
              txtIntro.Text=list[0].Intro;
              txtFee.Text=list[0].Fee.ToString();
              txtShipFee.Text=list[0].ShipFee.ToString();
              bntAdds.Hide();
              bntEids.Show();
              window_addGongYingShang.Title = "修改产品信息";
              window_addGongYingShang.Show();
          }

         //修改保存
          protected void EidSeve(object obj,DirectEventArgs e) 
          {
              string dat = d241.Value.ToString();
              DateTime? datetim= dat==""?null:Convert.ToDateTime(dat)as DateTime?; 
              if (ddsCPLX.Value.ToString() == "0")
              {
                  X.Msg.Alert("提示", "请选择产品类型").Show();
                  return;
              }
              string id = cpid.Value.ToString();
              Product prd = new Product();
              prd.ProductID = id;
              prd.coding = txtBh.Text;
              prd.ProductTypeID = hidSelectedTreeNode1.Value.ToString();
              prd.productName = txtName.Text;
              prd.productBrand = pinPai.Text;
              prd.productPrice = Convert.ToDecimal(txtJG.Text);
              prd.PmJGproduct = Convert.ToDecimal(PaiMaiJG.Text);
              prd.AuctionTime = datetim;
              prd.TimePoint = SetTime.Text == "" ? 0 : Convert.ToInt32(SetTime.Text);
              prd.isshouYei =Convert.ToInt32(comx.SelectedItem.Value);
              prd.ProductDetails = e.ExtraParams["txt_TiMuNeiRong"];
              prd.PriceAdd = Convert.ToDecimal(txtPriceAdd.Text);
              prd.AuctionPoint = Convert.ToInt32(txtAcutionPoint.Text);
              prd.Intro = txtIntro.Text;
              prd.Fee = txtFee.Text == "" ? 0 : Convert.ToDecimal(txtFee.Text);
              prd.ShipFee = txtShipFee.Text == "" ? 0 : Convert.ToDecimal(txtShipFee.Text);
              if (pcBll.Eid(prd) > 0) 
              {
                  X.Msg.Alert("提示","修改成功！").Show();
                  window_addGongYingShang.Hide();
                  DataBindList();
                  hidBh.Value = "";
                  cpid.Value = "";
              }
          }

          protected void Dele(object obj,DirectEventArgs e) 
          {
              CheckboxSelectionModel s = (CheckboxSelectionModel)GridPanel1.SelectionModel[0];
              if (s.SelectedRows.Count == 0)
              {
                  X.Msg.Alert("提示", "请选择数据！").Show();
                  return;
              }
              string id = s.SelectedRow.RecordID;
              string db=pcBll.Dele(id);
              if (db=="ok") 
              {
                  X.Msg.Alert("提示", "删除成功！").Show();
                  DataBindList();
              }
              else
              {
                  X.Msg.Alert("提示",db).Show();
              }
          }

          protected void ImegSever(object sender, DirectEventArgs e)
          {  
              string path = "~/Image/caiping/";
              CheckboxSelectionModel s = (CheckboxSelectionModel)GridPanel1.SelectionModel[0];
              if (s.SelectedRows.Count == 0)
              {
                  X.Msg.Alert("提示", "请选择数据！").Show();
                  return;
              }
              string cpid = s.SelectedRow.RecordID;
              if (fileLud.HasFile) 
              {
                string name=Guid.NewGuid().ToString()+fileLud.PostedFile.FileName;
                string phts = Server.MapPath(path + name);
                string strFileName = Path.GetExtension(fileLud.PostedFile.FileName).ToUpper();//获取文件后缀
                if (!(strFileName == ".BMP" || strFileName == ".GIF" || strFileName == ".JPG"))
                {
                    X.Msg.Alert("提示信息", "文件格式不正确！").Show();
                    return;
                }
                string xh = textXh.Text;
                string id = Guid.NewGuid().ToString();
                fileLud.PostedFile.SaveAs(phts);
                pcBll.AddCpImge(id, cpid, "../Image/caiping/" + name, xh);
                X.Msg.Alert("提示","上传成功").Show();
              }
              else
              {
                  X.Msg.Alert("提示","请选择上传的图片！").Show();
              }
          }

         //验证价格
          protected void YzJG(object sender,RemoteValidationEventArgs e) 
          {
              try
              {
                  decimal dta = Convert.ToDecimal(e.Value.ToString());
                  e.Success = true;
              }
              catch (Exception)
              {
                  e.ErrorMessage = e.Value.ToString()+"不是有效数值！";
                  e.Success = false;
              } 
          }

         //查看详情 
         protected void Selectd(object obj,DirectEventArgs e) 
          {
              CheckboxSelectionModel s = (CheckboxSelectionModel)GridPanel1.SelectionModel[0];
              if (s.SelectedRows.Count == 0)
              {
                  X.Msg.Alert("提示", "请选择数据！").Show();
                  return;
              }
              string id = s.SelectedRow.RecordID;
              window_sele.LoadContent("WebFormProductImigSelet.aspx?id=" + id + "&cdid=" + Request.QueryString["cdid"] + "");
              window_sele.Show();
          }

         protected void YzBH(object sender, RemoteValidationEventArgs e)
         {
             string bh = e.Value.ToString();
            List<Product> obj =pcBll.GetPort("","",null,bh) ;
            if (obj.Count <= 0 || hidBh.Value.ToString() == bh.Trim())
             {
                 e.Success = true;
             }
             else
             {
                 e.ErrorMessage = "该编号以存在！";
                 e.Success = false;
             }
         }
    }
  }
