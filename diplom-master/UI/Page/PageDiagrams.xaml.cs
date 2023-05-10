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
using System.Windows.Forms.DataVisualization.Charting;
using MityaginaNP.UX.Entity;
using Syncfusion.ProjIO;
using Syncfusion.Linq;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;

namespace MityaginaNP.UI.Page
{
    /// <summary>
    /// Логика взаимодействия для PageDiagrams.xaml
    /// </summary>
    public partial class PageDiagrams
    {
        private BDEntities _context = new BDEntities();
        private User user;
        public PageDiagrams()
        {
            InitializeComponent();
            ChartStaff.ChartAreas.Add(new ChartArea("Main"));
            

            var curSeries = new Series("Статус задачи")
            {
                IsValueShownAsLabel = true
            };
            ChartStaff.Series.Add(curSeries);
            dpDateBefore.SelectedDate = DateTime.Today;
            dpDateFrom.SelectedDate = DateTime.Today;
            cmbDiagram.ItemsSource = System.Enum.GetValues(typeof(SeriesChartType));
            cmbStatus.ItemsSource = App.DataBase.TaskStatus.ToList();
        }

        private void UpdateChart(object sender, SelectionChangedEventArgs e)
        {
            if (cmbStatus.SelectedItem is TaskStatu curStatus && cmbDiagram.SelectedItem is SeriesChartType currType)
            {
                Series curSeries = ChartStaff.Series.FirstOrDefault();
                Dictionary<User, decimal> tasks = new Dictionary<User, decimal>();
                curSeries.ChartType = currType;
                curSeries.Points.Clear();

                var task = _context.TaskProjects.ToList();
                var userList = App.DataBase.Users.ToList();
                foreach (var user in userList)
                {
                    //curSeries.Points.AddXY(user.LastName,
                    //    App.DataBase.TaskProjects.ToList().Where(p => p.TaskStatu == curStatus
                    //    && p.User == user && p.TaskStart == dpDateFrom.SelectedDate && p.TaskDeadLine == dpDateBefore.SelectedDate).Count());
                    foreach (var currentTask in task)
                    {
                        if (currentTask.TaskStart >= dpDateFrom.SelectedDate && currentTask.TaskStart <= dpDateBefore.SelectedDate)
                        {
                            //curSeries.Points.Clear();

                            //curSeries.Points.AddXY(user.LastName,
                            //    App.DataBase.TaskProjects.ToList().Where(p => p.TaskStatu == curStatus
                            //    && p.User == user).Count());
                            //curSeries.Points.Clear();
                            curSeries.Points.AddXY(user.LastName,
                            App.DataBase.TaskProjects.ToList().Where(p => p.TaskStatu == curStatus
                            && p.User == user && p.TaskStart == currentTask.TaskStart && p.TaskDeadLine == currentTask.TaskDeadLine).Count());
                            curSeries.Points.Clear();

                            //if (!tasks.ContainsKey(currentTask.User))
                            //    tasks.Add(currentTask.User, 0);

                            //tasks[currentTask.User] += currentTask.User.TaskProjects.ToList().Where(p => p.TaskStart == currentTask.TaskStart && p.TaskDeadLine == currentTask.TaskDeadLine).Count();
                        }
                    }
                }
                //if (dpDateBefore.SelectedDate > dpDateFrom.SelectedDate)
                //{
                //    MessageBox.Show("Дата окончания должна быть больше даты начала формирования отчета!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                //    dpDateBefore.SelectedDate = DateTime.Today;
                //    dpDateFrom.SelectedDate = DateTime.Today;
                //}
                if (dpDateBefore.SelectedDate < dpDateFrom.SelectedDate)
                {
                    MessageBox.Show("Дата окончания должна быть больше даты начала формирования отчета!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    dpDateBefore.SelectedDate = DateTime.Today;
                    dpDateFrom.SelectedDate = DateTime.Today;
                }
                foreach (var dataPoint in tasks)
                    {
                        curSeries.Points.Clear();
                        curSeries.Points.AddXY(dataPoint.Key.LastName, dataPoint.Value);
                    }
                }
            }
        

            private void UpdateChartDate(object sender, SelectionChangedEventArgs e)
            {
                if (cmbStatus.SelectedItem is TaskStatu curStatus && cmbDiagram.SelectedItem is SeriesChartType currType)
                {
                    Series curSeries = ChartStaff.Series.FirstOrDefault();
                    Dictionary<User, decimal> tasks = new Dictionary<User, decimal>();
                    curSeries.ChartType = currType;
                    curSeries.Points.Clear();

                    var task = _context.TaskProjects.ToList();
                    var userList = App.DataBase.Users.ToList();
                    foreach (var user in userList)
                    {
                        
                    foreach (var currentTask in task)
                        {
                            if (currentTask.TaskStart >= dpDateFrom.SelectedDate && currentTask.TaskStart <= dpDateBefore.SelectedDate)
                            {
                            //curSeries.Points.Clear();

                            //curSeries.Points.AddXY(user.LastName,
                            //    App.DataBase.TaskProjects.ToList().Where(p => p.TaskStatu == curStatus
                            //    && p.User == user).Count());
                            //curSeries.Points.Clear();
                            curSeries.Points.AddXY(user.LastName,
                                App.DataBase.TaskProjects.ToList().Where(p => p.TaskStatu == curStatus
                                && p.User == user && p.TaskStart == currentTask.TaskStart && p.TaskDeadLine == currentTask.TaskDeadLine).Count());
                                //curSeries.Points.Clear();

                                //if (!tasks.ContainsKey(currentTask.User))
                                //    tasks.Add(currentTask.User, 0);

                                //tasks[currentTask.User] += currentTask.User.TaskProjects.ToList().Where(p => p.TaskStart == currentTask.TaskStart && p.TaskDeadLine == currentTask.TaskDeadLine).Count();
                            }
                        }
                    }
                    //if (dpDateBefore.SelectedDate > dpDateFrom.SelectedDate)
                    //{
                    //    MessageBox.Show("Дата окончания должна быть больше даты начала формирования отчета!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    //    dpDateBefore.SelectedDate = DateTime.Today;
                    //    dpDateFrom.SelectedDate = DateTime.Today;
                    //}
                    if (dpDateBefore.SelectedDate < dpDateFrom.SelectedDate)
                    {
                        MessageBox.Show("Дата окончания должна быть больше даты начала формирования отчета!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        dpDateBefore.SelectedDate = DateTime.Today;
                        dpDateFrom.SelectedDate = DateTime.Today;
                    }
                    foreach (var dataPoint in tasks)
                    {
                        curSeries.Points.Clear();
                        curSeries.Points.AddXY(dataPoint.Key.LastName, dataPoint.Value);
                    }
                }
            }
        private void dpDateBefore_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //var task = _context.TaskProjects.ToList();
            //Dictionary<User, decimal> tasks = new Dictionary<User, decimal>();
            //Series taskStartSeries = ChartStaff.Series.FirstOrDefault();
            //if (cmbDiagram.SelectedItem is SeriesChartType currentType)
            //{
            //    taskStartSeries.ChartType = currentType;
            //    taskStartSeries.Points.Clear();
            //}
            //foreach (var currentTask in task)
            //{
            //    if (currentTask.TaskStart >= dpDateFrom.SelectedDate && currentTask.TaskStart <= dpDateBefore.SelectedDate)
            //    {

            //        if (!tasks.ContainsKey(currentTask.User))
            //            tasks.Add(currentTask.User, 0);

            //        tasks[currentTask.User] += currentTask.User.TaskProjects.Count();
            //    }
            //}
            
            //foreach (var dataPoint in tasks)
            //{
            //    taskStartSeries.Points.AddXY(dataPoint.Key.LastName, dataPoint.Value);
            //}
        }
    }
}
