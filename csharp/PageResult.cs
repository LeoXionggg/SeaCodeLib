
namespace SeaCodeLib.Common
{
    using System.Collections.Generic;
    public class PageResult<T>
    {
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> DataList { get; set; }
    }
 
}
