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
            if(cmbStatus.SelectedItem is TaskStatu curStatus && cmbDiagram.SelectedItem is SeriesChartType currType)
            {
                Series curSeries = ChartStaff.Series.FirstOrDefault();
                curSeries.ChartType = currType;
                curSeries.Points.Clear();

                var userList = App.DataBase.Users.ToList();
                foreach(var user in userList)
                {
                    curSeries.Points.AddXY(user.LastName, 
                        App.DataBase.TaskProjects.ToList().Where(p => p.TaskStatu == curStatus 
                        && p.User == user).Count());
                }
            }
        }

        private void dpDateFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //var task = _context.TaskProjects.ToList();
            //decimal consumption = 0;
            //tblConsumption.Text = consumption.ToString();
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
            //if (dpDateBefore.SelectedDate > dpDateFrom.SelectedDate)
            //{
            //    MessageBox.Show("Дата окончания должна быть больше даты начала формирования отчета!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    dpDateBefore.SelectedDate = DateTime.Today;
            //    dpDateFrom.SelectedDate = DateTime.Today;
            //}
            //foreach (var dataPoint in tasks)
            //{
            //    taskStartSeries.Points.AddXY(dataPoint.Key.LastName, dataPoint.Value);
            //    consumption += dataPoint.Value;
            //    tblConsumption.Text = consumption.ToString("N2");
            //}
            //if (tblConsumption.Text == "0")
            //{
            //    //tblNoSupply.Visibility = Visibility.Visible;
            //    //wfhSupply.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    wfhStaff.Visibility = Visibility.Visible;
            //}
        }

        private void dpDateBefore_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //var task = _context.TaskProjects.ToList();
            //decimal consumption = 0;
            //tblConsumption.Text = consumption.ToString();
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
            //if (dpDateBefore.SelectedDate < dpDateFrom.SelectedDate)
            //{
            //    MessageBox.Show("Дата окончания должна быть больше даты начала формирования отчета!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    dpDateBefore.SelectedDate = DateTime.Today;
            //    dpDateFrom.SelectedDate = DateTime.Today;
            //}
            //foreach (var dataPoint in tasks)
            //{
            //    taskStartSeries.Points.AddXY(dataPoint.Key.LastName, dataPoint.Value);
            //    consumption += dataPoint.Value;
            //    tblConsumption.Text = consumption.ToString("N2");
            //}
            //if (tblConsumption.Text == "0")
            //{
            //    //tblNoSupply.Visibility = Visibility.Visible;
            //    //wfhSupply.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    wfhStaff.Visibility = Visibility.Visible;
            //}
        }



        private void cmbDiagram_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //var task = _context.TaskProjects.ToList();
            //decimal consumption = 0;
            //tblConsumption.Text = consumption.ToString();
            //Dictionary<User, decimal> newtasks = new Dictionary<User, decimal>();
            //Dictionary<User, decimal> endtasks = new Dictionary<User, decimal>();
            //Dictionary<User, decimal> worktasks = new Dictionary<User, decimal>();
            //Dictionary<User, decimal> starttasks = new Dictionary<User, decimal>();
            //Series taskStartSeries = ChartStaff.Series.FirstOrDefault();
            //Series taskWorkSeries = ChartStaff.Series.FirstOrDefault();
            //Series taskCompleteSeries = ChartStaff.Series.FirstOrDefault();
            //Series taskDoesntStartSeries = ChartStaff.Series.FirstOrDefault();
            //if (cmbDiagram.SelectedItem is SeriesChartType currentType)
            //{
            //    taskStartSeries.ChartType = currentType;
            //    taskWorkSeries.ChartType = currentType;
            //    taskCompleteSeries.ChartType = currentType;
            //    taskDoesntStartSeries.ChartType = currentType;
            //    taskStartSeries.Points.Clear();
            //    taskWorkSeries.Points.Clear();
            //    taskCompleteSeries.Points.Clear();
            //    taskDoesntStartSeries.Points.Clear();
            //}
            //foreach (var newTask in task)
            //{
            //    if (newTask.TaskStart <= dpDateFrom.SelectedDate)
            //    {

            //        if (!newtasks.ContainsKey(newTask.User))
            //            newtasks.Add(newTask.User, 0);

            //        newtasks[newTask.User] = newTask.User.TaskProjects.Where(p => p.StatusId == 1).Count();
            //    }
            //}
            //foreach (var endTask in task)
            //{
            //    if (endTask.TaskStart <= dpDateFrom.SelectedDate)
            //    {

            //        if (!endtasks.ContainsKey(endTask.User))
            //            endtasks.Add(endTask.User, 0);

            //        endtasks[endTask.User] = endTask.User.TaskProjects.Where(p => p.StatusId == 4).Count();
            //    }
            //}
            //foreach (var workTask in task)
            //{
            //    if (workTask.TaskStart <= dpDateFrom.SelectedDate)
            //    {

            //        if (!worktasks.ContainsKey(workTask.User))
            //            worktasks.Add(workTask.User, 0);

            //        worktasks[workTask.User] = workTask.User.TaskProjects.Where(p => p.StatusId == 3).Count();
            //    }
            //}
            //foreach (var startTask in task)
            //{
            //    if (startTask.TaskStart <= dpDateFrom.SelectedDate)
            //    {

            //        if (!starttasks.ContainsKey(startTask.User))
            //            starttasks.Add(startTask.User, 0);

            //        starttasks[startTask.User] = startTask.User.TaskProjects.Where(p => p.StatusId == 2).Count();
            //    }
            //}

            //List<string> lastname = new List<string>();
            //var us = _context.Users;
            //for(int i = 0; i < us.Count();  i++)
            //{
            //    lastname.Add(us.Select(p=> p.LastName).ToString());
            //}

            //if (dpDateFrom.SelectedDate > dpDateBefore.SelectedDate)
            //{
            //    MessageBox.Show("Начальная дата должна быть меньше даты окончания формирования отчета!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    dpDateBefore.SelectedDate = DateTime.Today;
            //    dpDateFrom.SelectedDate = DateTime.Today;
            //}
            //else if (dpDateFrom.SelectedDate > dpDateBefore.SelectedDate)
            //{
            //    MessageBox.Show("Начальная дата должна быть меньше даты окончания формирования отчета!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    dpDateBefore.SelectedDate = DateTime.Today;
            //    dpDateFrom.SelectedDate = DateTime.Today;
            //}
            //foreach (var dataPoint in newtasks)
            //{
            //    List<decimal> y = new List<decimal>();
            //    for (int i = 0; i < newtasks.Count(); i++)
            //    {
            //        y.Add(dataPoint.Value);
            //    }
            //    taskDoesntStartSeries.Points.DataBindXY(lastname, y);
            //    consumption += dataPoint.Value;
            //    tblConsumption.Text = consumption.ToString("N2");
            //}
            //foreach (var dataPoint in endtasks)
            //{
            //    List<decimal> y = new List<decimal>();
            //    for (int i = 0; i < endtasks.Count(); i++)
            //    {
            //        y.Add(dataPoint.Value);
            //    }
            //    taskCompleteSeries.Points.DataBindXY(lastname, y);
            //    consumption += dataPoint.Value;
            //    tblConsumption.Text = consumption.ToString("N2");
            //}
            //foreach (var dataPoint in worktasks)
            //{
            //    List<decimal> y = new List<decimal>();
            //    for (int i = 0; i < worktasks.Count(); i++)
            //    {
            //        y.Add(dataPoint.Value);
            //    }
            //    taskWorkSeries.Points.DataBindXY(lastname, y);
            //    consumption += dataPoint.Value;
            //    tblConsumption.Text = consumption.ToString("N2");
            //}
            //foreach (var dataPoint in starttasks)
            //{
            //    List<decimal> y = new List<decimal>();
            //    for (int i = 0; i < starttasks.Count(); i++)
            //    {
            //        y.Add(dataPoint.Value);
            //    }
            //    taskStartSeries.Points.DataBindXY(lastname, y);
            //    consumption += dataPoint.Value;
            //    tblConsumption.Text = consumption.ToString("N2");
            //}
            //if (tblConsumption.Text == "0")
            //{
            //    //tblNoSupply.Visibility = Visibility.Visible;
            //    //wfhSupply.Visibility = Visibility.Hidden;
            //}
            //else
            //{
            //    wfhStaff.Visibility = Visibility.Visible;
            //}
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmbUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
