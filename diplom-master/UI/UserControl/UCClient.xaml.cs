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
    /// Логика взаимодействия для UCClient.xaml
    /// </summary>
    public partial class UCClient 
    {
        public UCClient()
        {
            InitializeComponent();
        }

        private void btnMore_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageProjects(null, (sender as Button).DataContext as Client));
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageAddEditClients((sender as Button).DataContext as Client));
        }
    }
}
