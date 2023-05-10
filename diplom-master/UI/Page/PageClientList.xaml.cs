using MityaginaNP.UX.Class;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MityaginaNP.UI.Page
{
    /// <summary>
    /// Логика взаимодействия для PageClientList.xaml
    /// </summary>
    public partial class PageClientList 
    {
        private int _currentPage = 1;
        private int _countOfItems = 3;
        private int _numberOfPages;

        private List<UX.Entity.Client> _client;
        private List<UX.Entity.Client> _filterclient;

        public PageClientList()
        {
            InitializeComponent();
            _client = App.DataBase.Clients.ToList();
            DGClients.ItemsSource = _client;
            NavigationChange();
            DataGridUpdate();
        }

        private void NavigationChange()
        {
            int count = 0;
            int page = 0;
            pageList.Items.Clear();
            count = _client.Count;
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
            _filterclient = _client;
            _filterclient = App.DataBase.Clients.ToList();
            if (txtFilter.Text != null)
                _filterclient = _filterclient.Where(p => p.ClientName.ToUpper().Contains(txtFilter.Text.ToUpper())).ToList();
            _client = _filterclient;
            DGClients.ItemsSource = _filterclient.Take(_countOfItems).ToList();
            NavigationChange();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
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
                DGClients.ItemsSource = _client.Skip(_countOfItems * _minusPage).Take(_countOfItems).ToList();
            }
            else
            {
                DGClients.ItemsSource = _client.Take(_countOfItems).ToList();
            }
            NavigationChange();

        }

        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {
            if(_currentPage > 0)
            {
                int _minusPage = _currentPage - 1;
                DGClients.ItemsSource = _client.Take(_countOfItems * _currentPage).Skip(_countOfItems * _minusPage).ToList();
                _currentPage--;
            }
            NavigationChange();

        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if(_currentPage < _numberOfPages)
            {
                DGClients.ItemsSource = _client.Skip(_countOfItems * _currentPage).Take(_countOfItems).ToList();
                _currentPage++;
            }
            NavigationChange();

        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageAddEditClients(null));
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            NavigationChange();
            DataGridUpdate();
        }
    }
}
