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
    /// Логика взаимодействия для PageStaffList.xaml
    /// </summary>
    public partial class PageStaffList
    {
        private int _currentPage = 1;
        private int _countOfItems = 5;
        private int _numberOfPages;

        Department currentDep;

        private List<UX.Entity.User> _user;
        private List<UX.Entity.User> _filteruser;
        public PageStaffList(Department _selectedDep)
        {
            InitializeComponent();

            if(_selectedDep != null)
            {
                currentDep = _selectedDep;
                DataContext = _selectedDep;
                _user = App.DataBase.Users.Where(p => p.DepartmentID == currentDep.DepartmentID && p.RoleID == 3).ToList();
                DGStaff.ItemsSource = _user;
            }

        }
        private void NavigationChange()
        {
            int count = 0;
            int page = 0;
            pageList.Items.Clear();
            count = _user.Count;
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
            _filteruser = _user;
            
                if (currentDep != null)
                {
                    _filteruser = App.DataBase.Users.Where(p => p.DepartmentID == currentDep.DepartmentID && p.RoleID == 3).ToList();
                }
            _filteruser = _filteruser.Where(p => p.LastName.ToUpper().Contains(txtFilter.Text.ToUpper()) || p.FirstName.ToUpper().Contains(txtFilter.Text.ToUpper()) || p.Patronymic.ToUpper().Contains(txtFilter.Text.ToUpper())).ToList();

            _user = _filteruser;
            DGStaff.ItemsSource = _filteruser.Take(_countOfItems).ToList();
            NavigationChange();

        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                DataGridUpdate();
                NavigationChange();
            }
            
        }
        private void UCPageBtn_ButtonClick(object sender, RoutedEventArgs e)
        {
            _currentPage = (int)sender;
            int _minusPage = _currentPage - 1;
            if (_currentPage > 1)
            {
                DGStaff.ItemsSource = _user.Skip(_countOfItems * _minusPage).Take(_countOfItems).ToList();

            }
            else
            {
                DGStaff.ItemsSource = _user.Take(_countOfItems).ToList();
            }
            NavigationChange();
        }

        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {
            if(_currentPage > 0)
            {
                int _minusPage = _currentPage - 1;
                DGStaff.ItemsSource = _user.Take(_countOfItems * _currentPage).Skip(_countOfItems * _minusPage).ToList();
                _currentPage--;
            }
            NavigationChange();

        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage < _numberOfPages)
            {
                DGStaff.ItemsSource = _user.Skip(_countOfItems * _currentPage).Take(_countOfItems).ToList();
                _currentPage++;
            }
            NavigationChange();

        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataGridUpdate();
            NavigationChange();
        }
    }
}
