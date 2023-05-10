using MityaginaNP.UX.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace MityaginaNP.UX.Class
{
    public class ClassNotification
    {
        private List<Entity.TaskProject> _taskProject = new List<Entity.TaskProject>();
        TaskProject taskProject;

        
        public static string connectionString = ClassConnect.GetSQLConnString();
        public static void CheckDateNotif(string user)
        {
            var projects = App.DataBase.TaskProjects.ToList();
            foreach (var project in projects)
            {
                var TaskProject = App.DataBase.TaskProjects.ToList();
                string sqlExpression = "INSERT INTO[dbo].[Notification] ([TypeID], [UserLogin], [NotificationDateTime], [TaskID]) VALUES (1, '" + user + "', '" + DateTime.Now.ToString() + "', '" + project.TaskID + "')";

                if (project.TaskDeadLine == DateTime.Today.AddDays(3) || project.TaskDeadLine == DateTime.Today.AddDays(1) || project.TaskDeadLine == DateTime.Today.AddDays(2) || project.TaskDeadLine == DateTime.Today)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(sqlExpression, con);
                        try
                        {
                            cmd.ExecuteNonQuery();
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
            }
        }
        public static void CheckDeadLineNotif(string user)
        {
            var projects = App.DataBase.TaskProjects.ToList();
            foreach (var project in projects)
            {
                var TaskProject = App.DataBase.TaskProjects.ToList();
                string sqlExpression = "INSERT INTO[dbo].[Notification] ([TypeID], [UserLogin], [NotificationDateTime], [TaskID]) VALUES (4, '" + user + "', '" + DateTime.Now.ToString() + "', '" + project.TaskID + "')";

                if (project.TaskDeadLine < DateTime.Today)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(sqlExpression, con);
                        try
                        {
                            cmd.ExecuteNonQuery();
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
            }
        }
        public static void CheckNewTaskNotif(string user)
        {
            var projects = App.DataBase.TaskProjects.ToList();
            foreach (var project in projects)
            {
                var TaskProject = App.DataBase.TaskProjects.ToList();
                string sqlExpression = "INSERT INTO[dbo].[Notification] ([TypeID], [UserLogin], [NotificationDateTime], [TaskID]) VALUES (2, '" + user + "', '" + DateTime.Now.ToString() + "', '" + project.TaskID + "')";

                    if (project.TaskStart == DateTime.Today || project.TaskStart == DateTime.Today.AddDays(-1))
                    {
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand(sqlExpression, con);
                            try
                            {
                                cmd.ExecuteNonQuery();
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
            }
        }
        public static void CheckNewStatusNotif(string user)
        {
            var projects = App.DataBase.TaskProjects.ToList();
            foreach (var project in projects)
            {
                var TaskProject = App.DataBase.TaskProjects.ToList();
                string sqlExpression = "INSERT INTO[dbo].[Notification] ([TypeID], [UserLogin], [NotificationDateTime], [TaskID]) VALUES (3, '" + user + "', '" + DateTime.Now.ToString() + "', '" + project.TaskID + "')";

                //if (project.StatusId != 1)
                //{
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(sqlExpression, con);
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                        }
                        finally
                        {
                            con.Close();
                        }
                    }


                //}
            }
        }
    }
}
