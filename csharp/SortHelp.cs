
namespace SeaCodeLib.Common
{

    public class SortHelp
    {

        public SortHelp() { }

        public SortHelp(string sortname, bool isdesc)
        {
            this.SortName = sortname;
            this.IsDesc = isdesc;
        }

        public string SortName { get; set; } = "";

        public bool IsDesc { get; set; } = false;

        public override string ToString()
        {
            SortName = SortName.Trim();
            string stype = IsDesc ? "DESC" : "ASC";
            return string.IsNullOrWhiteSpace(SortName) ? "" : $"{SortName} {stype}";
        }
    }
}
