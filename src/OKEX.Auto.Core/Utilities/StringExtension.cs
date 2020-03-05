using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace OKEX.Auto.Core.Utilities
{
    public static class StringExtension
    {
        /// <summary>
        /// 判断字符串是否为空。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 判断字符串是否为空。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 字符串分割成字符串数组。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator">分隔符，默认为逗号。</param>
        /// <returns></returns>
        public static string[] ToStrArray(this string str, string separator = ",")
        {
            return str.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 是否为数字(正则错了，不要用)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string str)
        {
            if (str.IsNullOrWhiteSpace()) return false;
            return Regex.IsMatch(str, @"^\d+$");
        }

        /// <summary>
        /// 是否为数字（包括两位小数）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDecimal(this string str)
        {
            if (str.IsNullOrWhiteSpace()) return false;
            return Regex.IsMatch(str, @"^[0-9]+(.[0-9]{1,2})?$");
        }

        /// <summary>
        /// 生产随机数
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string GetRandomString(int length)
        {
            string str = "0123456789";
            char[] ch = new char[length];
            Random r = new Random(System.Guid.NewGuid().GetHashCode());
            for (int i = 0; i < ch.Length; i++)
            { ch[i] = str[r.Next(0, str.Length)]; }
            return new string(ch);
        }

        /// <summary>
        /// 生成ID编号
        /// </summary>
        /// <returns></returns>
        private static readonly object IdLock = new object();
        public static string NewIdFromDateTickets()
        {
            string newid;
            lock (IdLock)
            {
                Thread.Sleep(1);
                newid = DateTime.Now.ToString("yyMMssffff") + GetRandomString(4);
            }
            return newid;
        }

        /// <summary> 
        /// 获取时间戳 
        /// </summary> 
        /// <returns></returns> 
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static DateTime MillisecondToDateTime(long milliseconds)
        {
            var baseDate = new DateTime(1970, 1, 1);
            var spanTime = TimeSpan.FromMilliseconds(milliseconds) + TimeSpan.FromHours(8);
            return baseDate + spanTime;
        }
    }

    public static class StringHelper
    {
        public static bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?/d*[.]?/d*$");
        }

        public static bool IsInt(string value)
        {
            return Regex.IsMatch(value, @"^\d+$");
        }

        public static bool IsUnsign(string value)
        {
            return Regex.IsMatch(value, @"^/d*[.]?/d*$");
        }
        //^[-]?(/d+/.?/d*|/./d+)$
        public static bool IsNumber(string value)
        {
            return Regex.IsMatch(value, @"^[-]?(/d+/.?/d*|/./d+)$");
        }
    }
}
