using MityaginaNP.UX.Class;
using MityaginaNP.UX.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MityaginaNP.UI.Page
{
    /// <summary>
    /// Логика взаимодействия для PageTaskList.xaml
    /// </summary>
    public partial class PageTaskList
    {
        private Project _selectedProject;
        private Department _selectedDep;
        private User _selectedUser;
        private int _currentPage = 1;
        private int _countOfItems = 1;
        private int _numberOfPages;
        private int _minusPage;

        private List<UX.Entity.TaskProject> _task;
        private List<UX.Entity.TaskProject> _filtertask;

        public PageTaskList(Project selectedProject, Department selectedDep, User selectedUser)
        {
            InitializeComponent();
            TaskNow.IsEnabled = false;
            if (selectedProject != null)
            {
                _selectedProject = selectedProject;
                //DataContext = selectedProject;
                _task = App.DataBase.TaskProjects.Where(p => p.ProjectID == _selectedProject.ProjectID).ToList();
                
            }
            if(selectedDep != null)
            {
                _selectedDep = selectedDep;
                DataContext = selectedDep;
                _task = App.DataBase.TaskProjects.Where(p => p.DepartmentID == _selectedDep.DepartmentID && p.StatusId == 2 || p.StatusId == 3 || p.StatusId == 4).ToList();

            }
            if (selectedUser != null)
            {
                _selectedUser = selectedUser;
                DataContext = selectedUser;
                _task = App.DataBase.TaskProjects.Where(p => p.UserLogin == _selectedUser.Login && p.StatusId == 2 || p.StatusId == 3 || p.StatusId == 4).ToList();

            }
            DGTask.ItemsSource = _task;
        }
        private void NavigationChange()
        {
            int count = 0;
            int page = 0;
            pageList.Items.Clear();
            count = _task.Count;
            if (count % _countOfItems == 0)
                _numberOfPages = count / _countOfItems;
            else
                _numberOfPages = count / _countOfItems + 1;
            for (int i = 1; i <= _numberOfPages; i++)
            {
                page = i;
                pageList.Items.Add(page.ToString());
                page = page + 1;

            }
        }
        private void DataGridUpdate()
        {
            _filtertask = _task;
            if (TaskNow.IsEnabled == false)
            {
                if (_selectedDep != null)
                    _filtertask = App.DataBase.TaskProjects.Where(p =>  p.StatusId == 2 || p.StatusId == 3 || p.StatusId == 1).Where(p => p.DepartmentID == _selectedDep.DepartmentID).ToList();
                if (_selectedProject != null)
                    _filtertask = App.DataBase.TaskProjects.Where(p => p.StatusId == 2 || p.StatusId == 3 || p.StatusId == 1).Where(p => p.ProjectID == _selectedProject.ProjectID).ToList();
                if (_selectedUser != null)
                    _filtertask = App.DataBase.TaskProjects.Where(p => p.StatusId == 2 || p.StatusId == 3 || p.StatusId == 1).Where(p => p.UserLogin == _selectedUser.Login).ToList();
            }
            if (TaskEnd.IsEnabled == false)
            {
                if (_selectedDep != null)
                    _filtertask = App.DataBase.TaskProjects.Where(p => p.DepartmentID == _selectedDep.DepartmentID && p.StatusId == 4).ToList();
                if (_selectedProject != null)
                    _filtertask = App.DataBase.TaskProjects.Where(p => p.ProjectID == _selectedProject.ProjectID && p.StatusId == 4).ToList();
                if (_selectedUser != null)
                    _filtertask = App.DataBase.TaskProjects.Where(p => p.UserLogin == _selectedUser.Login && p.StatusId == 4).ToList();
            }
            _filtertask = _filtertask.Where(p => p.TaskText.ToUpper().Contains(txtFilter.Text.ToUpper())).ToList();

            _task = _filtertask;
            DGTask.ItemsSource = _filtertask.Take(_countOfItems).ToList();
            NavigationChange();
        }
        private void Page_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                NavigationChange();
                DataGridUpdate();
            }
        }

        private void UCPageBtn_ButtonClick(object sender, RoutedEventArgs e)
        {
            _currentPage = (int)sender;
            int _minusPage = _currentPage - 1;
            if (_currentPage > 1)
            {
                //if(_selectedProject != null)
                DGTask.ItemsSource = _task.ToList().Skip(_countOfItems * _minusPage).Take(_countOfItems).ToList();
                //if(_selectedDep != null)
                //    DGTask.ItemsSource = App.DataBase.TaskProjects.ToList().Where(p => p.DepartmentID == _selectedDep.DepartmentID).Skip(_countOfItems * _minusPage).Take(_countOfItems).ToList();
                //if (_selectedUser != null)
                //    DGTask.ItemsSource = App.DataBase.TaskProjects.ToList().Where(p => p.UserLogin == _selectedUser.Login).Skip(_countOfItems * _minusPage).Take(_countOfItems).ToList();


            }
            else
            {
                //if (_selectedProject != null)
                DGTask.ItemsSource = _task.ToList().Take(_countOfItems).ToList();
                //if (_selectedDep != null)
                //    DGTask.ItemsSource = App.DataBase.TaskProjects.ToList().Where(p => p.DepartmentID == _selectedDep.DepartmentID).Take(_countOfItems).ToList();
                //if (_selectedUser != null)
                //    DGTask.ItemsSource = App.DataBase.TaskProjects.ToList().Where(p => p.UserLogin == _selectedUser.Login).Take(_countOfItems).ToList();

            }
            NavigationChange();
        }

        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {
            if(_currentPage > 0)
            {
                int _minusPage = _currentPage - 1;
                //if(_selectedProject  != null)
                DGTask.ItemsSource = _task.ToList().Take(_countOfItems * _currentPage).Skip(_countOfItems * _minusPage).ToList();
                //if(_selectedDep != null)
                //    DGTask.ItemsSource = App.DataBase.TaskProjects.ToList().Where(p => p.DepartmentID == _selectedDep.DepartmentID).Take(_countOfItems * _currentPage).Skip(_countOfItems * _minusPage).ToList();
                //if (_selectedUser != null)
                //    DGTask.ItemsSource = App.DataBase.TaskProjects.ToList().Where(p => p.UserLogin == _selectedUser.Login).Take(_countOfItems * _currentPage).Skip(_countOfItems * _minusPage).ToList();

                _currentPage--;
            }
            NavigationChange();
            
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if(_currentPage < _numberOfPages)
            {
                //if (_selectedProject != null)
                DGTask.ItemsSource = _task.ToList().Skip(_countOfItems * _currentPage).Take(_countOfItems).ToList();
                //if (_selectedDep != null)
                //    DGTask.ItemsSource = App.DataBase.TaskProjects.ToList().Where(p => p.DepartmentID == _selectedDep.DepartmentID).Skip(_countOfItems * _currentPage).Take(_countOfItems * _currentPage).ToList();
                //if (_selectedUser != null)
                //    DGTask.ItemsSource = App.DataBase.TaskProjects.ToList().Where(p => p.UserLogin == _selectedUser.Login).Skip(_countOfItems * _currentPage).Take(_countOfItems * _currentPage).ToList();

                _currentPage++;
            }
            NavigationChange();

        }

        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageAddEditTask(null, _selectedProject.ProjectID));
        }

        private void TaskNow_Click(object sender, RoutedEventArgs e)
        {
            TaskNow.IsEnabled = false;
            TaskEnd.IsEnabled = true;

            DataGridUpdate();
            NavigationChange();
        }

        private void TaskEnd_Click(object sender, RoutedEventArgs e)
        {
            TaskEnd.IsEnabled = false;
            TaskNow.IsEnabled = true;

            DataGridUpdate();
            NavigationChange();
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataGridUpdate();
            NavigationChange();
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            if (ClassNavigate.NavigateFrame.CanGoBack)
                ClassNavigate.NavigateFrame.GoBack();
            else
                btnGoBack.Visibility = Visibility.Hidden;
        }
    }
}
