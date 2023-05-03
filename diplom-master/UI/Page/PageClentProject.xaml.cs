﻿using MityaginaNP.UX.Entity;
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
    /// Логика взаимодействия для PageClentProject.xaml
    /// </summary>
    public partial class PageClentProject
    {
        private Client _client;
        private int _currentPage = 1;
        private int _countOfItems = 1;
        private int _numberOfPages;
        public PageClentProject(Client clientProject)
        {
            InitializeComponent();
            _client = clientProject;
            DataContext = clientProject;
        }
        

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                DGProjectClient.ItemsSource = App.DataBase.Projects.ToList().Where(p => p.ClientID == _client.ClientID).ToList();
                _countOfItems = 1;
                int count = DGProjectClient.Items.Count;
                if (count % _countOfItems == 0)
                    _numberOfPages = count / _countOfItems;
                else
                    _numberOfPages = count / _countOfItems + 1;

                DGProjectClient.ItemsSource = App.DataBase.Projects.Take(_countOfItems).ToList();


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
                DGProjectClient.ItemsSource = App.DataBase.Projects.Skip(_countOfItems * _minusPage).Take(_countOfItems).ToList();

            }
            else
            {
                DGProjectClient.ItemsSource = App.DataBase.Projects.Take(_countOfItems).ToList();
            }
        }

        private void PrevPage_Click(object sender, RoutedEventArgs e)
        {
            int _minusPage = _currentPage - 1;
            DGProjectClient.ItemsSource = App.DataBase.Projects.Take(_countOfItems * _currentPage).Skip(_countOfItems * _minusPage).ToList();
            _currentPage--;
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            DGProjectClient.ItemsSource = App.DataBase.Projects.Skip(_countOfItems * _currentPage).Take(_countOfItems).ToList();
            _currentPage++;
        }
    }
}
