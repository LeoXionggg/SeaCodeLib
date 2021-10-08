using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaCodeLib.Common
{
    /// <summary>
    /// 字符序列升位处理
    /// </summary>
    public class CharSequence
    {
        /// <summary>
        /// 给传入的字母升位如： 
        /// A->B
        /// Z->AA
        /// AA->AB
        /// AB->AC
        /// ZZ-AAA
        /// </summary>
        /// <param name="baseStr"></param>
        /// <returns></returns>
        public static string CharBitIncrease(string baseStr)
        {
            if (string.IsNullOrWhiteSpace(baseStr))
            {
                return "A";
            }
            int up = GetInt(baseStr);
            up++;

            return GetCharacter(up);
        }

        /// <summary>
        /// 根据数值得到对应的字母序列
        /// 1->A 2->B ... 26->Z 27->AA 28->AB ...
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static string GetCharacter(int n)
        {
            char[] buf = new char[(int)Math.Floor(Math.Log(25 * (n + 1)) / Math.Log(26))];
            for (int i = buf.Length - 1; i >= 0; i--)
            {
                n--;
                buf[i] = (char)('A' + n % 26);
                n /= 26;
            }
            return new string(buf);
        }

        /// <summary>
        /// 根据字母序列得到对应的数值
        /// </summary>
        /// <param name="baseStr"></param>
        /// <returns></returns>
        private static int GetInt(string baseStr)
        {
            char[] cc = baseStr.ToUpper().ToCharArray();
            int len = cc.Length;
            double db = 0;
            for (int i = 0; i < len; i++)
            {
                char c = cc[len - i - 1];
                db += Math.Pow(26, i) * (c - 'A' + 1);
            }

            return (int)db;
        }
    }
}
