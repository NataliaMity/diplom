using MityaginaNP.UX.Entity;
using nGantt;
using nGantt.GanttChart;
using nGantt.PeriodSplitter;
using Syncfusion.Windows.Controls.Gantt;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
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
    /// Логика взаимодействия для PageGanttChart.xaml
    /// </summary>
    public partial class PageGanttChart
    {
        private Project curProject;
        private Department curDep;
        private int GantLenght { get; set; }
        private ObservableCollection<ContextMenuItem> ganttTaskContextMenuItems = new ObservableCollection<ContextMenuItem>();
        private ObservableCollection<SelectionContextMenuItem> selectionContextMenuItems = new ObservableCollection<SelectionContextMenuItem>();
        private User curUser;

        public PageGanttChart(Project selectedProject, Department selectedDep, User selectedUser)
        {
            InitializeComponent();
            if (selectedProject != null)
                curProject = selectedProject;
            if (selectedDep != null)
                curDep = selectedDep;
            if (selectedUser != null)
                curUser = selectedUser;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GantLenght = 25;
            if(curProject != null)
            {
                dateTimePicker.SelectedDate = curProject.ProjectStartDate;
                DateTime minDate = (DateTime)curProject.ProjectStartDate;
                DateTime maxDate = (DateTime)curProject.ProjectEndDate;

                ganttControl1.TaskSelectionMode = nGantt.GanttControl.SelectionMode.Single;
                ganttControl1.AllowUserSelection = true;
                ganttControl1.GanttRowAreaSelected += new EventHandler<PeriodEventArgs>(ganttControl1_GanttRowAreaSelected);
            }
            else if(curDep != null)
            {
                dateTimePicker.SelectedDate = DateTime.Today;
                DateTime minDate = (DateTime)dateTimePicker.SelectedDate;
                DateTime maxDate = minDate.AddDays(GantLenght);
                ganttControl1.TaskSelectionMode = nGantt.GanttControl.SelectionMode.Single;
                ganttControl1.AllowUserSelection = true;
                ganttControl1.GanttRowAreaSelected += new EventHandler<PeriodEventArgs>(ganttControl1_GanttRowAreaSelected);
            }
            else if (curUser != null)
            {
                dateTimePicker.SelectedDate = DateTime.Today;
                DateTime minDate = (DateTime)dateTimePicker.SelectedDate;
                DateTime maxDate = minDate.AddDays(GantLenght);
                ganttControl1.TaskSelectionMode = nGantt.GanttControl.SelectionMode.Single;
                ganttControl1.AllowUserSelection = true;
                ganttControl1.GanttRowAreaSelected += new EventHandler<PeriodEventArgs>(ganttControl1_GanttRowAreaSelected);
            }
            else
            {
                dateTimePicker.SelectedDate = DateTime.Today;
                DateTime minDate = (DateTime)dateTimePicker.SelectedDate;
                DateTime maxDate = minDate.AddDays(GantLenght);
                ganttControl1.TaskSelectionMode = nGantt.GanttControl.SelectionMode.Single;
                ganttControl1.AllowUserSelection = true;
                ganttControl1.GanttRowAreaSelected += new EventHandler<PeriodEventArgs>(ganttControl1_GanttRowAreaSelected);
            }

        }

        private void ganttControl1_GanttRowAreaSelected(object sender, PeriodEventArgs e)
        {
            MessageBox.Show(e.SelectionStart.ToString() + " -> " + e.SelectionEnd.ToString());
        }
        private System.Windows.Media.Brush DetermineBackground(TimeLineItem timeLineItem)
        {
            if (timeLineItem.End.Date.DayOfWeek == DayOfWeek.Saturday || timeLineItem.End.Date.DayOfWeek == DayOfWeek.Sunday)
                return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LightBlue);
            else
                return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent);
        }
        private void CreateData(DateTime minDate, DateTime maxDate)
        {
            if (curProject != null)
            {
                ganttControl1.Initialize(minDate, maxDate);
                ganttControl1.CreateTimeLine(new PeriodYearSplitter(minDate, maxDate), FormatYear);
                ganttControl1.CreateTimeLine(new PeriodMonthSplitter(minDate, maxDate), FormatMonth);
                var gridLineTimeLine = ganttControl1.CreateTimeLine(new PeriodDaySplitter(minDate, maxDate), FormatDay);
                ganttControl1.CreateTimeLine(new PeriodDaySplitter(minDate, maxDate), FormatDayName);
                ganttControl1.SetGridLinesTimeline(gridLineTimeLine, DetermineBackground);


                var currentTasks = App.DataBase.TaskProjects.ToList().Where(p => p.ProjectID == curProject.ProjectID);
                try
                {
                    var rowgroup1 = ganttControl1.CreateGanttRowGroup(curProject.ProjectName);

                    foreach (var task in currentTasks)
                    {
                        var row1 = ganttControl1.CreateGanttRow(rowgroup1, task.TaskText);

                        ganttControl1.AddGanttTask(row1, new GanttTask() { Start = (DateTime)task.TaskStart, End = (DateTime)task.TaskDeadLine, Name = task.TaskText, TaskProgressVisibility = Visibility.Collapsed });
                    }
                }
                catch
                {

                }

            }
            else if (curDep != null)
            {
                ganttControl1.Initialize(minDate, maxDate);
                ganttControl1.CreateTimeLine(new PeriodYearSplitter(minDate, maxDate), FormatYear);
                ganttControl1.CreateTimeLine(new PeriodMonthSplitter(minDate, maxDate), FormatMonth);
                var gridLineTimeLine = ganttControl1.CreateTimeLine(new PeriodDaySplitter(minDate, maxDate), FormatDay);
                ganttControl1.CreateTimeLine(new PeriodDaySplitter(minDate, maxDate), FormatDayName);
                ganttControl1.SetGridLinesTimeline(gridLineTimeLine, DetermineBackground);

                var currentTasks = App.DataBase.TaskProjects.ToList().Where(p => p.DepartmentID == curDep.DepartmentID).ToList();
                var currentProjects = App.DataBase.Projects.ToList();
                try
                {
                    var rowgroup1 = ganttControl1.CreateGanttRowGroup("Текущие");
                    foreach (var task in currentTasks)
                    {
                        if (task.UserLogin == null)
                        {
                            var row = ganttControl1.CreateGanttRow(rowgroup1, "Нет исполнителя");
                            ganttControl1.AddGanttTask(row, new GanttTask() { Start = (DateTime)task.TaskStart, End = (DateTime)task.TaskDeadLine, Name = task.TaskText, TaskProgressVisibility = Visibility.Collapsed });

                        }
                        else
                        {
                            var row = ganttControl1.CreateGanttRow(rowgroup1, task.User.LastName);
                            ganttControl1.AddGanttTask(row, new GanttTask() { Start = (DateTime)task.TaskStart, End = (DateTime)task.TaskDeadLine, Name = task.TaskText, TaskProgressVisibility = Visibility.Collapsed });

                        }
                        
                    }
                }
                catch
                {

                }
            }
            else if (curUser != null)
            {
                ganttControl1.Initialize(minDate, maxDate);
                ganttControl1.CreateTimeLine(new PeriodYearSplitter(minDate, maxDate), FormatYear);
                ganttControl1.CreateTimeLine(new PeriodMonthSplitter(minDate, maxDate), FormatMonth);
                var gridLineTimeLine = ganttControl1.CreateTimeLine(new PeriodDaySplitter(minDate, maxDate), FormatDay);
                ganttControl1.CreateTimeLine(new PeriodDaySplitter(minDate, maxDate), FormatDayName);
                ganttControl1.SetGridLinesTimeline(gridLineTimeLine, DetermineBackground);

                var currentTasks = App.DataBase.TaskProjects.ToList().Where(p => p.UserLogin == curUser.Login && p.StatusId != 4).ToList();
                var currentProjects = App.DataBase.Projects.ToList();
                try
                {
                    var rowgroup1 = ganttControl1.CreateGanttRowGroup("Текущие");
                    foreach (var task in currentTasks)
                    {
                        var row = ganttControl1.CreateGanttRow(rowgroup1, task.TaskText);
                        ganttControl1.AddGanttTask(row, new GanttTask() { Start = (DateTime)task.TaskStart, End = (DateTime)task.TaskDeadLine, Name = task.TaskText, TaskProgressVisibility = Visibility.Collapsed });
                    }
                }
                catch
                {

                }
            }
            else
            {
                ganttControl1.Initialize(minDate, maxDate);
                ganttControl1.CreateTimeLine(new PeriodYearSplitter(minDate, maxDate), FormatYear);
                ganttControl1.CreateTimeLine(new PeriodMonthSplitter(minDate, maxDate), FormatMonth);
                var gridLineTimeLine = ganttControl1.CreateTimeLine(new PeriodDaySplitter(minDate, maxDate), FormatDay);
                ganttControl1.CreateTimeLine(new PeriodDaySplitter(minDate, maxDate), FormatDayName);
                ganttControl1.SetGridLinesTimeline(gridLineTimeLine, DetermineBackground);

                var currentTasks = App.DataBase.TaskProjects.ToList();
                var currentProjects = App.DataBase.Projects.ToList();
                try
                {
                    var rowgroup1 = ganttControl1.CreateGanttRowGroup("Текущие");
                    foreach (var project in currentProjects)
                    {
                        
                        var row = ganttControl1.CreateGanttRow(rowgroup1, project.ProjectName);
                        ganttControl1.AddGanttTask(row, new GanttTask() { Start = (DateTime)project.ProjectStartDate, End = (DateTime)project.ProjectEndDate, Name = project.ProjectName, TaskProgressVisibility = Visibility.Collapsed });
                    }
                }
                catch
                {

                }
            }
        }
        private string FormatYear(Period period)
        {
            return period.Start.Year.ToString("");
        }

        private string FormatMonth(Period period)
        {
            return period.Start.Month.ToString("");
        }

        private string FormatDay(Period period)
        {
            return period.Start.Day.ToString("");
        }

        private string FormatDayName(Period period)
        {
            return period.Start.ToString("ddd");
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            dateTimePicker.SelectedDate = ganttControl1.GanttData.MinDate.AddDays(-GantLenght);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            dateTimePicker.SelectedDate = ganttControl1.GanttData.MaxDate;
        }

        private void dateTimePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime minDate = (DateTime)dateTimePicker.SelectedDate;
            DateTime maxDate = minDate.AddDays(GantLenght);
            ganttControl1.ClearGantt();
            CreateData(minDate, maxDate);
        }
    }
}
