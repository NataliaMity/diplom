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

namespace MityaginaNP.UI.Page
{
    /// <summary>
    /// Логика взаимодействия для PageClientList.xaml
    /// </summary>
    public partial class PageClientList 
    {
        private int _currentPage = 1;
        private int _countOfItems = 1;
        private int _numberOfPages;
        public PageClientList()
        {
            InitializeComponent();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                DGClients.ItemsSource = App.DataBase.Clients.ToList();
                _countOfItems = 1;
                int count = DGClients.Items.Count;
                if (count % _countOfItems == 0)
                    _numberOfPages = count / _countOfItems;
                else
                    _numberOfPages = count / _countOfItems + 1;

                DGClients.ItemsSource = App.DataBase.Clients.Take(_countOfItems).ToList();


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
                DGClients.ItemsSource = App.DataBase.Clients.ToList().Skip(_countOfItems * _minusPage).Take(_countOfItems).ToList();
            }
            else
            {
                DGClients.ItemsSource = App.DataBase.Clients.ToList().Take(_countOfItems).ToList();
            }
        }

        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {
            int _minusPage = _currentPage - 1;
            DGClients.ItemsSource = App.DataBase.Clients.ToList().Take(_countOfItems * _currentPage).Skip(_countOfItems * _minusPage).ToList();
            _currentPage--;
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            DGClients.ItemsSource = App.DataBase.Clients.ToList().Skip(_countOfItems * _currentPage).Take(_countOfItems).ToList();
            _currentPage++;
        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            ClassNavigate.NavigateFrame.Navigate(new PageAddEditClients(null));
        }
    }
}
