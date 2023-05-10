using MityaginaNP.UX.Class;
using MityaginaNP.UX.Entity;
using Syncfusion.Linq;
using Syncfusion.ProjIO;
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
    /// Логика взаимодействия для PageProjects.xaml
    /// </summary>
    public partial class PageProjects
    {
        private int _currentPage = 1;
        private int _countOfItems = 1;
        private int _numberOfPages;
        private int _minusPage = 0;
        bool selected = true;

        User currentUser;

        private List<UX.Entity.Project> _project;
        private List<UX.Entity.Project> _filterproject;

        public PageProjects(User _selectedUser)
        {
            InitializeComponent();
            ProjectNow.IsEnabled = false;
            if( _selectedUser != null )
            {
                currentUser = _selectedUser;
                _project = App.DataBase.Projects.Where(p => p.UserLogin == currentUser.Login && p.ProjectActual == "1").ToList();
                DGProject.ItemsSource = _project;
            }
            else
            {
                _project = App.DataBase.Projects.Where(p => p.ProjectActual == "1").ToList();
                DGProject.ItemsSource = _project;
            }
            
        }
        private void NavigationChange()
        {
            int count = 0;
            int page = 0;
            pageList.Items.Clear();
                count = _project.Count;
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
            _filterproject = _project;
            if(ProjectNow.IsEnabled == false)
            {
                if(currentUser != null)
                {
                    _filterproject = App.DataBase.Projects.Where(p => p.UserLogin == currentUser.Login && p.ProjectActual == "1").ToList();
                }
                else
                {
                    _filterproject = App.DataBase.Projects.Where(p => p.ProjectActual == "1").ToList();
                }
            }
            if(ProjectEnd.IsEnabled == false)
            {
                if (currentUser != null)
                {
                    _filterproject = App.DataBase.Projects.Where(p => p.UserLogin == currentUser.Login && p.ProjectActual == "0").ToList();

                }
                else
                {
                    _filterproject = App.DataBase.Projects.Where(p => p.ProjectActual == "0").ToList();

                }
            }
            _filterproject = _filterproject.Where(p => p.ProjectName.ToUpper().Contains(txtFilter.Text.ToUpper())).ToList();
            
            _project = _filterproject;
            DGProject.ItemsSource = _filterproject.Take(_countOfItems).ToList();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                NavigationChange();
                DataGridUpdate();

            }
        }

        private void UCPageBtn_ButtonClick(object sender, RoutedEventArgs e)
        {
            _currentPage = (int)sender;
            _minusPage = _currentPage - 1;
            if (_currentPage > 1)
            {
                DGProject.ItemsSource = _project.ToList().Skip(_countOfItems * _minusPage).Take(_countOfItems).ToList();

            }
            else
            {
                DGProject.ItemsSource = _project.ToList().Take(_countOfItems).ToList();
            }
            NavigationChange();

        }

        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {
            if(_currentPage > 0)
            {
                _minusPage = _currentPage - 1;
                DGProject.ItemsSource = _project.ToList().Take(_countOfItems * _currentPage).Skip(_countOfItems * _minusPage).ToList();
                _currentPage--;
            }
                NavigationChange();

        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if(_currentPage < _numberOfPages)
            {
                DGProject.ItemsSource = _project.ToList().Skip(_countOfItems * _currentPage).Take(_countOfItems).ToList();
                _currentPage++;
            }
            NavigationChange();

        }

        private void ProjectNow_Click(object sender, RoutedEventArgs e)
        {
            ProjectNow.IsEnabled = false;
            ProjectEnd.IsEnabled = true;
                
            DataGridUpdate();
            NavigationChange();
        }

        private void ProjectEnd_Click(object sender, RoutedEventArgs e)
        {
            ProjectEnd.IsEnabled = false;
            ProjectNow.IsEnabled = true;
                
            DataGridUpdate();
            NavigationChange();
        }

        private void btnAddProject_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageAddEditProject(null, currentUser));
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataGridUpdate();
            NavigationChange();

        }
    }
}
