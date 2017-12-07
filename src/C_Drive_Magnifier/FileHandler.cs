using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace C_Drive_Magnifier
{
    class FileHandler
    {
        internal static string filePath;
        internal static IniFile settings = new IniFile("settings.ini");

        public FileHandler(string path)
        {
            filePath = path;
        }

        public bool CopyFileToLocal()
        {
            var settings = new IniFile("settings.ini");
            try
            {
                File.Copy(filePath, settings.Read("LocalFolder" + Path.GetFileName(filePath)), true);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public static void DoINIFileChecks()
        {
            if (!Directory.Exists(settings.Read("LocalDir")))
            {
                System.Windows.Forms.MessageBox.Show("The specified local path was not found and therefore was defaulted to current exe build path", "Invalid Path");
                settings.Write("LocalDir", )
            }
        }








    }
}
