using System;
using System.Globalization;

namespace SeaCodeLib.Common
{
    public enum DWMQYType
    {
        /// <summary>
        /// 天
        /// </summary>
        Day = 1,

        /// <summary>
        /// 周
        /// </summary>
        Week = 2,

        /// <summary>
        /// 月
        /// </summary>
        Month = 3,

        /// <summary>
        /// 季
        /// </summary>
        Quarter = 4,

        /// <summary>
        /// 年
        /// </summary>
        Year = 5

    }


    /// <summary>
    /// 常用日期值方式
    /// 年：2018   
    /// 季：201801    2018年第1季度
    /// 月：201810    2018年10月
    /// 周：201837    2018年第37周
    /// 天：20180203  2018年2月3日
    /// </summary>
    public class DWMQYDate
    {

        private DWMQYDate() { }


        public DWMQYDate(DWMQYType dtype, int dateval)
        {
            DateType = dtype;
            DateValue = dateval;
            CheckValue();
        }

        public DWMQYDate(int dtype, int dateval)
        {
            if (!Enum.IsDefined(typeof(DWMQYType), dtype))
            {
                throw new Exception("dtype类型值错误");
            }
            DWMQYType dwmqyType = (DWMQYType)dtype;
 
            DateType = dwmqyType;
            DateValue = dateval;

            CheckValue();
        }

        public DWMQYDate(int dtype, int yearval, int afterval)
        {
            if (!Enum.IsDefined(typeof(DWMQYType), dtype))
            {
                throw new Exception("dtype类型值错误");
            }
            DWMQYType dwmqyType = (DWMQYType)dtype;
            int dateval = dwmqyType == DWMQYType.Year ? yearval : (yearval * 100 + afterval);

            DateType = dwmqyType;
            DateValue = dateval;

            CheckValue();
        }


        private void CheckValue()
        {
            if (DateValue <= 0)
            {
                throw new Exception("数据值错误，不应小于等于0！");
            }
            int y = 0, q = 0, m = 0, w = 0, d = 0;
            switch (DateType)
            {
                case (DWMQYType.Day):
                    y = Year;
                    if (y < 1900 || y > 6000)
                    {
                        throw new Exception(string.Format("Year值({0})错误！", y));
                    }
                    m = Month;
                    if (m < 1 || m > 12)
                    {
                        throw new Exception(string.Format("Month值({0})错误！", m));
                    }
                    d = Day;
                    if (d < 1 || d > DateTime.DaysInMonth(y, m))
                    {
                        throw new Exception(string.Format("{0}年{1}月的Day值({2})错误！", y, m, d));
                    }
                    break;
                case (DWMQYType.Month):
                    y = Year;
                    if (y < 1900 || y > 6000)
                    {
                        throw new Exception(string.Format("Year值({0})错误！", y));
                    }
                    m = Month;
                    if (m < 1 || m > 12)
                    {
                        throw new Exception(string.Format("Month值({0})错误！", m));
                    }                     
                    break;
                case (DWMQYType.Quarter):
                    y = Year;
                    if (y < 1900 || y > 6000)
                    {
                        throw new Exception(string.Format("Year值({0})错误！", y));
                    }
                    q = Quarter;
                    if (q < 1 || q > 4)
                    {
                        throw new Exception(string.Format("Quarter值({0})错误！", q));
                    }
                    break;
                case (DWMQYType.Week):
                    y = Year;
                    if (y < 1900 || y > 6000)
                    {
                        throw new Exception(string.Format("Year值({0})错误！", y));
                    }
                    w = Week;
                    int wofy = GetYearWeekCount(y);
                    if (w < 1 || w > wofy)
                    {
                        throw new Exception(string.Format("Week({0})错误,{1}年的周范围1-{2}！", w, y, wofy));
                    }
                    break;
                case (DWMQYType.Year):
                    y = Year;
                    if (y < 1900 || y > 6000)
                    {
                        throw new Exception(string.Format("Year值({0})错误！", y));
                    }                    
                    break;
                default:
                    throw new Exception("Date Type error!");
            }
        }

        /// <summary>
        /// 反应需要类型的当前值
        /// </summary>
        /// <param name="dType">类型</param>
        /// <returns></returns>
        public static DWMQYDate GetNow(DWMQYType dType)
        {
            DateTime dt = DateTime.Now;
            switch (dType)
            {
                case (DWMQYType.Day):                    
                    return new DWMQYDate(dType, dt.Year * 10000 + dt.Month * 100 + dt.Day);
                case (DWMQYType.Month):
                    return new DWMQYDate(dType, dt.Year * 100 + dt.Month);
                case (DWMQYType.Quarter): 
                    return new DWMQYDate(dType, dt.Year * 100 + GetQuarterOfYear(dt));
                case (DWMQYType.Week):
                    return new DWMQYDate(dType, dt.Year * 100 + GetWeekOfYear(dt));                    
                case (DWMQYType.Year):
                    return new DWMQYDate(dType, dt.Year);
                default:
                    throw new Exception("Date Type error!");
            }
        }

