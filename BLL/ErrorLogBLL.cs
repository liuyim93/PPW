using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Model.Entities;
using Tools;
using System.Web;
using BLL.Home;

namespace BLL
{
    public class ErrorLogBLL:BaseBll
    {
        private LoginBll loginBll = new LoginBll();

        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <param name="errorDetails"></param>
        /// <param name="remark"></param>
        public void WriteErrorLog(string errorDetails, string remark)
        {
            YongHu yh=loginBll.GetYongHu((HttpContext.Current.Session["userid"] ?? "-1").ToString());
            ErrorLog errorLog = new ErrorLog()
            {
                id=Guid.NewGuid().ToString(),
                logTime=DateTime.Now,
                uid = yh==null?"":yh.YongHuId,
                ip = HttpContext.Current.Request.UserHostAddress,
                url = HttpContext.Current.Request.Url.ToString(),
                errorDetails = errorDetails.SqlSafe(),
                remark = remark.SqlSafe()
            };
            db.ErrorLogs.Add(errorLog);
            db.SaveChanges();
        }
    }
}
