using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

using System.IO;
using System.Text;


namespace KpInfohelp
{
    public enum DTFormate
    {
        SHORT_EN_US,
        SHORT_ZH_CN,
        LONG_EN_US,
        LONG_ZH_CN
    }


    public class DataTypeConvert
    {

        public static short ToInt16(object value)
        {
            return ToInt16(value, 0);
        }
        public static short ToInt16(object value, short defautValue)
        {
            short r = 0;

            if (value == null) return r;

            if (short.TryParse(value.ToString(), out r))
                return r;
            else
                return defautValue;
        }
        public static int ToInt32(object value)
        {
            return ToInt32(value, 0);
        }
        public static int ToInt32(object value, int defautValue)
        {
            Int32 r = 0;

            if (value == null) return r;

            if (int.TryParse(value.ToString(), out r))
                return r;
            else
                return defautValue;
        }
        public static long ToInt64(object value)
        {
            return ToInt64(value, 0);
        }
        public static long ToInt64(object value, long defautValue)
        {
            Int64 r = 0;

            if (value == null) return r;

            if (long.TryParse(value.ToString(), out r))
                return r;
            else
                return defautValue;
        }
        public static double ToDouble(object value)
        {
            return ToDouble(value, 0);
        }
        public static double ToDouble(object value, Double defautValue)
        {
            Double r = 0;

            if (value == null) return r;

            if (Double.TryParse(value.ToString(), out r))
                return r;
            else
                return defautValue;
        }
        public static decimal ToDecimal(object value)
        {
            return Decimal(value, 0);
        }
        public static decimal Decimal(object value, Decimal defautValue)
        {
            decimal r = 0;

            if (value == null) return r;

            if (decimal.TryParse(value.ToString().Replace(".",","), out r))
                return r;
            else
                return defautValue;
        }
        public static bool ToBoolean(object value)
        {
            Boolean r = false;

            if (value == null) return r;

            Boolean.TryParse(value.ToString(), out r);

            return r;
        }
        public static bool ToBoolean(object value, Boolean defautValue)
        {
            Boolean r = false;

            if (value == null) return r;

            if (Boolean.TryParse(value.ToString(), out r))
                return r;
            else
                return defautValue;
        }
        public static DateTime ToDateTime(object value)
        {
            DateTime r = DateTime.MinValue;

            if (value == null) return r;

            if (DateTime.TryParse(value.ToString(), out r)) return r;

            return r;
        }
        public static DateTime? ToDateTimeOrNull(object value)
        {
            DateTime r = DateTime.MinValue;

            if (value == null) return null;

            if (DateTime.TryParse(value.ToString(), out r))
                return r;
            else
                return null;
        }
        public static String DateTimeToString(Object value)
        {
            return DateTimeToString(value, DTFormate.SHORT_EN_US);
        }
        public static String DateTimeToString(Object value, DTFormate dtFormate)
        {
            if (value == null) return String.Empty;

            DateTime m_dt = (DateTime)value;

            switch (dtFormate)
            {
                case DTFormate.SHORT_EN_US: return m_dt.ToString("yyyy-MM-dd");
                case DTFormate.SHORT_ZH_CN: return m_dt.ToString("yyyy年MM月dd日");
                case DTFormate.LONG_EN_US: return m_dt.ToString("yyyy-MM-dd hh：mm：ss");
                case DTFormate.LONG_ZH_CN: return m_dt.ToString("yyyy年MM月dd日hh时mm分ss秒");
                default: return m_dt.ToString("yyyy-MM-dd");
            }
        }

