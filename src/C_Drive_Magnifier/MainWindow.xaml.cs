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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.IO;

namespace C_Drive_Magnifier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        internal string CurrentDir = "C:/";
        internal string PrevPath = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenSettingsWindow()
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void LoadDrive(string path = "C:/")
        {
            PrevPath = CurrentDir;
            CurrentDir = path;
            contentDisplay.Children.Clear();
            try
            {
                foreach (string content in Directory.GetDirectories(path))
                {
                    Button button = new Button();
                    button.Content = content;
                    button.Tag = "folder";
                    button.Background = Brushes.Yellow;
                    Thickness margin = button.Margin;
                    margin.Top = 10;
                    margin.Left = 40;
                    margin.Right = 40;
                    button.Margin = margin;
                    contentDisplay.Children.Add(button);
                    button.Click += new RoutedEventHandler(OnButtonClick);
                }
                foreach (string content in Directory.GetFiles(path))
                {
                    Button button = new Button();
                    button.Content = content;
                    button.Tag = "file";
                    button.Background = Brushes.CadetBlue;
                    Thickness margin = button.Margin;
                    margin.Top = 10;
                    margin.Left = 40;
                    margin.Right = 40;
                    button.Margin = margin;
                    contentDisplay.Children.Add(button);
                    button.Click += new RoutedEventHandler(OnButtonClick);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to load drive content due to the following error : " + e.Message + Environment.NewLine + "If the error was an unauthorized access error, make sure to attatch VS's debugger since this should transfer VS's admin rights", "Exception caught");
            }

        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            string clickedPath = sender.ToString().Replace("System.Windows.Controls.Button: ", "");
            FileAttributes attribs = File.GetAttributes(clickedPath);
            if (attribs.HasFlag(FileAttributes.Directory))
            {
                LoadDrive(clickedPath);
            }
            else
            {
                FileHandler userFile = new FileHandler(clickedPath);
                userFile.CopyFileToLocal();
            }
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenSettingsWindow();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            FileHandler.DoINIFileChecks();
            LoadDrive();
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadDrive(CurrentDir);
        }

        private void prevPathButton_Click(object sender, RoutedEventArgs e)
        {
            LoadDrive(PrevPath);
        }
    }
}