        /// <summary>
        /// 下一值
        /// </summary>
        /// <returns></returns>
        public DWMQYDate GetNext()
        {
            int y = 0, q = 0, m = 0, w = 0, d = 0;
            DateTime dt = new DateTime();
            switch (DateType)
            {
                case (DWMQYType.Day):
                    y = Year;
                    m = Month;
                    d = Day;
                    dt = new DateTime(y, m, d);
                    dt.AddDays(1);
                    return new DWMQYDate(DateType, dt.Year * 10000 + dt.Month * 100 + dt.Day);
                case (DWMQYType.Month):
                    y = Year;                     
                    m = Month;
                    m++;
                    if (m > 12)
                    {
                        y++;
                        m = 1;
                    }
                    return new DWMQYDate(DateType, y * 100 + m); 
                case (DWMQYType.Quarter):
                    y = Year;
                    q = Quarter;
                    q++;
                    if (q > 4)
                    {
                        y++;
                        q = 1;
                    }
                    return new DWMQYDate(DateType, y * 100 + q);
                case (DWMQYType.Week):
                    y = Year;                     
                    w = Week;
                    w++;
                    int wofy = GetYearWeekCount(y);
                    if (w > wofy)
                    {
                        y++;
                        w = 1;
                    }
                    return new DWMQYDate(DateType, y * 100 + w); 
                case (DWMQYType.Year):
                    y = Year + 1;
                    return new DWMQYDate(DateType, y);
                default:
                    throw new Exception("Date Type error!");
            }
        }

        /// <summary>
        /// 上一值
        /// </summary>
        /// <returns></returns>
        public DWMQYDate GetLast()
        {
            int y = 0, q = 0, m = 0, w = 0, d = 0;
            DateTime dt = new DateTime();
            switch (DateType)
            {
                case (DWMQYType.Day):
                    y = Year;
                    m = Month;
                    d = Day;
                    dt = new DateTime(y, m, d);
                    dt.AddDays(-1);
                    return new DWMQYDate(DateType, dt.Year * 10000 + dt.Month * 100 + dt.Day);
                case (DWMQYType.Month):
                    y = Year;
                    m = Month;
                    m--;
                    if (m <= 0)
                    {
                        y--;
                        m = 12;
                    }
                    return new DWMQYDate(DateType, y * 100 + m);
                case (DWMQYType.Quarter):
                    y = Year;
                    q = Quarter;
                    q--;
                    if (q <= 0)
                    {
                        y--;
                        q = 4;
                    }
                    return new DWMQYDate(DateType, y * 100 + q);
                case (DWMQYType.Week):
                    y = Year;
                    w = Week;
                    w--;
                    if (w <= 0)
                    {
                        y--;
                        w = GetYearWeekCount(y); ;
                    }
                    return new DWMQYDate(DateType, y * 100 + w);
                case (DWMQYType.Year):
                    y = Year - 1;
                    return new DWMQYDate(DateType, y);
                default:
                    throw new Exception("Date Type error!");
            }
        }

        /// <summary>
        /// 求某年有多少周
        /// </summary>
        /// <param name="year"></param>
        /// <returns>多少周</returns>
        private static int GetYearWeekCount(int year)
        {
            DateTime fDt = new DateTime(year, 1, 1);
            int k = Convert.ToInt32(fDt.DayOfWeek);//得到该年的第一天是周几 
            if (k == 1)
            {
                int countDay = fDt.AddYears(1).AddDays(-1).DayOfYear;
                int countWeek = countDay / 7 + 1;
                return countWeek;
            }
            else
            {
                int countDay = fDt.AddYears(1).AddDays(-1).DayOfYear;
                int countWeek = countDay / 7 + 2;
                return countWeek;
            }
        }

        /// <summary>
        /// 获取指定日期，在为一年中为第几周
        /// </summary>
        /// <param name="dt">指定时间</param>
        /// <reutrn>返回第几周</reutrn>
        private static int GetWeekOfYear(DateTime dt)
        {
            GregorianCalendar gc = new GregorianCalendar();
            int weekOfYear = gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            return weekOfYear;
        }

        /// <summary>
        /// 获取指定日期，在为一年中为第几季度
        /// </summary>
        /// <param name="dt">指定时间</param>
        /// <returns>返回第几季度</returns>
        private static int GetQuarterOfYear(DateTime dt)
        {
            int m = dt.Month;
            int q = m % 3 == 0 ? (m / 3) : (m / 3 + 1);
            return q;
        }

