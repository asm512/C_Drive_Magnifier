using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

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
                File.Copy(filePath, settings.Read("LocalDir") + Path.GetFileName(filePath), Convert.ToBoolean(settings.Read("OpenFileAfterCopy")));
                if (Convert.ToBoolean(settings.Read("ShowSuccessfulTransferDialog")))
                {
                    MessageBox.Show("Successfully retreived file", "Success", MessageBoxButtons.OK);   
                }
                if (Convert.ToBoolean(settings.Read("OpenFileAfterCopy")))
                {
                    System.Diagnostics.Process.Start(settings.Read("LocalDir") + Path.GetFileName(filePath));   
                }

                return true;
        }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
                throw;
            }
}

        public static void DoINIFileChecks()
        {
            if (!Directory.Exists(settings.Read("LocalDir")))
            {
                MessageBox.Show("The specified local path was not found and therefore was defaulted to current exe path", "Invalid Path");
                settings.Write("LocalDir", AppDomain.CurrentDomain.BaseDirectory + @"My Files");
                if (!Directory.Exists(settings.Read("LocalDir"))) { Directory.CreateDirectory(settings.Read("LocalDir")); }
            }
        }








    }
}
