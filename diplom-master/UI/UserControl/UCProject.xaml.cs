using MityaginaNP.UI.Page;
using MityaginaNP.UX.Class;
using MityaginaNP.UX.Entity;
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

namespace MityaginaNP.UI.UserControl
{
    /// <summary>
    /// Логика взаимодействия для UCProject.xaml
    /// </summary>
    public partial class UCProject
    {
        public UCProject()
        {
            InitializeComponent();
        }

        private void ProjectTask_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageTaskList((sender as Button).DataContext as Project, null, null));
        }

        private void ProjectChange_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageAddEditProject((sender as Button).DataContext as Project, null));
        }

        private void GanttChart_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageGanttChart((sender as Button).DataContext as Project, null, null));
        }

        private void ProjectDos_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageDocs((sender as Button).DataContext as Project, null));
        }
    }
}
