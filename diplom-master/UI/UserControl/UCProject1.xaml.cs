using MityaginaNP.UI.Page;
using MityaginaNP.UX.Class;
using MityaginaNP.UX.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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
    public partial class UCProject1
    {
        public UCProject1()
        {
            InitializeComponent();
        }

        private void ProjectTasks_Click(object sender, RoutedEventArgs e)
        {
            /*ClassNavigate.NavigateFrame.Navigate(new PageTaskList());*/
        }
    }
}
