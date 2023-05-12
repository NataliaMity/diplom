using MityaginaNP.UI.Page;
using MityaginaNP.UX.Class;
using MityaginaNP.UX.Entity;
using Syncfusion.ProjIO;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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
        public BDEntities entities;
        public static string connectionString = ClassConnect.GetSQLConnString();
        public string depChef;
        public UCTask()
        {
            InitializeComponent();
            cbStatus.ItemsSource = App.DataBase.TaskStatus.ToList();

            if (ClassAuthorization.roleUser == 1)
            {
                cbStatus.Visibility = Visibility.Collapsed;
                btnChange.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
            }
            if (ClassAuthorization.roleUser == 2)
            {
                cbStatus.Visibility = Visibility.Collapsed;
                btnChange.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Collapsed;
            }
            if (ClassAuthorization.roleUser == 3)
            {
                cbStatus.Visibility = Visibility.Visible;
                btnChange.Visibility = Visibility.Collapsed;
                btnDelete.Visibility = Visibility.Collapsed;

            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            string projNum = txtProjNum.Text;
            ClassNavigate.NavigateFrame.Navigate(new PageAddEditTask((sender as Button).DataContext as TaskProject, projNum));
        }

        private void btnDocs_Click(object sender, RoutedEventArgs e)
        {
            string taskNum = txtTaskNum.Text;
            ClassNavigate.NavigateFrame.Navigate(new PageDocs(null, (sender as Button).DataContext as TaskProject));
        }

        private void cbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = (cbStatus.SelectedIndex + 1);
            int dep = Convert.ToInt32(txtDep.Text);
            var depChef = App.DataBase.Users.Where(p => p.DepartmentID == dep && p.RoleID == 2).Select(p => p.Login).FirstOrDefault().ToString();


            string sqlExpression = "UPDATE [dbo].[TaskProject] Set StatusId = '" + (id) + "' Where TaskID = '" + txtTaskNum.Text + "'";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlExpression, con);
                try
                {
                    cmd.ExecuteNonQuery();
                    App.DataBase.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());

                    MessageBox.Show("Статус изменен!");
                    ClassNotification.CheckNewStatusNotif(depChef, Convert.ToInt32(txtTaskNum.Text));

                }
                catch (SqlException ex)
                {
                }
                finally
                {
                    con.Close();
                }
            }
            ClassNavigate.NavigateFrame.Navigate(new PageTaskList(null, null, App.DataBase.Users.Where(p => p.Login == user.Text).FirstOrDefault()));

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int taskid = Convert.ToInt32(txtTaskNum.Text);
            try
            {
                var taskforremoving = App.DataBase.TaskProjects.Where(p => p.TaskID == taskid).First();
                if (App.DataBase.Documents.Where(p => p.TaskID == taskid).Count() > 0)
                    MessageBox.Show("Невозможно удалить задачу, имеющую документацию!");
                else
                {
                    App.DataBase.TaskProjects.Remove((TaskProject)taskforremoving);
                    App.DataBase.SaveChanges();
                    MessageBox.Show("Успешно удалено!");
                    App.DataBase.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                }
            }
            catch { }

        }
    }
}
