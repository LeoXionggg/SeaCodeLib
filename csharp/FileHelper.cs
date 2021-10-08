using System;
using System.IO;
using System.Web;

namespace SeaCodeLib.Common
{
    public class FileHelper
    {
        public static byte[] GetByte(string webfilepath)
        {
            string strFilePath = HttpContext.Current.Server.MapPath("~/"+ webfilepath);
            if (!File.Exists(strFilePath))
            {
                return null;
            }
            byte[] filebyte = File.ReadAllBytes(strFilePath); 
            return filebyte; 
        }

        public static bool DelFile(string webfilepath)
        {
            try
            {
                string strRootPath = HttpContext.Current.Server.MapPath("~/");
                strRootPath += "\\" + webfilepath;
                if (System.IO.File.Exists(strRootPath))
                {
                    System.IO.File.Delete(strRootPath);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string MakeRndPath(string RootPath, string TypePath, out string strSaveDBPath)
        {
            string strYear = DateTime.Now.ToString("yyyy");
            string strMonth = DateTime.Now.ToString("MM");
            string strDay = DateTime.Now.ToString("dd");

            string diskpath = HttpContext.Current.Server.MapPath("~/");
            diskpath += "\\" + RootPath;
            if (!Directory.Exists(diskpath))
            {
                Directory.CreateDirectory(diskpath);
            }
            diskpath += "\\" + TypePath;
            if (!Directory.Exists(diskpath))
            {
                Directory.CreateDirectory(diskpath);
            }
            diskpath += "\\" + strYear;
            if (!Directory.Exists(diskpath))
            {
                Directory.CreateDirectory(diskpath);
            }
            diskpath += "\\" + strMonth;
            if (!Directory.Exists(diskpath))
            {
                Directory.CreateDirectory(diskpath);
            }
            diskpath += "\\" + strDay;
            if (!Directory.Exists(diskpath))
            {
                Directory.CreateDirectory(diskpath);
            }

            strSaveDBPath = RootPath + "/" + TypePath + "/" + strYear + "/" + strMonth + "/" + strDay;

            return diskpath;
        }

    }
}
