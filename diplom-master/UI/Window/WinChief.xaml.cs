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

namespace MityaginaNP.UI.Window
{
    /// <summary>
    /// Логика взаимодействия для WinChief.xaml
    /// </summary>
    public partial class WinChief
    {
        bool hidden;
        public WinChief()
        {
            InitializeComponent();

            var dep = App.DataBase.Departments.Where(p => p.DepartmentID == 1);



            ClassNavigate.NavigateFrame = ProjectFrame;
            ProjectFrame.Navigate(new PageStaffList());
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

        private void btnDiagrams_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageDiagrams());
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

        private void btnStaff_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageStaffList());
        }

        private void btnProjects_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageProjects(null));

        }

        private void btnDepTasks_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageTaskList(null, App.DataBase.Departments.Where(p => p.DepartmentID == 1).First(), null));

        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            WinAuth winAuth = new WinAuth();
            winAuth.Show();
            this.Close();
        }

        private void btnGanttChart_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageGanttChart(null, null, null));
        }

        private void btnGanttChartTask_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageGanttChart(null, App.DataBase.Departments.Where(p => p.DepartmentID == 1).First(), null));

        }
    }
}
