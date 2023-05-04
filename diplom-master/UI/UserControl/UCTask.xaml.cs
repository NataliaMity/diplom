using MityaginaNP.UI.Page;
using MityaginaNP.UX.Class;
using MityaginaNP.UX.Entity;
using Syncfusion.Windows.Shared;
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
    /// Логика взаимодействия для UCTask.xaml
    /// </summary>
    public partial class UCTask
    {
        public UCTask()
        {
            InitializeComponent();
        }
        
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            string projNum = txtProjNum.Text;
            ClassNavigate.NavigateFrame.Navigate(new PageAddEditTask((sender as Button).DataContext as TaskProject, projNum));
        }

        private void btnDocs_Click(object sender, RoutedEventArgs e)
        {
            string projNum = txtProjNum.Text;
            ClassNavigate.NavigateFrame.Navigate(new PageDocs((sender as Button).DataContext as Document, projNum));
        }
    }
}
