using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Script.Serialization;

namespace Tools
{
    public class Resolve
    {
        /// <summary>  
        /// JSON文本转对象,泛型方法  
        /// </summary>  
        /// <typeparam name="T">类型</typeparam>  
        /// <param name="jsonText">JSON文本</param>  
        /// <returns>指定类型的对象</returns>  
        public static T JSONToObject<T>(string jsonText, HttpContext context)  
        {  
            JavaScriptSerializer jss = new JavaScriptSerializer();  
            try  
            {  
                return jss.Deserialize<T>(jsonText);  
            }  
            catch (Exception ex)  
            {  
                return default(T);  
            }  
        }  
  
        /// <summary>  
        /// 将JSON文本转换成数据行  
        /// </summary>  
        /// <param name="jsonText">JSON文本</param>  
        /// <returns>数据行的字典</returns>  
        public static Dictionary<string, object> DataRowFromJSON(string jsonText, HttpContext context)  
        {  
            return JSONToObject<Dictionary<string, object>>(jsonText, context);  
        }  
    }      
}
