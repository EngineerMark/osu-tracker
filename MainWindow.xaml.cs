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

namespace osu_tracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SettingsWindow settingsWindow = null;

        public MainWindow()
        {
            InitializeComponent();

            if(Properties.Settings.Default.OsuApiKey==String.Empty || Properties.Settings.Default.OsuUsername == String.Empty)
            {
                MessageBox.Show("Some settings are not set (correctly), please check them.", "Warning");
            }

            this.SourceInitialized += MainWindow_SourceInitialized;
            this.Closed += MainWindow_Closed;
        }

        private void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            this.Top = Properties.Settings.Default.Top;
            this.Left = Properties.Settings.Default.Left;
            this.Height = Properties.Settings.Default.Height;
            this.Width = Properties.Settings.Default.Width;
            if (Properties.Settings.Default.Maximized)
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            if(settingsWindow!=null){
                settingsWindow.Close();
            }

            if (WindowState == WindowState.Maximized)
            {
                Properties.Settings.Default.Top = RestoreBounds.Top;
                Properties.Settings.Default.Left = RestoreBounds.Left;
                Properties.Settings.Default.Height = RestoreBounds.Height;
                Properties.Settings.Default.Width = RestoreBounds.Width;
                Properties.Settings.Default.Maximized = true;
            }
            else
            {
                Properties.Settings.Default.Top = this.Top;
                Properties.Settings.Default.Left = this.Left;
                Properties.Settings.Default.Height = this.Height;
                Properties.Settings.Default.Width = this.Width;
                Properties.Settings.Default.Maximized = false;
            }

            Properties.Settings.Default.Save();
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            if(settingsWindow==null){
                settingsWindow = new SettingsWindow();
                settingsWindow.Show();
                settingsWindow.Focus();
                settingsWindow.Closed += (s, _e) =>
                {
                    settingsWindow = null;
                };
            }else{
                settingsWindow.Focus();
            }
        }
    }
}
