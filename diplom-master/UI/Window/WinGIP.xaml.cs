using MityaginaNP.UI.Page;
using MityaginaNP.UI.UserControl;
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
using System.Collections.ObjectModel;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using System.Windows.Threading;
using MityaginaNP.UX.Entity;

namespace MityaginaNP.UI.Window
{
    /// <summary>
    /// Логика взаимодействия для WinGIP.xaml
    /// </summary>
    public partial class WinGIP 
    {
        bool hidden;
        User currentUser;
        public WinGIP(User _selectedUser)
        {
            InitializeComponent();

            if(_selectedUser != null)
            {
                currentUser = _selectedUser;
                DataContext = _selectedUser;
            }

            ProjectFrame.Navigate(new PageProjects(currentUser));
            ClassNavigate.NavigateFrame = ProjectFrame;
            notificationsBar.ItemsSource = App.DataBase.Notifications.ToList().Where(p => p.UserLogin == currentUser.Login);
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageClientList());
        }

        private void btnProjects_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageProjects(currentUser));
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if(IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    ProjectFrame.Height = this.Height;

                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    ProjectFrame.Height = this.Height;
                    IsMaximized = true;
                }
            }
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

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            WinAuth winAuth = new WinAuth();
            winAuth.Show();
            this.Close();
        }

        private void GanttChart_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageGanttChart(null, null, null));
        }
    }
}
