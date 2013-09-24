using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Entities;
using Model;

namespace BLL.Home
{
    /*
     * 张彤彤
     */
    public class SystemParamsBll
    {
        PPWContext db = new PPWContext();

        //public string GetSystemParamsByKey(string key)
        //{
        //    var data = db.SystemParams.Where(s=>s.key == key);
        //    if (data.Count() >0)
        //    {
        //        return data.First().Value;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public void SetSystemParamsByKey(string key, string value)
        //{
        //    var data = db.SystemParams.Where(s => s.key == key);
        //    if (data.Count() > 0)
        //    {
        //        SystemParam sp = data.First();
        //        sp.Value = value;
        //        db.SaveChanges();
        //    }
        //}

        //public SystemParam GetSystemParamByKey(string key)
        //{
        //    var data = db.SystemParams.Where(s=>s.key == key);
        //    if (data.Count()>0)
        //    {
        //        return data.First();
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}