        /// <summary>
        /// 类型
        /// 年：2018   
        /// 季：201801    2018年第1季度
        /// 月：201810    2018年10月
        /// 周：201837    2018年第37周
        /// 天：20180203  2018年2月3日
        /// </summary>
        public DWMQYType DateType
        {
            get;
            private set;
        }

        /// <summary>
        /// 值
        /// 年：2018   
        /// 季：201801    2018年第1季度
        /// 月：201810    2018年10月
        /// 周：201837    2018年第37周
        /// 天：20180203  2018年2月3日
        /// </summary>
        public int DateValue
        {
            get;
            private set;
        }

        /// <summary>
        /// 年部份值
        /// </summary>
        public int Year
        {
            get {
                switch (DateType)
                {
                    case (DWMQYType.Day):
                        return DateValue / 10000;
                    case (DWMQYType.Month):
                        return DateValue / 100;
                    case (DWMQYType.Quarter):
                        return DateValue / 100;
                    case (DWMQYType.Week):
                        return DateValue / 100;
                    case (DWMQYType.Year):
                        return DateValue;
                    default:
                        throw new Exception("Date Type error!");
                }
            }
            
        }

        /// <summary>
        /// 季度部份
        /// </summary>
        public int Quarter
        {
            get
            {
                switch (DateType)
                {
                    case (DWMQYType.Quarter):
                        return DateValue % 100;
                    case (DWMQYType.Day):                         
                    case (DWMQYType.Month):                    
                    case (DWMQYType.Week):                         
                    case (DWMQYType.Year):                         
                    default:
                        throw new Exception("Date Type error!");
                }
            }

        }

        /// <summary>
        /// 月部份
        /// </summary>
        public int Month
        {
            get
            {
                switch (DateType)
                {
                    case (DWMQYType.Month):                    
                        return DateValue % 100;
                    case (DWMQYType.Day):
                    case (DWMQYType.Quarter):
                    case (DWMQYType.Week):
                    case (DWMQYType.Year):
                    default:
                        throw new Exception("Date Type error!");
                }
            }
        }

        /// <summary>
        /// 周部份
        /// </summary>
        public int Week
        {
            get
            {
                switch (DateType)
                {
                    case (DWMQYType.Week):                    
                        return DateValue % 100;
                    case (DWMQYType.Day):
                    case (DWMQYType.Quarter):
                    case (DWMQYType.Month):
                    case (DWMQYType.Year):
                    default:
                        throw new Exception("Date Type error!");
                }
            }
        }

        /// <summary>
        /// 天部份
        /// </summary>
        public int Day
        {
            get
            {
                switch (DateType)
                {
                    case (DWMQYType.Day):
                        return DateValue % 10000;
                    case (DWMQYType.Week):
                    case (DWMQYType.Month):
                    case (DWMQYType.Quarter):
                    case (DWMQYType.Year):
                    default:
                        throw new Exception("Date Type error!");
                }
            }
        }

        /// <summary>
        /// 检查某日期是否为此相应期间内
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool CheckDate(DateTime dt)
        {
            switch (DateType)
            {
                case DWMQYType.Day:
                    return this.Year == dt.Year && this.Month == dt.Month && this.Day == dt.Day;
                case DWMQYType.Week:
                    return this.Year == dt.Year && this.Week == GetWeekOfYear(dt);
                case DWMQYType.Month:
                    return this.Year == dt.Year && this.Month == dt.Month;
                case DWMQYType.Quarter:
                     return this.Year == dt.Year && this.Quarter == GetQuarterOfYear(dt);
                case DWMQYType.Year:
                    return this.Year == dt.Year;
                default:
                    throw new Exception("Date Type error!");
            }
        }

        /// <summary>
        /// 年：2018   
        /// 季：201801    2018年第1季度
        /// 月：201810    2018年10月
        /// 周：201837    2018年第37周
        /// 天：20180203  2018年2月3日
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            switch (DateType)
            {
                case (DWMQYType.Day):
                    return string.Format("{0}年{1}月{2}日", Year, Month, Day);
                case (DWMQYType.Week):
                    return string.Format("{0}年第{1}周", Year, Week);
                case (DWMQYType.Month):
                    return string.Format("{0}年{1}月", Year, Month);
                case (DWMQYType.Quarter):
                    return string.Format("{0}年第{1}季度", Year, Quarter);
                case (DWMQYType.Year):
                    return string.Format("{0}年", Year);
                default:
                    throw new Exception("Date Type error!");
            }
        }
                

        

    }
}
