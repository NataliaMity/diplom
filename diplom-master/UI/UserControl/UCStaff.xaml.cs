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
    /// Логика взаимодействия для UCStaff.xaml
    /// </summary>
    public partial class UCStaff
    {
        public UCStaff()
        {
            InitializeComponent();
        }

        private void btnMore_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageGanttChart(null, null, (sender as Button).DataContext as User));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            //var users = App.DataBase.Users.ToList().Where(p => p.LastName == txtName.Text).ToList();
            ////
            //foreach (var user in users)
            //{
            //if (txtName.Text.Length != 0)
                txtCount.Text = App.DataBase.TaskProjects.ToList().Where(p => p.User != null && p.User.LastName == txtName.Text).Count().ToString();
            //}
        }
    }
}
