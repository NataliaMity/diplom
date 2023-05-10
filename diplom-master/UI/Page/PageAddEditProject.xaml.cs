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

namespace MityaginaNP.UI.Page
{
    /// <summary>
    /// Логика взаимодействия для PageAddEditProject.xaml
    /// </summary>
    public partial class PageAddEditProject 
    {
        private Project _curproject = new Project();
        User currentUser;
        private string projId = null;
        public PageAddEditProject(Project selectedProject, User _selectedUser)
        {
            InitializeComponent();
            if (selectedProject != null)
            {
                _curproject = selectedProject;
                projId = selectedProject.ProjectID;
            }
            currentUser = _selectedUser;
                DataContext = _curproject;
                cbClient.ItemsSource = App.DataBase.Clients.ToList();
        }

        private void btnSaveProject_Click(object sender, RoutedEventArgs e)
        {
            if(checkProject.IsChecked == true)
            {
                _curproject.ProjectActual = "1";
            }
            else
            {
                _curproject.ProjectActual = "0";

            }
            _curproject.ProjectStartDate = (DateTime)DateStart.SelectedDate;
            _curproject.ProjectEndDate = (DateTime)DateEnd.SelectedDate;
            if (projId == null)
            {
                try
                {
                    _curproject.UserLogin = currentUser.Login;
                    App.DataBase.Projects.Add(_curproject);
                    App.DataBase.SaveChanges();
                    MessageBox.Show("Ok");
                }
                catch (Exception ex) 
                { 
                    MessageBox.Show(ex.ToString()); 
                    App.DataBase.Projects.Remove(_curproject);
                }
            }
            else
            {
                try
                {
                    App.DataBase.SaveChanges();
                    MessageBox.Show("Ok");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    App.DataBase.Projects.Remove(_curproject);
                }
            }
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.GoBack();
        }
    }
}