        public static DataSet ToDataSet<T>(IList<T> list)
        {
            var elementType = typeof(T);
            var ds = new DataSet();
            var t = new DataTable();
            ds.Tables.Add(t);

            if (elementType.IsValueType)
            {
                var colType = Nullable.GetUnderlyingType(elementType) ?? elementType;
                t.Columns.Add(elementType.Name, colType);

            }
            else
            {
                //add a column to table for each public property on T
                foreach (var propInfo in elementType.GetProperties())
                {
                    var colType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;
                    t.Columns.Add(propInfo.Name, colType);
                }
            }

            //go through each property on T and add each value to the table
            foreach (var item in list)
            {
                var row = t.NewRow();

                if (elementType.IsValueType)
                {
                    row[elementType.Name] = item;
                }
                else
                {
                    foreach (var propInfo in elementType.GetProperties())
                    {
                        row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                    }
                }
                t.Rows.Add(row);
            }

            return ds;
        }
        public static DataTable ConvertToDataTable<T>(IList<T> list)
        {
            Type tp = typeof(T);
            PropertyInfo[] infoArray = tp.GetProperties();
            DataTable tb = new DataTable();
            foreach (var item in list)
            {
                DataRow dr = tb.NewRow();

                foreach (var info in infoArray)
                {
                    if (!tb.Columns.Contains(info.Name)) tb.Columns.Add(info.Name, info.PropertyType);

                    dr[info.Name] = info.GetValue(item, null);
                }
                tb.Rows.Add(dr);

            }
            return tb;
        }
        public static DataTable ConvertToDataTable(IList list)
        {
            DataTable tb = new DataTable();
            foreach (Object[] item in list)
            {
                DataRow dr = tb.NewRow();
                for (int i = 0; i < item.Length; i++)
                {
                    if (!tb.Columns.Contains(i.ToString())) tb.Columns.Add(i.ToString());

                    dr[i.ToString()] = item[i];
                }
                tb.Rows.Add(dr);
            }
            return tb;
        }
        public static String ConvertToString<T>(IList<T> list, String PropertyName)
        {
            if (list == null || list.Count <= 0) return String.Empty;

            if (String.IsNullOrEmpty(PropertyName)) return String.Empty;

            Type tp = typeof(T);

            PropertyInfo[] infoArray = tp.GetProperties();

            PropertyInfo propertyInfo = null;

            foreach (var p in infoArray)
            {
                if (p.Name.ToLower() == PropertyName.ToLower())
                    propertyInfo = p;
            }

            if (propertyInfo == null) return String.Empty;

            StringBuilder builder = new StringBuilder();

            foreach (var item in list)
            {
                //       builder.AppendFormat("{0},", ReflectionHelper.GetValue(item, propertyInfo));
            }

            builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }

        public static List<T> ToList<T>(IList<T> list)
        {
            List<T> collection = new List<T>();
            foreach (var item in list)
            {
                collection.Add(item);
            }
            return collection;
        }

        public static String[] ToArray(String value, char split)
        {
            if (String.IsNullOrEmpty(value)) return new string[0];

            return value.Split(split);
        }

        public static Dictionary<String, String> ToDictionary(String value)
        {
            return ToDictionary(value, '&', '=');
        }

