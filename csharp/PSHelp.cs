
namespace SeaCodeLib.Common
{

    public class PSHelp
    {

        public PSHelp() { }

        public PSHelp(int pagesize, int pageindex, string sortname, bool isdesc = false)
        {
            Page = new PageHelp(pagesize, pageindex);
            Sort = new SortHelp(sortname, isdesc);

            EnsureDefault(this);
        }


        public static PSHelp GetDefault()
        {
            return new PSHelp
            {
                Page = GetDefaultPage(),
                Sort = new SortHelp() 
            };
        }

        private static PageHelp GetDefaultPage()
        {
            return new PageHelp(20, 1);
        }

        private static SortHelp GetDefaultSort()
        {
            return new SortHelp();
        }

        public void EnsureDefault()
        {
            EnsureDefault(this);
        }

        public static void EnsureDefault(PSHelp ps)
        {
            if (ps == null)
            {
                ps = GetDefault();
            }
            if (ps.Page == null || ps.Page.PageSize == 0 || ps.Page.PageIndex == 0)
            {
                ps.Page = GetDefaultPage();
            }
            if (ps.Sort == null)
            {
                ps.Sort = GetDefaultSort();
            }
        }

      

        public PageHelp Page { get; set; }

 		
        public SortHelp Sort { get; set; }
 
    }
}
