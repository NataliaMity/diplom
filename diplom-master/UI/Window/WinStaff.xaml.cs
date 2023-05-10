using MityaginaNP.UI.Page;
using MityaginaNP.UX.Class;
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
using System.Windows.Threading;
using System.Xml.Linq;

namespace MityaginaNP.UI.Window
{
    /// <summary>
    /// Логика взаимодействия для WinStaff.xaml
    /// </summary>
    public partial class WinStaff 
    {
        bool hidden;

        public WinStaff()
        {
            InitializeComponent();
            
            ClassNavigate.NavigateFrame = ProjectFrame;
            ProjectFrame.Navigate(new PageTaskList(null, null, App.DataBase.Users.Where(p => p.Login == "User").First()));
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }
            }
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
            dispatcherTimer.Start();
            dispatcherTimer.Tick += new EventHandler((object c, EventArgs eventArgs) =>
            {
                ClassNotification.CheckDateNotif("User");
            });
        }

        private void btnTasks_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageTaskList(null, null, null));
        }

        private void btnGanttChart_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageGanttChart(null, null, App.DataBase.Users.Where(p => p.Login == "User").First()));
        }

        private void btnNotifications_Click(object sender, RoutedEventArgs e)
        {
            if (hidden)
            {

                notificationsBar.Visibility = Visibility.Visible;
                hidden = false;
            }
            else
            {
                notificationsBar.Visibility = Visibility.Hidden;
                hidden = true;
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            WinAuth winAuth = new WinAuth();
            winAuth.Show();
            this.Close();
        }
    }
}
