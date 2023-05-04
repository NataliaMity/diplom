using MityaginaNP.UX.Class;
using MityaginaNP.UX.Entity;
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
        private int _currentPage = 1;
        private int _countOfItems = 1;
        private int _numberOfPages;
        public PageTaskList(Project selectedProject)
        {
            InitializeComponent();
            _selectedProject = selectedProject;
            DataContext = selectedProject;
        }

        private void Page_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                DGTask.ItemsSource = App.DataBase.TaskProjects.ToList().Where(p => p.ProjectID == _selectedProject.ProjectID);
                _countOfItems = 1;
                int count = DGTask.Items.Count;
                if (count % _countOfItems == 0)
                    _numberOfPages = count / _countOfItems;
                else
                    _numberOfPages = count / _countOfItems + 1;

                DGTask.ItemsSource = App.DataBase.TaskProjects.ToList().Where(p => p.ProjectID == _selectedProject.ProjectID).Take(_countOfItems).ToList();


                for (int i = 1; i <= _numberOfPages; i++)
                {
                    int page = i;
                    pageList.Items.Add(page.ToString());
                    page = page + 1;

                }

            }
        }

        private void UCPageBtn_ButtonClick(object sender, RoutedEventArgs e)
        {
            _currentPage = (int)sender;
            int _minusPage = _currentPage - 1;
            if (_currentPage > 1)
            {
                DGTask.ItemsSource = App.DataBase.TaskProjects.ToList().Where(p => p.ProjectID == _selectedProject.ProjectID).Skip(_countOfItems * _minusPage).Take(_countOfItems).ToList();

            }
            else
            {
                DGTask.ItemsSource = App.DataBase.TaskProjects.ToList().Where(p => p.ProjectID == _selectedProject.ProjectID).Take(_countOfItems).ToList();
            }
        }

        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {
            if(_currentPage > 0)
            {
                int _minusPage = _currentPage - 1;
                DGTask.ItemsSource = App.DataBase.TaskProjects.ToList().Where(p => p.ProjectID == _selectedProject.ProjectID).Take(_countOfItems * _currentPage).Skip(_countOfItems * _minusPage).ToList();
                _currentPage--;
            }
            
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if(_currentPage < _numberOfPages)
            {
                DGTask.ItemsSource = App.DataBase.TaskProjects.ToList().Where(p => p.ProjectID == _selectedProject.ProjectID).Skip(_countOfItems * _currentPage).Take(_countOfItems).ToList();
                _currentPage++;
            }
            
        }

        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageAddEditTask(null, _selectedProject.ProjectID));
            /*ClassNavigate.NavigateFrame.Navigate(new PageAddEditTask((sender as Button).DataContext as Project));*/
        }
    }
}