        public static Dictionary<String, String> ToDictionary(String value, char split, char split1)
        {
            Dictionary<String, String> collection = new Dictionary<string, string>();

            if (!String.IsNullOrEmpty(value))
            {
                var tmpCollection = ToArray(value, split);

                foreach (var t in tmpCollection)
                {
                    var tmp = ToArray(value, split1);

                    if (tmp != null && tmp.Length >= 2)
                        collection[tmp[0]] = tmp[1];
                }
            }
            return collection;
        }
        public static string WytnijDO(string s, char doStr)
        {
            string st = string.Empty;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == doStr)
                {
                    st = s.Substring(0, i);
                    break;
                }
                //  sub = s.Substring(i, 1);
            }
            return st;
        }
        public static Decimal ConvertFrameUdpToDecimal(string recData)
        {
            string frame =string.Empty;
            decimal r = 0;
             if(recData.Substring(0,1)=="M")  //jezeli masa 
            {
                frame = FrameGet(recData);
                frame = RamkaMasy(frame);
            }
            if (decimal.TryParse(frame.ToString(), out r))
                return r;
            else
                return 0;
          
        }
        private static string RamkaMasy(string Frame)
        {
          string MessageTemp = string.Empty;
            MessageTemp = Frame.Substring(1, Frame.Length - 1);
            return MessageTemp;


        }


        private static string FrameGet(string Frame)
        {
            string DefineSend = string.Empty;
            string MessageTemp = string.Empty;
            string spr = string.Empty;
            int Height;
            int CRC = 0;
            string CRCtemp;
            int i;

            DefineSend = Frame.Substring(0, 1);
            spr = Frame.Substring(1, 1);
            Height = Char.ConvertToUtf32(spr,0);
        //    Height = Convert.ToInt16(Frame.Substring(1, 1));

           
            for (i = 2; i < Height + 2; i++)
            {
                MessageTemp += Frame.Substring(i, 1);
            }

        
        //    CRCtemp = Frame.Substring((Height + 3), (Height + 4));

            return MessageTemp;
        }
        public static Decimal ConvertFrameToDecimal(string frameType, string recData)
        {
            decimal r = 0;
            switch (frameType)
            {
                case "Radwag":
                    //if (recData == null) return 0;
                    recData = recData.Replace("?", "");
                    recData = recData.Replace("ES", "");
                    recData = recData.Replace("SI", "");
                    recData = recData.Replace(" ", "");
                    recData = recData.Replace("kg", "");
                    recData = recData.Replace(".",",");

                    if (decimal.TryParse(recData, out r))
                            return r;
                        else
                            return 0;
                case "Udp":
                    //if (recData == null) return 0;
                    recData = recData.Replace("?", "");
                    recData = recData.Replace("ES", "");
                    recData = recData.Replace("SI", "");
                    recData = recData.Replace(" ", "");
                    recData = recData.Replace("kg", "");
                    recData = recData.Replace(".", ",");

                    if (decimal.TryParse(recData, out r))
                        return r;
                    else
                        return 0;
                case "Axis":
                    //if (recData == null) return 0;
                    recData = recData.Replace(" ", "");
                    recData = recData.Replace("g", "");
                    recData = recData.Replace(".", ",");

                    if (decimal.TryParse(recData, out r))
                        return r;
                    else
                        return 0;
                case "Rinstrum":
                    //if (recData == null) return 0;

                    //robimy readto 
                    string s = string.Empty;
                    recData = WytnijDO(recData, 'G');
                    recData = recData.Replace("81050026:", "");
                    recData = recData.Replace("\u0002", "");
                    recData = recData.Replace("\u0003", "");
                    recData = recData.Replace("?", "");
                    recData = recData.Replace("kg", "");
                    recData = recData.Replace(" ", "");
                    recData = recData.Replace("", "");
                    recData = recData.Replace("M", "");
                    recData = recData.Replace("G", "");
                    recData = recData.Replace("N", "");
                    recData = recData.Replace("", "");
                    recData = recData.Replace("", "");
                    recData = recData.Replace("", "");
                    recData = recData.Replace("", "");
                    if (decimal.TryParse(recData, out r))
                                              return r;
                    else
                        return 0;
                case "Rinstrum2":
                    //if (recData == null) return 0;
                    recData = recData.Replace("81050026:", "");
                    recData = recData.Replace("?", "");
                    recData = recData.Replace("k", "");
                    recData = recData.Replace("g", "");
                    recData = recData.Replace(" ", "");
                    recData = recData.Replace("", "");
                    recData = recData.Replace("kg", "");
                    recData = recData.Replace("M", "");
                    recData = recData.Replace("G", "");
                    recData = recData.Replace("", "");
                    recData = recData.Replace("", "");
                    recData = recData.Replace("", "");
                    recData = recData.Replace(" ", "");
                    if (decimal.TryParse(recData, out r))
                        return r;


                    else
                        return 0;
                default:
                    return 0;
            }
    }

        public static String FormateData(String data, String c, int length)
        {
            if (data.Length < length)
            {
                var num = length - data.Length;
                for (int i = 0; i < num; i++)
                {
                    data = c + data;
                }
            }
            return data;
        }
        public static String Substring(String data, int length, String other)
        {
            if (data.Length > length)
            {
                data = data.Substring(0, length) + other;
            }

            return data;
        }
        public static IList<TreePoint> ToTreePoint(DataTable source)
        {
            return ToTreePoint(source, null, null, null, null);
        }
        public static IList<TreePoint> ToTreePoint(DataTable source, String id, String pId, String name, String other)
        {
            IList<TreePoint> collection = new List<TreePoint>();

            if (source == null) return collection;

            if (String.IsNullOrEmpty(id)) id = "id";
            if (String.IsNullOrEmpty(pId)) pId = "pId";
            if (String.IsNullOrEmpty(name)) name = "name";
            if (String.IsNullOrEmpty(other)) other = "other";

            foreach (DataRow item in source.Rows)
            {
                if (item[id] == null || item[pId] == null || item[name] == null) continue;

                if (item[other] == null)
                    collection.Add(new TreePoint(item[id].ToString(), item[pId].ToString(), item[name].ToString()));
                else
                    collection.Add(new TreePoint(item[id].ToString(), item[pId].ToString(), item[name].ToString(), item[other].ToString()));
            }

            return collection;
        }
        public static String GetGuid()
        {
            return Guid.NewGuid().ToString();
        }
    
    }

    public class TreePoint
    {
        public TreePoint()
        {

        }

        public TreePoint(String id, String pId, String name)
        {
            this.id = id;
            this.pId = pId;
            this.name = name;
            other = String.Empty;
        }

        public TreePoint(String id, String pId, String name, String other)
        {
            this.id = id;
            this.pId = pId;
            this.name = name;
            this.other = other;
        }

        public String id { get; set; }

        public String pId { get; set; }

        public String name { get; set; }

        public String other { get; set; }

    }
}
