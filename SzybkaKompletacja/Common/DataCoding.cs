using System;
using System.Collections.Generic;

using System.Text;

namespace Global.Common
{
    public class DataCoding
    {
        #region 字    段

        #endregion

        #region 属    性


        #endregion

        #region 构造函数

        #endregion

        #region 基本方法

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static byte[] GetBytes(string value)
        {
            if (value == null) value = String.Empty;

            return Encoding.UTF8.GetBytes(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inBuffer"></param>
        /// <returns></returns>
        public static string GetString(byte[] inBuffer)
        {
            if (inBuffer == null) return string.Empty;

            return Encoding.UTF8.GetString(inBuffer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inBuffer"></param>
        /// <returns></returns>
        public static string GetString(byte[] inBuffer, int index, int length)
        {
            if (inBuffer == null) return string.Empty;

            return Encoding.UTF8.GetString(inBuffer, index, length);
        }

        #endregion

        #region 其他方法

        #endregion  
    }
}
