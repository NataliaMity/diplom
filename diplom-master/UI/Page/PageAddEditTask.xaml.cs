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
        private string _curProjId;
        public DateTime end;
        public DateTime start;
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
            if(ClassAuthorization.roleUser == 1)
            {
                ComboWorker.Visibility = Visibility.Collapsed;
                DateDeadLine.Visibility = Visibility.Collapsed;
                DateStart.Visibility = Visibility.Collapsed;
            }
            _curProjId = proj;
            DataContext = _curProj;
            end = (DateTime)App.DataBase.Projects.ToList().Where(p => p.ProjectID == proj).Select(p => p.ProjectEndDate).First();
            start = (DateTime)App.DataBase.Projects.ToList().Where(p => p.ProjectID == proj).Select(p => p.ProjectStartDate).First();
            DateDeadLine.SelectedDate = end;
            DateStart.SelectedDate = start;
            ComboDepartment.ItemsSource = App.DataBase.Departments.ToList();
            ComboWorker.ItemsSource = App.DataBase.Users.ToList().Where(p => p.RoleID == 3 && p.DepartmentID == _curProj.DepartmentID);
        }

        private void BtnSaveTask_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (txtTaskName.Text.Length == 0)
                errors.AppendLine("Необходимо указать текст задачи");
            if (ComboDepartment.SelectedIndex == -1)
                errors.AppendLine("Необходимо указать отдел");
            if (ClassAuthorization.roleUser == 2)
            {
                if (DateStart.SelectedDate <= start)
                    errors.AppendLine("Дата начала задачи должна быть больше даты начала проекта");
                if (DateDeadLine.SelectedDate >= end)
                    errors.AppendLine("Дата окончания задачи должна быть меньше даты окончания проекта");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_curProj.TaskID == 0)
            {
                _curProj.ProjectID = _curProjId;
                _curProj.StatusId = 1;
                
                
                _curProj.TaskStart = start;
                _curProj.TaskDeadLine = end;
                try
                {
                    
                    if(errors.Length == 0)
                    {
                        App.DataBase.TaskProjects.Add(_curProj);
                        App.DataBase.SaveChanges();

                        MessageBox.Show("Сохранение прошло успешно!");

                        ClassNotification.CheckNewTaskNotif(App.DataBase.Users.Where(p => p.RoleID == 2 && p.DepartmentID == ComboDepartment.SelectedIndex + 1).First().Login, _curProj.TaskID);

                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    _curProj.TaskDeadLine = (DateTime)DateDeadLine.SelectedDate;
                    _curProj.TaskStart = (DateTime)DateStart.SelectedDate;
                    if(errors.Length == 0)
                    {
                        App.DataBase.SaveChanges();
                        MessageBox.Show("Сохранение прошло успешно!");
                        ClassNotification.CheckNewTaskNotif(App.DataBase.Users.Where(p => p.RoleID == 3 && p.Login == ComboWorker.SelectedItem.ToString()).First().Login, _curProj.TaskID);

                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }

        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.GoBack();
        }
    }
}
