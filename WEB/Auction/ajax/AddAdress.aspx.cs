using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BLL.SystemSeting;
using Model.Entities;
using System.Web.Script.Serialization;

namespace WEB.Auction.ajax
{
    public partial class AddAdress : System.Web.UI.Page
    {
        ShouHuoDZBll adressBll = new ShouHuoDZBll();
        JavaScriptSerializer serialize = new JavaScriptSerializer();
        protected void Page_Load(object sender, EventArgs e)
        {                        
            string jsonStr = "";
            if (HttpContext.Current.Session["HuiYuanID"]==null) {
                
            }
            else if(Request.QueryString["name"]!=null&&Request.QueryString["adress"]!=null&&Request.QueryString["phone"]!=null&&Request.QueryString["code"]!=null){
                string hyId=HttpContext.Current.Session["HuiYuanID"].ToString();
                string name = Request.QueryString["name"];
                string adress = Request.QueryString["adress"];
                string phone = Request.QueryString["phone"];
                string code = Request.QueryString["code"];
                string remark = Request.QueryString["remark"];
                ShouHuoDZ adr = new ShouHuoDZ();
                adr.ShouHuoName = name;
                adr.DZ = adress;
                adr.Mode = phone;
                adr.CreateTime = DateTime.Now;
                adr.YouBian = code;
                adr.Remark = remark;
                adr.HuiYuanID = hyId;
                adressBll.AddShouHuoDZ(adr);
                List<ShouHuoDZ> list = adressBll.GetShouHuoDZbyhyId(hyId);
                jsonStr = serialize.Serialize(list);
            }
            Response.Clear();
            Response.Write(jsonStr);
            Response.End();
        }
    }
}