using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SeaCodeLib.Common
{
    public class StringHelper
    {

        /// <summary>
        /// 从字符串中提取邮箱地址返回数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> MatchEmailAddr(string str)  
        {
            Regex regEmail = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            MatchCollection mc = regEmail.Matches(str);
            List<string> ls = new List<string>();
            foreach (Match m in mc)
            {
                ls.Add(m.Value);
            }

            return ls;
            
        }

        //从字符串中返回整型数组如：12，345，28，94
        public static List<int> GetIntListFromStr(string str, char separator)
        {
            string[] arr = str.Split(separator);
            int len = arr.Length;
            List<int> rv = new List<int>();
            for (int i = 0; i < len; i++)
            {
                rv.Add(arr[i].Trim().ConvertValue<int>());
            }
            return rv;
        }

        public static List<double> GetDoubleListFromStr(string str, char separator)
        {
            string[] arr = str.Split(separator);
            int len = arr.Length;
            List<double> rv = new List<double>();
            for (int i = 0; i < len; i++)
            {
                rv.Add(arr[i].Trim().ConvertValue<double>());
            }
            return rv;
        }

        public static double[] GetDoubleFromStr(string str, char separator)
        {
            string[] arr = str.Split(separator);
            int len = arr.Length;
            double[] rv = new double[len];
            for (int i = 0; i < len; i++)
            {
                rv[i] = arr[i].Trim().ConvertValue<double>();
            }
            return rv;
        }

    }
}
