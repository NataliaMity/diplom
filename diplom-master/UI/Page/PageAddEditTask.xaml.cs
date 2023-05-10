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
    /// Логика взаимодействия для PageAddEditTask.xaml
    /// </summary>
    public partial class PageAddEditTask
    {
        private TaskProject _curProj = new TaskProject();
        private Project _proj = new Project();
        private string _curProjId;
        public PageAddEditTask(TaskProject selectedProj, string proj)
        {
            InitializeComponent();
            if (selectedProj != null)
            {
                _curProjId = proj;
                _curProj = selectedProj;

            }
            if(ClassAuthorization.roleUser == 2)
            {
                ComboDepartment.IsEnabled = false;
                txtTaskName.IsEnabled = false;
            }
            _curProjId = proj;
            DateDeadLine.SelectedDate = DateTime.Today.Date;
            DateStart.SelectedDate = DateTime.Today.Date;
            DataContext = _curProj /*_proj*/;
            ComboDepartment.ItemsSource = App.DataBase.Departments.ToList();
            ComboWorker.ItemsSource = App.DataBase.Users.ToList().Where(p => p.RoleID == 3 && p.DepartmentID == ComboDepartment.SelectedIndex + 1);
        }

        private void BtnSaveTask_Click(object sender, RoutedEventArgs e)
        {
            if(_curProj.TaskID == 0)
            {
                _curProj.ProjectID = _curProjId;
                _curProj.StatusId = 1;
                _curProj.TaskDeadLine = (DateTime)DateDeadLine.SelectedDate;
                _curProj.TaskStart = (DateTime)DateStart.SelectedDate;
                try
                {

                    App.DataBase.TaskProjects.Add(_curProj);
                    App.DataBase.SaveChanges();
                    
                MessageBox.Show("Сохранение прошло успешно!");
                ClassNotification.CheckNewTaskNotif(App.DataBase.Users.Where(p => p.RoleID == 3 && p.DepartmentID == ComboDepartment.SelectedIndex + 1).First().Login);
            }
                catch (Exception ex)
                {
                MessageBox.Show(ex.Message);
            }
        }
            else
            {
                try
                {
                    _curProj.TaskStart = (DateTime)DateStart.SelectedDate;
                    _curProj.TaskDeadLine = (DateTime)DateDeadLine.SelectedDate;
                    
                    App.DataBase.SaveChanges();
                    
                    MessageBox.Show("Сохранение прошло успешно!");
                    ClassNotification.CheckNewStatusNotif(App.DataBase.Users.Where(p => p.Login == _curProj.UserLogin).First().Login);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.GoBack();
        }
    }
}
