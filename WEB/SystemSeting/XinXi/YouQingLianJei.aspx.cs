using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Model.Entities;
using BLL.SystemSeting;
using System.IO;
using BLL.Home;

namespace WEB.SystemSeting.XinXi
{
    //友情连接
    public partial class YouQingLianJei :BasePage
    {
        YouQingBll yqbll = new YouQingBll();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest&&!IsPostBack)
            {
                DataBin();
                QX();
            }
        }

        //绑定友情连接列表
        public void DataBin() 
        {
            string name = txtTile.Text;
            string type = comxDis.Text;
            List<YouQinLianJi> list=yqbll.GetYouQin("", name, type);
            StoreList.DataSource = list.Select(x => new 
            {
                YouQinLianJiId = x.YouQinLianJiId,
                GsName = x.GsName,
                Url = x.Url,
                DispType = x.DispType,
                xh = x.xh
            });
            StoreList.DataBind();
        }

        public void Ref(object sender,StoreRefreshDataEventArgs e)
        {
            DataBin();
        }

        protected void QX() 
        {
            QuanXianBll qxbll = new QuanXianBll();
            int cdid = Convert.ToInt32(Request.QueryString["cdid"]);
            bntAdd.Visible = qxbll.IsHaveQx(cdid, "add");
            bntDel.Visible = qxbll.IsHaveQx(cdid, "delete");
            bntEid.Visible = qxbll.IsHaveQx(cdid, "edit");
        }
        protected void Add(object sender,DirectEventArgs e)
        {
            YouQinLianJi yq = new YouQinLianJi();
            yq.YouQinLianJiId = Guid.NewGuid().ToString();
            yq.GsName = textGsName.Text;
            yq.DispType = comType.SelectedItem.Value;
            yq.Url = textWZ.Text;
            yq.xh =Convert.ToInt32(txtXh.Text);
            if (filedTP.HasFile)
            {
                string path = "~/Image/youQing/";
                string name = Guid.NewGuid().ToString() + filedTP.PostedFile.FileName;
                string strFileName = Path.GetExtension(filedTP.PostedFile.FileName).ToUpper();//获取文件后缀
                if (!(strFileName == ".BMP" || strFileName == ".GIF" || strFileName == ".JPG"))
                {
                    X.Msg.Alert("提示信息", "文件格式不正确！").Show();
                    return;
                }
                string phts = Server.MapPath(path + name);
                filedTP.PostedFile.SaveAs(phts);
                yq.img = "../../Image/youQing/" + name;
            }
            if (yqbll.Add(yq) > 0)
            {
                window_add.Hide();
                DataBin();
                X.Msg.Notify("提示","添加成功！").Show();
            }
        }

        
        protected void Dele(object sender,DirectEventArgs e) 
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)grid.SelectionModel[0];
            if (s.SelectedRows.Count== 0) 
            {
                X.Msg.Alert("提示","请选择数据").Show();
                return;
            }
            string id = s.SelectedRow.RecordID;
            List<YouQinLianJi> list=yqbll.GetYouQin(id, "", "");
            string img = list[0].img;
            if (img != "" && img!=null)
            {
                string ims=Server.MapPath(img);
                if (File.Exists(ims)) 
                {
                    File.Delete(ims);
                }
            }
            if (yqbll.Dele(id) > 0)
            {
                DataBin();
                X.Msg.Notify("提示","删除成功！").Show();
            }
        }

        protected void EidShow(object sender,DirectEventArgs e)
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)grid.SelectionModel[0];
            if (s.SelectedRows.Count == 0)
            {
                X.Msg.Alert("提示", "请选择数据").Show();
                return;
            }
            string id = s.SelectedRow.RecordID;
            List<YouQinLianJi> list = yqbll.GetYouQin(id, "", "");
            string img = list[0].img;
            filedTP.Text = img;
            textGsName.Text = list[0].GsName;
            txtXh.Text = list[0].xh.ToString();
            textWZ.Text = list[0].Url;
            comType.SelectedItem.Value = list[0].DispType;
            window_add.Title = "修改友情连接";
            bntEids.Show();
            bntAdds.Hide();
            window_add.Show();

        }

        protected void EidSeve(object sender,DirectEventArgs e)
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)grid.SelectionModel[0];
            if (s.SelectedRows.Count == 0)
            {
                X.Msg.Alert("提示", "请选择数据").Show();
                return;
            }
            string id = s.SelectedRow.RecordID;
            List<YouQinLianJi> list = yqbll.GetYouQin(id, "", "");
            YouQinLianJi yq = new YouQinLianJi();
            string img = list[0].img;
            if (filedTP.HasFile)
            {
               string path = "~/Image/youQing/";
               string name = Guid.NewGuid().ToString() + filedTP.PostedFile.FileName;
               string strFileName = Path.GetExtension(filedTP.PostedFile.FileName).ToUpper();//获取文件后缀
               if (!(strFileName == ".BMP" || strFileName == ".GIF" || strFileName == ".JPG"))
               {
                   X.Msg.Alert("提示信息", "文件格式不正确！").Show();
                   filedTP.Text = img;
                   return;
               }
               string phts = Server.MapPath(path + name);
               filedTP.PostedFile.SaveAs(phts);
               yq.img = "../../Image/youQing/" + name;
               if (list[0].img!=""&&list[0].img!=null)
               {
                   string stoimg = Server.MapPath(img);
                   File.Delete(stoimg);
               }
              
            }
            else
            {
                yq.img = list[0].img;
            }
            yq.GsName = textGsName.Text;
            yq.Url = textWZ.Text;
            yq.xh =Convert.ToInt32(txtXh.Text);
            yq.DispType = comType.SelectedItem.Value;
            yq.YouQinLianJiId = id;
            if (yqbll.Eid(yq) > 0) 
            {
                X.Msg.Notify("提示","修改成功！").Show();
                window_add.Hide();
                DataBin();
            }
        }

        protected void Select(object sender,DirectEventArgs e) 
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)grid.SelectionModel[0];
            if (s.SelectedRows.Count == 0)
            {
                X.Msg.Alert("提示", "请选择数据").Show();
                return;
            }
            string id = s.SelectedRow.RecordID;
            List<YouQinLianJi> list = yqbll.GetYouQin(id, "", "");
            disName.Text = list[0].GsName;
            disUrl.Text = list[0].Url;
            comxDis.Text = list[0].DispType;
            disXh.Text = list[0].xh.ToString();
            disTp.Text = "<img  height='190' src='" + list[0].img + "' />";
            window_select.Show();
        }
    }
}