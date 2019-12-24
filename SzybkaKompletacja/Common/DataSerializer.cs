using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Global.Common
{

    /// <summary>
    /// Author: Adam
    /// Time: 2013-06-19
    /// Remark:序列化
    /// Version:0.0.0.1
    /// </summary>
    public class DataSerializer
    {
        #region 字    段

        #endregion

        #region 属    性


        #endregion

        #region 构造函数

        #endregion

        #region 基本方法

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="objectToConvert"></param>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        public static void PositiveSerializer(object objectToConvert, string path, Encoding encoding)
        {
            // 对象不为空
            if (objectToConvert != null)
            {
                Type t = objectToConvert.GetType();
                BinaryFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(path, FileMode.Create,
                FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, t);
                stream.Close();
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="path">路经加文件名</param>
        /// <param name="objectType">内容类型</param>
        /// <param name="encoding">编码类型</param>
        /// <returns></returns>
        public static object InsteadSerializer(string path, Type objectType, Encoding encoding)
        {
            object convertedObject = null;
            // 文件名不为空
            if (!string.IsNullOrEmpty(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(path, FileMode.Open,
                FileAccess.Read, FileShare.Read);
                convertedObject = formatter.Deserialize(stream);
                stream.Close();
            }
            return convertedObject;
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="objectToConvert"></param>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        public static void PositiveSerializerXml(object objectToConvert, string path, Encoding encoding)
        {
            // 对象不为空
            if (objectToConvert != null)
            {
                Type t = objectToConvert.GetType();
                //t = typeof(ArrayList).GetType();
                XmlSerializer ser = new XmlSerializer(t);
                using (StreamWriter writer = new StreamWriter(path, false, encoding))
                {
                    ser.Serialize(writer, objectToConvert);
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="path">路经加文件名</param>
        /// <param name="objectType">内容类型</param>
        /// <param name="encoding">编码类型</param>
        /// <returns></returns>
        public static object InsteadSerializerXml(string path, Type objectType, Encoding encoding)
        {
            object convertedObject = null;
            // 文件名不为空
            if (!string.IsNullOrEmpty(path))
            {
                //objectType = typeof(ArrayList).GetType();
                XmlSerializer ser = new XmlSerializer(objectType);
                using (StreamReader reader = new StreamReader(path, encoding))
                {
                    convertedObject = ser.Deserialize(reader);
                    reader.Close();
                }
            }
            return convertedObject;
        }

        /// <summary>
        /// 对象序列化成 XML String
        /// List<string> al = new List<string>();
        /// al.Add("xsm");
        /// string xml = Serializer.XmlSerialize<List<string>>(strPath, Encoding.UTF8, al);
        /// al = Serializer.XmlDeserialize<List<string>>(strPath, Encoding.UTF8);
        /// </summary>
        public static string XmlSerialize<T>(T obj)
        {
            string xmlString = string.Empty;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                xmlSerializer.Serialize(ms, obj);
                xmlString = Encoding.UTF8.GetString(ms.ToArray());
            }
            //File.WriteAllText(path, xmlString, encoding);
            return xmlString;
        }

        /// <summary>
        /// XML String 反序列化成对象
        /// List<string> al = new List<string>();
        /// al.Add("xsm");
        /// string xml = Serializer.XmlSerialize<List<string>>(strPath, Encoding.UTF8, al);
        /// al = Serializer.XmlDeserialize<List<string>>(strPath, Encoding.UTF8);
        /// </summary>
        public static T XmlDeserialize<T>(string xmlString)
        {
            T t = default(T);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
            {
                using (XmlReader xmlReader = XmlReader.Create(xmlStream))
                {
                    Object obj = xmlSerializer.Deserialize(xmlReader);
                    t = (T)obj;
                }
            }
            return t;
        }

        #endregion

        #region 其他方法

        #endregion

    }
}
