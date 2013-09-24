using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools
{

    public static class Common
    {
        /// <summary>
        /// 防止sql注入
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SqlSafe(this string str)
        {
            return (str??"").Trim().Replace("'","''");
        }
    }
}
