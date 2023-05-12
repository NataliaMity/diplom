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
                DateStart.SelectedDate = _curproject.ProjectStartDate;
                DateEnd.SelectedDate = _curproject.ProjectEndDate;
                txtProjectCode.IsEnabled = false;
                if(_curproject.ProjectActual == "0")
                    checkProject.IsChecked = true;
                else
                    checkProject.IsChecked = false;
            }
            else
            {
                DateStart.SelectedDate = DateTime.Today;
                DateEnd.SelectedDate = DateTime.Today;
                checkProject.IsChecked = false;
            }
            currentUser = _selectedUser;

            DataContext = _curproject;
           
            cbClient.ItemsSource = App.DataBase.Clients.ToList();
        }

        private void btnSaveProject_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (txtProjectCode.Text.Length == 0)
                errors.AppendLine("Необходимо указать код проекта");
            if (txtProjectDescription.Text.Length == 0)
                errors.AppendLine("Необходимо указать описание проекта");
            if (txtProjectName.Text.Length == 0)
                errors.AppendLine("Необходимо указать название проекта");
            if (cbClient.SelectedIndex == -1)
                errors.AppendLine("Необходимо указать клиента");
            if (DateEnd.SelectedDate == null)
                errors.AppendLine("Необходимо указать дату окончания проекта");
            if (DateStart.SelectedDate == null)
                errors.AppendLine("Необходимо указать дату начала проекта");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            else
            {
                if (checkProject.IsChecked == true)
                {
                    _curproject.ProjectActual = "0";
                }
                else
                {
                    _curproject.ProjectActual = "1";

                }
                _curproject.ProjectStartDate = (DateTime)DateStart.SelectedDate;
                _curproject.ProjectEndDate = (DateTime)DateEnd.SelectedDate;
                if (projId == null)
                {
                    try
                    {
                        _curproject.UserLogin = currentUser.Login;
                        if(errors.Length == 0)
                        {
                            App.DataBase.Projects.Add(_curproject);
                            App.DataBase.SaveChanges();
                            MessageBox.Show("Успешно сохранено!");
                        }
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
                        if (errors.Length == 0)
                        {
                            App.DataBase.SaveChanges();
                            MessageBox.Show("Успешно сохранено!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        App.DataBase.Projects.Remove(_curproject);
                    }
                }
            }
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.GoBack();
        }
    }
}
