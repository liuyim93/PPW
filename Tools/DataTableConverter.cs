using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Data;
using Newtonsoft.Json;

namespace Tools
{
    public class DataTableConverter
    {
        public override void WriteJson(JsonWriter writer, object value)
        {
            DataTable dt = (DataTable)value;

            writer.WriteStartArray();
            foreach (DataRow dr in dt.Rows)
            {
                writer.WriteStartObject();
                foreach (DataColumn dc in dt.Columns)
                {
                    writer.WritePropertyName(dc.ColumnName);
                    writer.WriteValue(dr[dc].ToString());
                }
                writer.WriteEndObject();
            }
            writer.WriteEndArray();


        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(DataTable).IsAssignableFrom(objectType);
        }  
    }
}
