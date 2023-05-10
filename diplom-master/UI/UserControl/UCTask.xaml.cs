using MityaginaNP.UI.Page;
using MityaginaNP.UX.Class;
using MityaginaNP.UX.Entity;
using Syncfusion.ProjIO;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
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
        public static string connectionString = ClassConnect.GetSQLConnString();
        public UCTask()
        {
            InitializeComponent();
            cbStatus.ItemsSource = App.DataBase.TaskStatus.ToList();
            if(ClassAuthorization.roleUser == 1 || ClassAuthorization.roleUser == 2)
            {
                cbStatus.Visibility = Visibility.Collapsed;
                btnChange.Visibility = Visibility.Visible;
            }
            if (ClassAuthorization.roleUser == 3)
            {
                cbStatus.Visibility = Visibility.Visible;
                btnChange.Visibility = Visibility.Collapsed;
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
            if (txtStatNum.Text != id.ToString())
            {
                string sqlExpression = "UPDATE [dbo].[TaskProject] Set StatusId = '" + (id) + "' Where TaskID = '" + txtTaskNum.Text + "'";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(sqlExpression, con);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Статус изменен!");
                        ClassNotification.CheckNewStatusNotif(user.Text);
                    }
                    catch (SqlException ex)
                    {
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            else
            {

            }
        }
    }
}
