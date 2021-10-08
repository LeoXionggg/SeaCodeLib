using System;

namespace SeaCodeLib.Common
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum obj)
        {
            try
            {
                string objName = obj.ToString();
                Type t = obj.GetType();
                System.Reflection.FieldInfo fi = t.GetField(objName);
                if (fi == null)
                {
                    return "";
                }
                System.ComponentModel.DescriptionAttribute[] arrDesc = (System.ComponentModel.DescriptionAttribute[])fi.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                return arrDesc.Length > 0 ? arrDesc[0].Description : "";
            }
            catch
            {
                return obj.ToString();
            }
        }
        public static void GetEnum<T>(string a, ref T t)
        {
            foreach (T b in Enum.GetValues(typeof(T)))
            {
                if (GetDescription(b as Enum) == a)
                    t = b;
            }
        }
    }
}
