using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL.SystemSeting;
using Tools;
using Ext.Net;
using BLL;

namespace WEB.SystemSeting.XiaoShou
{
    public partial class DingDan : BasePage
    {
        DingDanBll DinDanbll = new DingDanBll();
        ShouHuoDZBll AdressBll = new ShouHuoDZBll();
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
            string orderTypeId = cboxType.SelectedItem.Value;

            List<Model.Entities.DingDan> list = DinDanbll.GetOrder(bh, beg, end, st,orderTypeId);
            if (list.Count>0)
            {
                storList.DataSource = list.Select(x => new
                {
                    DingDanID = x.DingDanID,
                    DingDanBH = x.DingDanBH,
                    HuiYuan = x.HuiYuanName,
                    ProductName = x.ProductName,
                    DingDanTime = x.DingDanTime == null ? "" : x.DingDanTime.Value.ToString("yyyy-MM-dd"),
                    ShouHuoName = x.ShouHuoDZID == "" ? "" : AdressBll.GetShouHuoDZ(x.ShouHuoDZID)[0].ShouHuoName,
                    Mode = x.ShouHuoDZID == "" ? "" : AdressBll.GetShouHuoDZ(x.ShouHuoDZID)[0].Mode,
                    DZ = x.ShouHuoDZID == "" ? "" : AdressBll.GetShouHuoDZ(x.ShouHuoDZID)[0].DZ,
                    YouBian = x.ShouHuoDZID == "" ? "" : AdressBll.GetShouHuoDZ(x.ShouHuoDZID)[0].YouBian,
                    Status = (Status)x.Status,
                    OrderType = x.OrderType,
                    TotalPrice = x.TotalPrice.ToString()
                });
                storList.DataBind();
            }
            storeOrderType.DataSource = DinDanbll.GetAllOrderType();
            storeOrderType.DataBind();
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