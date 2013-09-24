using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.SystemSeting;
using Tools;
using Ext.Net;
namespace WEB.SystemSeting.XiaoShou
{
    public partial class DingDan : BasePage
    {
        DingDanBll DinDanbll = new DingDanBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack&&!X.IsAjaxRequest)
            {
                DataBindList();
            }
        }

        protected void DataBindList()
        {
            string bh = txtBH.Text;
            string st = cmdStat.SelectedItem.Value;
            DateTime? beg = xtxtbegin.GetTime();
            DateTime? end = xtxtbend.GetTime();

            List<DinDans> list = DinDanbll.GetDinDan(bh, beg, end, st);
            storList.DataSource = list.Select(x => new
            {
                DingDanID = x.DingDanID,
                DingDanBH = x.DingDanBH,
                HuiYuanID = x.HuiYuanID,
                ProductID = x.ProductID,
                DingDanTime = x.DingDanTime == null ? "" : x.DingDanTime.Value.ToString("yyyy-MM-dd"),
                ShouHuoName = x.ShouHuoName,
                Mode = x.Mode,
                DZ = x.DZ,
                YouBian = x.YouBian,
                Status = (Status)x.Status,
                OrderTypeID=x.OrderTypeID
            });
            storList.DataBind();
        }

        protected void SetSata(object sender,DirectEventArgs e) 
        {
            CheckboxSelectionModel s = (CheckboxSelectionModel)Grid.SelectionModel[0];
            if (s.SelectedRows.Count==0)
            {
                X.Msg.Alert("提示","请选择数据！").Show();
                return;
            }
            string id = s.SelectedRow.RecordID;
            string st = ((int)Status.已发货).ToString();
            if (DinDanbll.Eid(id, st) > 0) 
            {
                X.Msg.Notify("提示","发货成功！").Show();
                DataBindList();
            }
        }

        protected void Ref(object sender,StoreRefreshDataEventArgs e) 
        {
            DataBindList();
        }

    }
}