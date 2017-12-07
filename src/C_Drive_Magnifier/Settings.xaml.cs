using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Windows.Forms;

namespace C_Drive_Magnifier
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : MetroWindow
    {
        internal static bool HaveSettingsBeenModified = false;
        internal static IniFile settings = new IniFile("settings.ini");

        public Settings()
        {
            InitializeComponent();
        }

        private void ReadSettings()
        {
            overwriteFileIfExistsCheckbox.IsChecked = Convert.ToBoolean(settings.Read("OverwriteFileIfExists"));
            openFileAfterCopyCheckbox.IsChecked = Convert.ToBoolean(settings.Read("OpenFileAfterCopy"));
            showSuccessfulTransferCheckbox.IsChecked = Convert.ToBoolean(settings.Read("ShowSuccessfulTransferDialog"));
        }

        private void SaveSettings()
        {
            if (overwriteFileIfExistsCheckbox.IsChecked == true) { settings.Write("OverwriteFileIfExists", "true"); }
            else { settings.Write("OverwriteFileIfExists", "false"); }

            if (openFileAfterCopyCheckbox.IsChecked == true) { settings.Write("OpenFileAfterCopy", "true"); }
            else { settings.Write("OpenFileAfterCopy", "false"); }

            if (showSuccessfulTransferCheckbox.IsChecked == true) { settings.Write("ShowSuccessfulTransferDialog", "true"); }
            else { settings.Write("ShowSuccessfulTransferDialog", "false"); }
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (HaveSettingsBeenModified)
            {
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Are you sure you want to close without saving?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveSettings();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void ModifiedSettings(object sender, RoutedEventArgs e)
        {
            HaveSettingsBeenModified = true;
        }

        private void saveSettings_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ReadSettings();
        }
    }
}
