using System;

namespace SeaCodeLib.Common
{
    public static class GoConvert
    {


        public static T ConvertValue<T>(this string obj) where T : struct
        {
            try
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            catch
            {
                if (typeof(T) == typeof(DateTime))
                {
                    obj = "1900-01-01";
                    return (T)Convert.ChangeType(obj, typeof(T));
                }
                return default(T);
            }

        } 

    }
}
