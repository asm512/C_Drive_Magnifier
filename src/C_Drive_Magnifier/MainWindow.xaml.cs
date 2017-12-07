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
        public string CurrentDir = "C:/";

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
            CurrentDir = path;
            contentDisplay.Children.Clear();
            foreach(string content in Directory.GetDirectories(path))
            {
                Button button = new Button();
                button.Content = content;
                Thickness margin = button.Margin;
                margin.Top = 10;
                margin.Left = 40;
                margin.Right = 40;
                button.Margin = margin;
                contentDisplay.Children.Add(button);
                button.Click += new RoutedEventHandler(OnButtonClick);
            }
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            string clickedPath = sender.ToString().Replace("System.Windows.Controls.Button: ", "");
            FileAttributes attr = File.GetAttributes(clickedPath);
            if (attr.HasFlag(FileAttributes.Directory))
                MessageBox.Show("Its a directory");
            else
                MessageBox.Show("Its a file");
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
    }
}
