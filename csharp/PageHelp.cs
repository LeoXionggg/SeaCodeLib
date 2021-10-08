
namespace SeaCodeLib.Common
{

    public class PageHelp
    {

        public PageHelp() { }

        public PageHelp(int pagesize, int pageindex)
        {
            this.PageSize = pagesize;
            this.PageIndex = pageindex;
        }
        
        public int PageSize { get; set; }

        public int PageIndex { get; set; }
 

    }
}
